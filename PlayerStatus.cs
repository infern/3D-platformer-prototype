using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    #region Variables

    [Header("Settings")]    /********/
    [SerializeField]
    [Range(0f, 2f)]
    float groundRayRange = 0.2f;
    [SerializeField]
    [Range(0.1f, 5f)]
    float groundRaySphereRadius = 0.2f;
    [SerializeField]
    [Range(0f, 1f)]
    float groundedGracePeriodDuration = 0.175f;
    [SerializeField]
    [Range(5f, 20f)]
    float dangerousVelocity = 16f;
    [SerializeField]
    List<AudioClip> landSounds = new List<AudioClip>();


    [Header("Data")]    /********/
    [SerializeField]
    int currentHealth;
    bool alive = true;
    public bool grounded;
    public float groundedGracePeriodTimer;
    bool groundedGracePeriodActive;
    float castingNTimer;
    float castingMTimer;
    bool landed = true;
    int landSoundsQueue;


    [Header("Components")]    /********/
    [SerializeField]
    PlayerComponents player;
    [SerializeField] LayerMask groundLayer;
    [SerializeField]
    #endregion


    #region Base Methods


    void Update()
    {
        GroundCheck();
        GroundedGracePeriod();
        CastingNoMoveTimer();
        CastingMoveTimer();
        Land();
    }

    #endregion


    #region Ground Detection
    void GroundCheck()
    {

        RaycastHit hit;
        Vector3 dir = new Vector3(0, -1);


        if (Physics.SphereCast(transform.position, groundRaySphereRadius, dir, out hit, groundRayRange, groundLayer)) //Physics.Raycast(transform.position, dir, out hit, groundRayRange)
        {
            grounded = true;
            groundedGracePeriodActive = false;
        }
        else
        {
            landed = false;
            grounded = false;
        }


        if (!grounded && !groundedGracePeriodActive)
        {
            groundedGracePeriodActive = true;
            groundedGracePeriodTimer = groundedGracePeriodDuration;
        }
    }

    //Timer that allows player to jump if he fell of the platform and pressed the button a moment too late
    void GroundedGracePeriod()
    {
        if (groundedGracePeriodActive)
        {
            if (groundedGracePeriodTimer > 0) groundedGracePeriodTimer -= Time.deltaTime;
            else
            {
                groundedGracePeriodTimer = 0;
            }
        }
    }

    void Land()
    {
        bool fatalLanding = false;
        if (player.rb.velocity.y < -dangerousVelocity) fatalLanding = true;

        if (!landed && grounded)
        {

            landed = true;
            player.jump.DisableDoubleJump();
            player.PlaySound(1, landSounds[landSoundsQueue]);
            landSoundsQueue++;
            if (landSoundsQueue >= landSounds.Capacity) landSoundsQueue = 0;
            if (fatalLanding) EventManager.PlayerDied();
        }
    }

    #endregion


    #region Casting
    public void BeginCasting(int move, float duration)
    {
        if (move == 0) castingNTimer = duration;
        else castingMTimer = duration;

    }
    void CastingNoMoveTimer()
    {
        if (castingNTimer > 0) castingNTimer -= Time.deltaTime;
        else castingNTimer = 0;
    }

    void CastingMoveTimer()
    {
        if (castingMTimer > 0) castingMTimer -= Time.deltaTime;
        else castingMTimer = 0;
    }


    public bool ActionPossibleN()
    {
        if (castingNTimer <= 0) return true;
        else return false;
    }

    public bool ActionPossibleM()
    {
        if (castingMTimer <= 0) return true;
        else return false;
    }

    public bool IsGrounded()
    {
        if (grounded) return true;
        else return false;
    }
    #endregion



}

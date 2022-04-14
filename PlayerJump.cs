using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    #region Variables

    [Header("Settings")]    /********/
    [SerializeField]
    [Range(1f, 100f)]
    float lowJumpForce = 1.07f;
    [SerializeField]
    [Range(0f, 1f)]
    float jumpGracePeriodDuration = 0.265f;
    [SerializeField]
    [Range(0f, 1f)]
    float jumpCooldownDuration = 0.368f;
    [SerializeField]
    [Range(0f, 20f)]
    float highJumpForce = 0.38f;
    [SerializeField]
    [Range(0f, 1f)]
    float highJumpMaxDuration = 0.14f;
    [SerializeField]
    [Range(1f, 100f)]
    float doubleJumpForce = 3f;
    [SerializeField]
    [Range(0f, 1f)]
    float doubleJumpCastingDuration = 0f;
    [SerializeField]
    List<AudioClip> jumpSounds = new List<AudioClip>();




    [Header("Data")]    /********/
    [SerializeField]
    [Range(0.05f, 2f)]
    float highJumpMaxTimer = 0;
    bool buttonHeld = false;
    [SerializeField]
    public float jumpGracePeriodTimer;
    bool jumpTrigger = false;
    [SerializeField]
    float jumpCooldownTimer;
    bool doubleJumpAvailable = false;
    int jumpSoundsQueue;

    [Header("Components")]    /********/
    [SerializeField]
    PlayerComponents player;

    #endregion


    #region Base Methods

    void Update()
    {
        JumpCooldownTimer();
        HighJumpTimer();
        JumpGracePeriod();
        LowJump();

    }

    void FixedUpdate()
    {
        HighJumpForce();
    }
    #endregion


    #region Unique Methods

    public void ButtonDown()
    {
        if (!player.status.grounded)
        {
            if (!doubleJumpAvailable) jumpGracePeriodTimer = jumpGracePeriodDuration;
            else DoubleJump();
        }
        else jumpTrigger = true;
        buttonHeld = true;
    }



    public void ButtonUp()
    {
        highJumpMaxTimer = 0f;
        buttonHeld = false;
    }

    void LowJump()
    {
        bool grounded = (player.status.grounded || player.status.groundedGracePeriodTimer > 0);
        bool jumpAvailable = jumpTrigger || (jumpGracePeriodTimer > 0 && jumpCooldownTimer <= 0);
        if ((player.status.ActionPossibleM() && jumpAvailable) && grounded)
        {
            jumpCooldownTimer = jumpCooldownDuration;
            doubleJumpAvailable = true;
            jumpTrigger = false;
            player.rb.velocity = new Vector3(player.rb.velocity.x, lowJumpForce, player.rb.velocity.z);
            highJumpMaxTimer = highJumpMaxDuration;
            EventManager.JumpPerformed();
            player.PlaySound(0, jumpSounds[jumpSoundsQueue]);
            jumpSoundsQueue++;
            if (jumpSoundsQueue >= jumpSounds.Capacity) jumpSoundsQueue = 0;
        }
    }

    void HighJumpTimer()
    {
        if (buttonHeld)
        {
            if (highJumpMaxTimer > 0) highJumpMaxTimer -= Time.deltaTime;
            else highJumpMaxTimer = 0f;
        }
    }

    void HighJumpForce()
    {
        if (highJumpMaxTimer > 0 && buttonHeld)
        {
            player.rb.AddForce(new Vector3(0f, highJumpForce, 0), ForceMode.Impulse);
        }
    }

    void JumpGracePeriod()
    {
        if (jumpGracePeriodTimer > 0) jumpGracePeriodTimer -= Time.deltaTime;
        else jumpGracePeriodTimer = 0;
    }

    void JumpCooldownTimer()
    {
        if (jumpCooldownTimer > 0) jumpCooldownTimer -= Time.deltaTime;
        else jumpCooldownTimer = 0;
    }


    void DoubleJump()
    {
        if ((player.status.ActionPossibleM()) && player.rb.velocity.y >= -9f)
        {
            player.status.BeginCasting(0, doubleJumpCastingDuration);
            doubleJumpAvailable = false;
            highJumpMaxTimer = 0f;
            player.rb.velocity = new Vector3(player.rb.velocity.x, doubleJumpForce, player.rb.velocity.z);
            EventManager.JumpPerformed();
            player.PlaySound(0, jumpSounds[jumpSoundsQueue]);
            jumpSoundsQueue++;
            if (jumpSoundsQueue >= jumpSounds.Capacity) jumpSoundsQueue = 0;

        }

    }

    public void DisableDoubleJump()
    {
        doubleJumpAvailable = false;
    }

}



#endregion


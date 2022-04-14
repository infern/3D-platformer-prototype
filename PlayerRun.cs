using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    #region Variables


    [Header("Settings")]    /********/
    [SerializeField]
    [Range(1f, 90f)]
    float runSpeed = 6.5f;
    [SerializeField]
    [Range(0.1f, 6f)]
    float lookSpeed = 3;
    [SerializeField]
    [Range(0f, 1f)]
    float slowDuringJump = 0.7f;

    [Header("Data")]    /********/
    public Vector2 direction;
    public bool walking = false;
    Vector2 rotation = Vector2.zero;
    bool lookingEnabled = true;

    [Header("Components")]    /********/
    [SerializeField]
    PlayerComponents player;


    #endregion


    #region Base Methods

    void OnEnable()
    {
        EventManager.PauseGameTrigger += DisableLook;
        EventManager.FloorPanelTrigger += DisableLook;

    }

    void OnDisable()
    {
        EventManager.PauseGameTrigger -= DisableLook;
        EventManager.FloorPanelTrigger -= DisableLook;
    }

    void Update()
    {
        Look();
    }

    void FixedUpdate()
    {
        Run();
    }

    #endregion

    #region Unique Methods

    public void Run()
    {
        if (direction != Vector2.zero)
        {
            float alterSpeed = !player.status.IsGrounded() ? slowDuringJump : 1f;
            Vector2 move = direction * runSpeed * alterSpeed;
            Vector2 forward = new Vector3(transform.forward.x * direction.x, 0, transform.forward.z * direction.y) * runSpeed;
            player.rb.AddRelativeForce(new Vector3(move.x, 0f, move.y), ForceMode.Impulse);
            walking = true;
        }
        else walking = false;
    }

    public void Look()
    {
        if (lookingEnabled)
        {
            rotation.y += Input.GetAxis("Mouse X");
            rotation.x += -Input.GetAxis("Mouse Y");
            rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
            transform.eulerAngles = new Vector2(0, rotation.y) * lookSpeed;
            Camera.main.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
        }
    }

    void DisableLook()
    {
        lookingEnabled = !lookingEnabled;
    }
    #endregion
}

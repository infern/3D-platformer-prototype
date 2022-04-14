using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.InputSystem.InputAction;


public class ReadPlayerInput : MonoBehaviour
{
    #region Variables
    [Header("Components")]    /********/
    [SerializeField]
    PlayerComponents player;


    private InputMap keyMap;

    #endregion

    #region Base Methods
    void Awake()
    {
        keyMap = new InputMap();

    }
    void OnEnable()
    {
        keyMap.Enable();
    }
    void OnDisable()
    {
        keyMap.Disable();
    }


    void Update()
    {


    }
    #endregion



    #region Action Inputs

    public void RunContext(CallbackContext context)
    {
        player.run.direction = context.ReadValue<Vector2>();
    }



    public void JumpContext(CallbackContext context)
    {
        if (context.started) player.jump.ButtonDown();
        else if (context.canceled) player.jump.ButtonUp();
    }

    public void ShootContext(CallbackContext context)
    {
        if (context.started) player.shoot.ButtonDown();
        else if (context.canceled) player.shoot.ButtonUp();
    }

    public void PauseContext(CallbackContext context)
    {
       if (context.started) EventManager.PauseToggle();

    }


    #endregion

}

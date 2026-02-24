 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSubscription : MonoBehaviour
{
    //Set up read variables here.
    //these names will be used to access inputs in your scripts.
    // _Input.MovementInput.x; _Input.MenuInput; etc.

    //all must be public
    public Vector2 NormalizedMovementInput { get; private set; } = Vector2.zero;
    public Vector2 AnalogMovementInput { get; private set; } = Vector2.zero;
    public Vector2 MouseDeltaInput { get; private set; } = Vector2.zero;
    public bool ActionInput1 { get; private set; } = false;
    public bool ActionInput2 { get; private set; } = false;
    public bool ActionInput3 { get; private set; } = false;
    public bool ActionInput4 { get; private set; } = false;
    public bool SpaceInput { get; private set; } = false;
    public bool ShiftInput { get; private set; } = false;
    public bool EInput { get; private set; } = false;
    public bool QInput { get; private set; } = false;
    public bool MenuInput { get; private set; } = false;
    public bool ConfirmInput { get; private set; } = false;

    GameInpuMap Input = null;



    private void OnEnable() // Subscription
    {
        Input = new GameInpuMap();

        //get your input map branch -> Input. >>{ ActionMapName(AAA_Game_InputMap) }<<

        Input.MainInputMap.Enable();

        //Make sure all input calls have your ActionMapName
        //make sure all inputs have their CallBack ctv event assingned =>   +=/-= SetAction

        Input.MainInputMap.MovementBinds.performed += SetMoveNormal;
        Input.MainInputMap.MovementBinds.canceled += SetMoveNormal;

        Input.MainInputMap.AnalogMovementBinds.performed += SetMoveAnalog;
        Input.MainInputMap.AnalogMovementBinds.canceled += SetMoveAnalog;

        Input.MainInputMap.MouseAimBind.performed += SetMouseDelta;
        Input.MainInputMap.MouseAimBind.canceled += SetMouseDelta;

        Input.MainInputMap.ActionBind1.started += SetAction1;
        Input.MainInputMap.ActionBind1.canceled += SetAction1;

        Input.MainInputMap.ActionBind2.started += SetAction2;
        Input.MainInputMap.ActionBind2.canceled += SetAction2;

        Input.MainInputMap.ActionBind3.started += SetAction3;
        Input.MainInputMap.ActionBind3.canceled += SetAction3;

        Input.MainInputMap.ActionBind4.started += SetAction4;
        Input.MainInputMap.ActionBind4.canceled += SetAction4;

        Input.MainInputMap.SpaceInput.started += SetSpace;
        Input.MainInputMap.SpaceInput.canceled += SetSpace;

        Input.MainInputMap.ShiftInput.started += SetShift;
        Input.MainInputMap.ShiftInput.canceled += SetShift;

        Input.MainInputMap.EInput.started += SetE;
        Input.MainInputMap.EInput.canceled += SetE;

        Input.MainInputMap.QInput.started += SetQ;
        Input.MainInputMap.QInput.canceled += SetQ;
    }

    private void OnDisable() // UNsubscription
    {
        Input.MainInputMap.MovementBinds.performed -= SetMoveNormal;
        Input.MainInputMap.MovementBinds.canceled -= SetMoveNormal;
        
        Input.MainInputMap.AnalogMovementBinds.performed -= SetMoveAnalog;
        Input.MainInputMap.AnalogMovementBinds.canceled -= SetMoveAnalog;

        Input.MainInputMap.MouseAimBind.performed -= SetMouseDelta;
        Input.MainInputMap.MouseAimBind.canceled -= SetMouseDelta;

        Input.MainInputMap.ActionBind1.started -= SetAction1;
        Input.MainInputMap.ActionBind1.canceled -= SetAction1;

        Input.MainInputMap.ActionBind2.started -= SetAction2;
        Input.MainInputMap.ActionBind2.canceled -= SetAction2;

        Input.MainInputMap.ActionBind3.started -= SetAction3;
        Input.MainInputMap.ActionBind3.canceled -= SetAction3;

        Input.MainInputMap.ActionBind4.started -= SetAction4;
        Input.MainInputMap.ActionBind4.canceled -= SetAction4;

        Input.MainInputMap.SpaceInput.started -= SetSpace;
        Input.MainInputMap.SpaceInput.canceled -= SetSpace;

        Input.MainInputMap.ShiftInput.started -= SetShift;
        Input.MainInputMap.ShiftInput.canceled -= SetShift;

        Input.MainInputMap.EInput.started -= SetE;
        Input.MainInputMap.EInput.canceled -= SetE;

        Input.MainInputMap.QInput.started -= SetQ;
        Input.MainInputMap.QInput.canceled -= SetQ;



        Input.MainInputMap.Disable();
    }

    private void Update() 
    {

        // SINGLE CALL BUTTONS - button triggers once on press even if it's called in Update function.
        MenuInput = Input.MainInputMap.MenuBinds.WasPressedThisFrame();
        ConfirmInput = Input.MainInputMap.ConfirmBinds.WasPressedThisFrame();

       
    }

    private void SetMoveNormal(InputAction.CallbackContext ctx)
    {
        NormalizedMovementInput = ctx.ReadValue<Vector2>();
    }
    private void SetMoveAnalog(InputAction.CallbackContext ctx)
    {
        AnalogMovementInput = ctx.ReadValue<Vector2>();
    }

    private void SetMouseDelta(InputAction.CallbackContext ctx)
    {
        MouseDeltaInput = ctx.ReadValue<Vector2>();
    }

    private void SetAction1(InputAction.CallbackContext ctx)
    {
        ActionInput1 = ctx.started;
    }

    private void SetAction2(InputAction.CallbackContext ctx)
    {
        ActionInput2 = ctx.started;
    }
    private void SetAction3(InputAction.CallbackContext ctx)
    {
        ActionInput3 = ctx.started;
    }
    private void SetAction4(InputAction.CallbackContext ctx)
    {
        ActionInput4 = ctx.started;
    }

    private void SetSpace(InputAction.CallbackContext ctx)
    {
        SpaceInput = ctx.started;
    }

    private void SetShift(InputAction.CallbackContext ctx)
    {
        ShiftInput = ctx.started;
    }

    private void SetE(InputAction.CallbackContext ctx)
    {
        EInput = ctx.started;
    }

    private void SetQ(InputAction.CallbackContext ctx)
    {
        QInput = ctx.started;
    }

}

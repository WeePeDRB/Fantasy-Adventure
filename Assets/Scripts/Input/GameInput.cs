using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    //
    public class HandleWeaponMovementEventArgs : EventArgs
    {
        public string weaponMovementEventArgs;
    }
    //
    private InputManager inputManager;

    //Event for the skill
    public event EventHandler OnDashAction;     //For the dash skill
    public event EventHandler OnSpecialAction;  //For the speacial skill
    public event EventHandler OnUltimateAction; //For the ultimate skill
    public event EventHandler<HandleWeaponMovementEventArgs> OnHandleWeaponMovement; //For the weapon movement

    private void Awake()
    {
        inputManager = new InputManager();
        inputManager.Player.Enable();

        //Assign function for event
        inputManager.Player.DashSkill.performed +=  DashSkill_Performed;        
        inputManager.Player.SpecialSkill.performed += SpecialSkill_Performed;   
        inputManager.Player.UltimateSkill.performed += UltimateSkill_Performed;
        inputManager.Player.HandleWeapon.performed += HandleWeaponMovement;
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = inputManager.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }

    //Functions for the performed input action
    private void DashSkill_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnDashAction?.Invoke(this,EventArgs.Empty);
    }

    private void SpecialSkill_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSpecialAction?.Invoke(this,EventArgs.Empty);
    }

    private void UltimateSkill_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnUltimateAction?.Invoke(this,EventArgs.Empty);
    }

    public void HandleWeaponMovement(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnHandleWeaponMovement?.Invoke(this, new HandleWeaponMovementEventArgs {weaponMovementEventArgs = obj.control.name});
    }
}

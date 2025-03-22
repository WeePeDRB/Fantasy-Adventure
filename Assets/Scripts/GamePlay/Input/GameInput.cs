using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    
    // A custom eventargs for the handle weapon movement event 
    public class HandleWeaponMovementEventArgs : EventArgs
    {
        public string weaponMovementEventArgs;
    }


    // Refernce to the input actions assets
    private static InputManager inputManager;   // Input actions reference

    // Event for the skill
    public static event EventHandler OnDashAction;     //For the dash skill
    public static event EventHandler OnSpecialAction;  //For the speacial skill
    public static event EventHandler OnUltimateAction; //For the ultimate skill
    public static event EventHandler<HandleWeaponMovementEventArgs> OnHandleWeaponMovement; //For the weapon movement


    // Read, normalized and return the  value from player input  
    public static Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = inputManager.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }


    // Handle the performed event in the input actions
    private void DashSkill_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnDashAction?.Invoke(this,EventArgs.Empty);
    }


    // Handle the performed event in the input actions
    private void SpecialSkill_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSpecialAction?.Invoke(this,EventArgs.Empty);
    }


    // Handle the performed event in the input actions
    private void UltimateSkill_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnUltimateAction?.Invoke(this,EventArgs.Empty);
    }


    // Handle the performed event in the input actions
    public void HandleWeaponMovement(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnHandleWeaponMovement?.Invoke(this, new HandleWeaponMovementEventArgs {weaponMovementEventArgs = obj.control.name});
    }


    //
    //
    //
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
}

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

    // Events for the skill
    public static event Action OnDashAction;     //For the dash skill
    public static event Action OnSpecialAction;  //For the speacial skill
    public static event Action OnUltimateAction; //For the ultimate skill

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
        OnDashAction?.Invoke();
    }


    // Handle the performed event in the input actions
    private void SpecialSkill_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSpecialAction?.Invoke();
    }


    // Handle the performed event in the input actions
    private void UltimateSkill_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnUltimateAction?.Invoke();
    }

    //
    //
    //
    private void Awake()
    {
        inputManager = new InputManager();
        inputManager.Player.Enable();

        // Assign function for event
        inputManager.Player.DashSkill.performed +=  DashSkill_Performed;        
        inputManager.Player.SpecialSkill.performed += SpecialSkill_Performed;   
        inputManager.Player.UltimateSkill.performed += UltimateSkill_Performed;
    }
}

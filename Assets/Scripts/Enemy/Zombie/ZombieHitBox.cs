using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitBox : MonoBehaviour
{
    //
    //
    private bool isPlayerInside = false;  // Check if player is still inside the hit box
    private Coroutine stayCoroutine;  // Coroutine checking for perforamance optimazation
    private float coroutineInterval = 0.5f;  



    //
    //  Events for the zombie attack funtion
    //
    public event Action OnEnterZombieHitBox;  // This event occur when Player or the Player Shield enter the zombie hit box
    public event Action OnStayZombieHitBox;  // This event occur when Player or Player Shield stay in the zombie hit box 
    public event Action OnExitZombieHitBox;  // This event occur when Player or the Player Shield exit the zombie hit box
    public event Action OnPlayerTakeDamage;  // This event occur when Player or Player Shield get hit

    //
    //  Summary:
    //      Check the trigger enter and fire the event if its match the condition
    //
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") || collider.gameObject.CompareTag("PlayerShield") )
        {
            Debug.Log("Enter hit box check");
            isPlayerInside = true;
            OnEnterZombieHitBox?.Invoke();

            // Check if the coroutine is null 
            if (stayCoroutine == null)
            {
                // Start the new coroutine
                stayCoroutine = StartCoroutine(StayRoutine());
            }
        }
    }




    //
    //  Summary:
    //      Check the trigger exit and fire the event if its match the condition
    //
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") || collider.gameObject.CompareTag("PlayerShield") )
        {
            Debug.Log("Exit hit box check");
            isPlayerInside = false;
            OnExitZombieHitBox?.Invoke();
        }
    }


    //
    //  Summary:
    //      
    //      
    private IEnumerator StayRoutine()
    {
        while (isPlayerInside)
        {
            yield return new WaitForSeconds(coroutineInterval);
            OnStayZombieHitBox?.Invoke();
            Debug.Log("Stay hit box check !");
        }
        stayCoroutine = null; // Reset the coroutine when player exit the hit box
    }


    //
    //  Summary:
    //      Trigger envent when player is inside the hit box
    //
    public void PlayerTakeDamage()
    {
        if (isPlayerInside)
        {
            OnPlayerTakeDamage?.Invoke();
        }
    }
}

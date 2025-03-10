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
    private float coroutineInterval = 0.1f;  // 



    //
    //  Events for the zombie attack funtion
    //
    public event Action OnPlayerInZombieHitBox;  // This event occur when Player or the Player Shield is in the zombie hit box
    public event Action OnPlayerExitZombieHitBox;  // This event occur when Player or the Player Shield exit the zombie hit box


    //
    //
    private void Update()
    {
        InvokeEventControl();
    }


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
        }
    }


    //
    //  Summary:
    //      Control the event due to the "isPlayerInside" 
    //      This function will be update in every frame
    private void InvokeEventControl()
    {
        // Check if player is inside the hit box
        if (isPlayerInside)
        {
            // Check if the coroutine 
            if (stayCoroutine == null)
            {
                // Start the new coroutine
                stayCoroutine = StartCoroutine(StayRoutine());
            }
        }
        else
        {
            StopCoroutine(stayCoroutine);
            stayCoroutine = null;
            OnPlayerExitZombieHitBox?.Invoke();
        }
    }


    //
    //  Summary:
    //      Coroutine to invoke the event
    //      
    private IEnumerator StayRoutine()
    {
        yield return new WaitForSeconds(coroutineInterval);
        OnPlayerInZombieHitBox?.Invoke();
        Debug.Log("Stay hit box check !");
        stayCoroutine = null; // Set the coroutine to null when the event invoke
    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitBox : MonoBehaviour
{
    //
    //  Events for the zombie attack funtion
    //
    public event Action OnEnterZombieHitBox;    //  This event will fire when Player or the Player Shield enter the zombie hit box

    public event Action OnExitZombieHitBox; //  This event will fire when Player or the Player Shield exit the zombie hit box

    //
    //  Summary:
    //      Check the trigger enter and fire the event if its match the condition
    //
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") || collider.gameObject.CompareTag("PlayerShield") )
        {
            Debug.Log("Enemy hit box hit player !");
            OnEnterZombieHitBox?.Invoke();
        }
    }

    //
    //  Summary:
    //      Check the trigger stay and fire the event if its match the condition
    //
    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") || collider.gameObject.CompareTag("PlayerShield") )
        {
            Debug.Log("Enemy hit box hit player on stay triggerr !");
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
            Debug.Log("Player out of enemy reach !");
            OnExitZombieHitBox?.Invoke();
        }
    }
}

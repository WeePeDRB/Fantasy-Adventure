using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitBox : MonoBehaviour
{
    //Events for the zombie attack funtion

    //This event will fire when Player or the Player Shield enter the zombie hit box
    public event Action OnEnterZombieHitBox;

    //This event will fire when Player or the Player Shield exit the zombie hit box
    public event Action OnExitZombieHitBox; 


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") || collider.gameObject.CompareTag("PlayerShield") )
        {
            Debug.Log("Enemy hit box hit player !");
            OnEnterZombieHitBox?.Invoke();
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") || collider.gameObject.CompareTag("PlayerShield") )
        {
            Debug.Log("Enemy hit box hit player on stay triggerr !");
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") || collider.gameObject.CompareTag("PlayerShield") )
        {
            Debug.Log("Player out of enemy reach !");
            OnExitZombieHitBox?.Invoke();
        }
    }
}

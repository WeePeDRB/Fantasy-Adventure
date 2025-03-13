using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseHitBox : MonoBehaviour
{
    // Events for enemy behavior
    
    //
    // Event occurs when player enter the attack range, all the logic
    // related to the attack function will listen to this event
    //
    public event Action OnPlayerEnterEnemyAttackRange;

    //
    // Event occurs when player exit the attack range, all the logic 
    // related to the attack function will listen to this event
    //
    public event Action OnPlayerExitEnemyAttackRange;


    //
    // Check if player enter the enemy attack range
    //
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            OnPlayerEnterEnemyAttackRange?.Invoke();
        }
    }


    //
    // Check if player exit the enem attack range
    //
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            OnPlayerExitEnemyAttackRange?.Invoke();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBaseHitBox : MonoBehaviour
{
    // Events for monster behavior
    
    // Event occurs when player enter the attack range, all the logic
    // related to the attack function will listen to this event
    public event Action OnPlayerEnterMonsterAttackRange;

    // Event occurs when player exit the attack range, all the logic 
    // related to the attack function will listen to this event
    public event Action OnPlayerExitMonsterAttackRange;


    // Check if player enter the monster attack range
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            OnPlayerEnterMonsterAttackRange?.Invoke();
        }
    }


    // Check if player exit the enem attack range
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            OnPlayerExitMonsterAttackRange?.Invoke();
        }
    }
}

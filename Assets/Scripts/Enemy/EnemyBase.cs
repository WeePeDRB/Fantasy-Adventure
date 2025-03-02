using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    //
    //  Summary:     
    //      Enemy basic stats
    //
    protected float maxHealth;
    protected float health;
    protected float speed;
    protected float attackSpeed;
          
    //
    protected bool isReady; //Ready to move
    protected bool isReadyToAttack; //Ready to attack
    
    //
    //  Summary: 
    //      Return the isReady bool to true
    //
    public void EnemyReady()
    {
        isReady = true;
    }


    //
    //  Summary: 
    //      This function will check the position of player and move to there
    //
    protected virtual void HandleMovement()
    {
        //Specify direction
        Vector3 direction = (CharacterBase.Instance.transform.position - this.transform.position).normalized;
        Vector3 moveDirVector = new Vector3(direction.x, 0, direction.z);
        //Movement
        transform.position += moveDirVector * speed * Time.deltaTime;

        //Rotation
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirVector, Time.deltaTime * rotateSpeed);
    }

    //
    //  Summary:
    //      Instantiate the stats for enemy
    //
    protected abstract void InstantiateCharacter();

 
 
    
    //  ATTACK FUNTIONs
    
    //
    //  Summary: 
    //      This function is used to check if player is in the attack range of monster
    //
    protected abstract void IsReadyToAttack();
    
    //
    //  Summary: 
    //      This is the monster attack function 
    //
    protected abstract void Attack();

    //
}

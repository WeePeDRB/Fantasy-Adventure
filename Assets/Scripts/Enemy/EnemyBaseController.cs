using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseController : MonoBehaviour
{
    //  
    // Enemy basic stats
    //
    protected float maxHealth;
    protected float health;
    protected float speed;
    protected float attackSpeed;
    
    //
    // 
    //
    protected bool isPlayerInside;
    protected bool isPlayerMoving;
    public bool IsPlayerMoving
    {
        get { return isPlayerMoving; }
    }


    //
    // References  
    //
    protected EnemyBaseHitBox enemyBaseHitBox;


    //
    // Instantiate the stats for enemy
    //
    protected virtual void InstantiateCharacter(float instantiateMaxHealth, float instantiateSpeed, float instantiateAttackSpeed)
    {
        //
        isPlayerInside = false;
        
        //
        enemyBaseHitBox = GetComponentInChildren<ZombieHitBox>();

        //
        enemyBaseHitBox.OnPlayerEnterEnemyAttackRange += Attack;
        enemyBaseHitBox.OnPlayerExitEnemyAttackRange += IsOutOfRange;

        //
        maxHealth = instantiateMaxHealth;
        health = maxHealth;
        speed = instantiateSpeed;
        attackSpeed = instantiateAttackSpeed;

    }


    //
    // This function will check the position of player and move to there
    //
    protected virtual void HandleMovement()
    {
        if (isPlayerInside == false)
        {
            //Specify direction
            Vector3 direction = (CharacterBaseController.Instance.transform.position - this.transform.position).normalized;
            Vector3 moveDirVector = new Vector3(direction.x, 0, direction.z);
            //Movement
            transform.position += moveDirVector * speed * Time.deltaTime;

            //Rotation
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirVector, Time.deltaTime * rotateSpeed);

            isPlayerMoving = true;
        }
    }

    
    //
    // 
    //
    protected virtual void Attack()
    {
        isPlayerInside = true;
        isPlayerMoving = false;
    }
    
    
    //
    // Check if player is out of attack range
    //
    protected virtual void IsOutOfRange()
    {
        isPlayerInside = false;
        isPlayerMoving = true;
    }


    //
    //  
    //      
    protected virtual void Hurt()
    {

    }

    
    //
    //
    //
    protected virtual void Dead()
    {

    }
}

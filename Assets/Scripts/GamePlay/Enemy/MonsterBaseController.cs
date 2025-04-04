using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBaseController : MonoBehaviour
{ 
    //
    // FIELDS
    //

    // MONSTER STATS
    
    protected MonsterStats monsterStats;



    // MONSTER EFFECT STATUS
    // EffectStatus 
    protected EffectStatus effectStatus; // Effect manager



    // CHECKING FLAGS
    // Player position flags
    protected bool isPlayerInside; // Check if player is inside monster hit box
    protected bool isPlayerMoving;

    // References  
    protected MonsterBaseHitBox monsterBaseHitBox;



    //
    // PROPERTIES
    //

    // Flags
    public bool IsPlayerMoving
    {
        get { return isPlayerMoving; }
    }



    //
    // FUNCTIONS
    //

    // INITIAL VALUES FOR MONSTER
    // Monster stats
    public virtual void InstantiateCharacter()
    {
        monsterBaseHitBox = GetComponentInChildren<ZombieHitBox>();

        //
        monsterBaseHitBox.OnPlayerEnterMonsterAttackRange += Attack;
        monsterBaseHitBox.OnPlayerExitMonsterAttackRange += IsOutOfRange;

    }



    // SET UP COMMON FUNCTIONS FOR CHARACTER
    // Monster movement function
    protected virtual void HandleMovement()
    {
        if (isPlayerInside == false)
        {
            //Specify direction
            Vector3 direction = (CharacterBaseController.Instance.transform.position - this.transform.position).normalized;
            Vector3 moveDirVector = new Vector3(direction.x, 0, direction.z);
            //Movement
            transform.position += moveDirVector * monsterStats.Speed * Time.deltaTime;

            //Rotation
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirVector, Time.deltaTime * rotateSpeed);

            isPlayerMoving = true;
        }
    }

    
    // Monster attack function
    protected virtual void Attack()
    {
        isPlayerInside = true;
        isPlayerMoving = false;
    }
    
    
    // Check if player is out of attack range
    protected virtual void IsOutOfRange()
    {
        isPlayerInside = false;
        isPlayerMoving = true;
    }

    // Monster hurt function
    protected virtual void Hurt()
    {

    }

    // Monster dead function
    protected virtual void Dead()
    {

    }

    // SUPPORT FUNCTIONS
    // Access status effect
    public void ReceiveSpecialEffect(SpecialEffectBase specialEffect)
    {
        effectStatus.ReceiveEffect(specialEffect);
    }
}

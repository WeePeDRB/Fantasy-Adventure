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
    // Basic stats
    protected float maxHealth; // Maximum health;
    protected float health; // Current health
    protected float speed; // Movement speed
    protected float attackSpeed; // Attack speed
    
    // Special stats
    protected float resistance; // This stat will block a percentage of the damage received by the player, with a value ranging from 1 to 100.
    protected float abilityHaste; // This stat represents the percentage of time reduced for skill cooldowns.
    protected float damageAmplifier; // This stat increases the damage dealt by weapons.



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
    public virtual void InstantiateCharacter(float instantiateMaxHealth, float instantiateSpeed, float instantiateAttackSpeed)
    {
        monsterBaseHitBox = GetComponentInChildren<ZombieHitBox>();

        //
        monsterBaseHitBox.OnPlayerEnterMonsterAttackRange += Attack;
        monsterBaseHitBox.OnPlayerExitMonsterAttackRange += IsOutOfRange;

        //
        maxHealth = instantiateMaxHealth;
        health = maxHealth;
        speed = instantiateSpeed;
        attackSpeed = instantiateAttackSpeed;

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
            transform.position += moveDirVector * speed * Time.deltaTime;

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



    // MODIFY CHARACTER STATS
    // Modify health
    public void ModifyHealth(float healthValue)
    {
        health += healthValue;
    }

    // Modify speed
    public void ModifySpeed(float speedValue)
    {
        speed += speedValue;
    }

    // Modify resistance
    public void ModifyResistance(float resistanceValue)
    {
        resistance += resistanceValue;
    }

    // Modify ability haste
    public void ModifyAbilityHaste(float abilityHasteValue)
    {
        abilityHaste += abilityHasteValue;
    }

    // Modify damage
    public void ModifyDamage(float damageAmplifierValue)
    {
        damageAmplifier += damageAmplifierValue;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : EnemyBase
{
    //
    //
    [SerializeField] private ZombieHitBox zombieHitBox;

    private void Start()
    {
        InstantiateCharacter();

    }


    private void Update()
    {
        if (isReadyToMove)
        {
            HandleMovement();
        }
    }


    //
    //  Override funciton
    //

    protected override void InstantiateCharacter()
    {
        // Set up original stats
        maxHealth = 100f;
        health = maxHealth;
        speed = 1.3f;
        attackSpeed = 2f;
        isReadyToMove = false;
        
        //
    }


    protected override void IsReadyToAttack()
    {
        //
        //  Check special effect 
        //


        isReadyToAttack = true;
        Attack();
    }

    protected override void Attack()
    {
        if (isReadyToAttack)
        {
            Debug.Log("Enemy Attacks!");
            isReadyToAttack = false;    
        }
    }

    protected override void IsOutOfRange()
    {
        isReadyToAttack = false;    
    }
}

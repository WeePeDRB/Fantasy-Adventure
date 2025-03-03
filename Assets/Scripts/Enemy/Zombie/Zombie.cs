using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : EnemyBase
{
    //
    //  
    //
    [SerializeField] private ZombieHitBox zombieHitBox; //  Reference wto the zombie hit box


    //
    //
    //
    private void Start()
    {
        InstantiateCharacter();
        zombieHitBox.OnEnterZombieHitBox += IsReadyToAttack;
    }

    private void Update()
    {
        if (isReady)
        {
            HandleMovement();
        }
    }

    //
    //  Override funciton
    //
    
    protected override void InstantiateCharacter()
    {
        maxHealth = 100f;
        health = maxHealth;
        speed = 1.3f;
        attackSpeed = 2f;
        isReady = false;
    }


    protected override void IsReadyToAttack()
    {
        isReady = false;
        Invoke(nameof(Attack), 1.3f);
    }


    protected override void Attack()
    {
        Debug.Log("The enemy is attack !");
    }
}

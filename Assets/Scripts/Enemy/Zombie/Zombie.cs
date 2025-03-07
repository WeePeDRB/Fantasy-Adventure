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
        zombieHitBox.OnEnterZombieHitBox += IsReadyToAttack;
        zombieHitBox.OnStayZombieHitBox += IsReadyToAttack;
        zombieHitBox.OnExitZombieHitBox += IsOutOfRange;
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
        maxHealth = 100f;
        health = maxHealth;
        speed = 1.3f;
        attackSpeed = 2f;
        isReadyToMove = false;
    }


    protected override void IsReadyToAttack()
    {
        if (isReadyToAttack) return;
        
        isReadyToAttack = true;

        if (instantAttack)
        {
            Debug.Log("Instant attack !");
            Attack();
        }

        else if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(AttackCoroutine());
        }
    }

    protected override void Attack()
    {
        Debug.Log("Enemy Attacks!");
        zombieHitBox.PlayerTakeDamage();
        isReadyToAttack = false;    
    }

    protected override void IsOutOfRange()
    {
        isReadyToAttack = false;
        instantAttack = true;

        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }
}

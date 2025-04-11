using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimator : MonsterBaseAnimator
{
    //
    // FIELDS
    //

    // ANIMATOR
    // References
    protected Animator animator;
    protected MonsterBaseHitBox zombieBaseHitBox;
    protected ZombieController zombieController;

    // Animator parameters
    protected const string IS_MOVING = "IsMoving";
    protected const string ATTACK = "Attack";
    protected const string IS_DEAD_TRIGGER = "DeadTrigger";

    // Flags
    protected bool isDead;

    //
    // FUNCTIONS
    //

   // INITIAL SET UP FOR ANIMATOR
    protected override void InstantiateAnimator()
    {
        animator = GetComponent<Animator>();
        zombieController = GetComponentInParent<ZombieController>();
        zombieBaseHitBox = zombieController.GetComponentInChildren<MonsterBaseHitBox>();
    }

    // HANDLING CHARACTER ANIMATIOn
    // Monster movement
    protected override void Move()
    {
        if ( isDead == false )
        {
            animator.SetBool(IS_MOVING, zombieController.IsMoving);
        }
    }

    // Monster attack
    protected override void Attack()
    {
        if ( isDead == false )
        {
            animator.SetTrigger(ATTACK);
        }
    }
    protected void ControllerAttack()
    {
        zombieController.Attack();
    }

    // Monster dead
    protected override void Dead()
    {
        animator.SetTrigger(IS_DEAD_TRIGGER);
    }


    private void Awake()
    {
        //
        InstantiateAnimator();

        //
        zombieController.OnZombieAttack += Attack;
        zombieController.OnZombieDead += Dead;
    }

    private void Update()
    {
        isDead = zombieController.IsDead;
        Move();
    }
}

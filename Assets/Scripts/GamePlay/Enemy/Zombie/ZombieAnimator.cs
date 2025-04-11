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
    private Animator animator;
    private MonsterBaseHitBox zombieBaseHitBox;
    private ZombieController zombieController;

    // Animator parameters
    private const string IS_MOVING = "IsMoving";
    private const string ATTACK = "Attack";
    private const string IS_DEAD_TRIGGER = "DeadTrigger";

    // Flags
    private bool isDead;

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

    // HANDLING ZOMBIE ANIMATION
    // Zombie movement
    protected override void Move()
    {
        if ( isDead == false )
        {
            animator.SetBool(IS_MOVING, zombieController.IsMoving);
        }
    }

    // Zombie attack
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


    private void Start()
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

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

    // Behavior state
    private MonsterBehavior zombieBehaviorState;

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
        if ( zombieBehaviorState == MonsterBehavior.Move )
        {
            animator.SetBool(IS_MOVING, true);
        }
        else 
        {
            animator.SetBool(IS_MOVING, false);
        }
    }

    // Zombie attack
    protected override void Attack()
    {
        animator.SetTrigger(ATTACK);
    }
    protected void AttackHit()
    {
        zombieController.Attack();
    }
    protected void ResetAttack()
    {
        zombieController.ResetAttack();
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
        zombieController.OnMonsterAttack += Attack;
        zombieController.OnMonsterDead += Dead;
    }

    private void Update()
    {
        zombieBehaviorState = zombieController.MonsterBeHaviorState;
        Move();
    }
}

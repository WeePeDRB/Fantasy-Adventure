using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimator : MonoBehaviour, IMonsterAnimator
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


    //
    // FUNCTIONS
    //

   // INITIAL SET UP FOR ANIMATOR
    public void InstantiateAnimator()
    {
        animator = GetComponent<Animator>();
        zombieController = GetComponentInParent<ZombieController>();
        zombieBaseHitBox = zombieController.GetComponentInChildren<MonsterBaseHitBox>();
    }

    // HANDLING CHARACTER ANIMATIOn
    public void Move()
    {
        animator.SetBool(IS_MOVING, zombieController.IsMoving);
    }

    public void Attack()
    {
        animator.SetTrigger(ATTACK);
    }

    public void Dead()
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
        Move();
    }
}

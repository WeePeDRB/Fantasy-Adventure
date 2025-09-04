using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeMonsterAnimator : MonsterAnimator
{
    // Monster controller 
    protected RangeMonsterController rangeMonsterController;

    // Initialize data
    protected override void InitializeData()
    {
        // Animator 
        animator = GetComponent<Animator>();

        // Monster controller
        rangeMonsterController = GetComponentInParent<RangeMonsterController>();
    }

    // Animation handle
    // Monster movement
    protected override void MoveAnimate()
    {
        if (rangeMonsterController.BehaviorState == MonsterBehaviorState.Moving)
        {
            animator.SetBool(IS_MOVING, true);
        }
        else
        {
            animator.SetBool(IS_MOVING, false);
        }

    }
    // Monster attack
    protected override void AttackAnimate()
    {
        animator.SetTrigger(IS_ATTACKING);
    }
    protected void SpawnProjectile()
    {
        rangeMonsterController.SpawnProjectile();
    }
    protected override void AttackRecover()
    {
        StartCoroutine(rangeMonsterController.AttackRecover());
    }
    // Monster dead
    protected override void DeadAnimate(MonsterDead monsterDead)
    {
        animator.SetTrigger(IS_DEAD);
    }

    protected override void DropItem()
    {
        throw new System.NotImplementedException();
    }

    //
    private void Start()
    {
        // Initialize data
        InitializeData();

        // Susbcribe to controller event
        rangeMonsterController.OnMonsterAttack += AttackAnimate;
        rangeMonsterController.OnMonsterDead += DeadAnimate;
    }
    private void Update()
    {
        MoveAnimate();
    }
}

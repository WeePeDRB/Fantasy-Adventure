using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonsterAnimator : MonsterAnimator
{
    // Monster controller
    protected MeleeMonsterController meleeMonsterController;

    // Initialize data
    protected override void InitializeData()
    {
        // Animator 
        animator = GetComponent<Animator>();

        // Monster controller
        meleeMonsterController = GetComponentInParent<MeleeMonsterController>();
    }

    // Animation handle
    // Monster movement
    protected override void MoveAnimate()
    {
        if (meleeMonsterController.BehaviorState == MonsterBehaviorState.Moving)
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
    protected void ApplyDamage()
    {
        meleeMonsterController.ApplyDamage(null);
    }
    protected override void AttackRecover()
    {
        StartCoroutine(meleeMonsterController.AttackRecover());
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
        meleeMonsterController.OnMonsterAttack += AttackAnimate;
        meleeMonsterController.OnMonsterDead += DeadAnimate;
    }
    private void Update()
    {
        MoveAnimate();
    }
}

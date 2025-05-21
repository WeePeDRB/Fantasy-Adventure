using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBaseAnimator : MonoBehaviour
{
    //
    // FIELDS
    //

    // ANIMATOR
    // References
    protected Animator animator;
    protected MonsterBaseController monsterBaseController;

    // Animator parameters
    protected const string IS_MOVING = "IsMoving";
    protected const string ATTACK = "Attack";
    protected const string IS_DEAD_TRIGGER = "Dead";

    
    // Behavior state
    protected MonsterMovementState monsterMovementState;

    //
    // FUNCTIONS
    //
    
    // INITIAL SET UP FOR ANIMATOR
    protected virtual void InstantiateAnimator()
    {
        animator = GetComponent<Animator>();
        monsterBaseController = GetComponentInParent<MonsterBaseController>();
    }

    // HANDLING MONSTER ANIMATION
    // Monster movement
    protected virtual void Move()
    {
        if ( monsterMovementState == MonsterMovementState.Move )
        {
            animator.SetBool(IS_MOVING, true);
        }
        else 
        {
            animator.SetBool(IS_MOVING, false);
        }
    }

    // Monster attack
    protected virtual void Attack()
    {
        animator.SetTrigger(ATTACK);
    }
    protected virtual void ResetAttack()
    {
        monsterBaseController.ResetAttack();
    }

    // Monster dead
    protected virtual void Dead(object sender, OnMonsterDeadEventArgs monsterDeadEventArgs)
    {
        animator.SetTrigger(IS_DEAD_TRIGGER);
    }
    
    protected virtual void DropItem()
    {
        // Drop item when monster dead
        monsterBaseController.DropCoin();
        monsterBaseController.DropExp();
    }
}

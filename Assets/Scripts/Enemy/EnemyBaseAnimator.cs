using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseAnimator : MonoBehaviour
{
    //
    protected const string IS_ENEMY_MOVING = "IsMoving";

    //
    protected bool isMoving;

    // References
    protected Animator animator;
    protected EnemyBaseHitBox enemyBaseHitBox;
    protected EnemyBaseController enemyBaseController;

    // Instantiate 
    protected virtual void InstantiateAnimator()
    {
        //
        animator = GetComponent<Animator>();
        enemyBaseController = GetComponentInParent<EnemyBaseController>();
        enemyBaseHitBox = enemyBaseController.GetComponentInChildren<EnemyBaseHitBox>();

        //

    }


    //
    protected virtual void Moving()
    {
        animator.SetBool(IS_ENEMY_MOVING, enemyBaseController.IsPlayerMoving);
    }
}

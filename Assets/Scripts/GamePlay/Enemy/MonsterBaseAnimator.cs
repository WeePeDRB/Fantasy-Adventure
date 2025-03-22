using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBaseAnimator : MonoBehaviour
{
    //
    protected const string IS_MONSTER_MOVING = "IsMoving";

    //
    protected bool isMoving;

    // References
    protected Animator animator;
    protected MonsterBaseHitBox monsterBaseHitBox;
    protected MonsterBaseController monsterBaseController;

    // Instantiate 
    protected virtual void InstantiateAnimator()
    {
        //
        animator = GetComponent<Animator>();
        monsterBaseController = GetComponentInParent<MonsterBaseController>();
        monsterBaseHitBox = monsterBaseController.GetComponentInChildren<MonsterBaseHitBox>();

        //

    }


    //
    protected virtual void Moving()
    {
        animator.SetBool(IS_MONSTER_MOVING, monsterBaseController.IsPlayerMoving);
    }
}

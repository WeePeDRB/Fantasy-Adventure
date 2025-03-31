using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBaseAnimator : MonoBehaviour
{
    //
    // FIELDS
    //

    // ANIMATOR
    // References
    protected Animator animator;
    protected MonsterBaseHitBox monsterBaseHitBox;
    protected MonsterBaseController monsterBaseController;

    // Animator parameters
    protected const string IS_MOVING = "IsMoving";
    protected bool isMoving;



    //
    // FUNCTIONS
    //

    // HANDLING CHARACTER ANIMATIOn
    // Control monster movement animation
    protected virtual void Moving()
    {
        animator.SetBool(IS_MOVING, monsterBaseController.IsPlayerMoving);
    }



    //
    private void Awake()
    {
        animator = GetComponent<Animator>();
        monsterBaseController = GetComponentInParent<MonsterBaseController>();
        monsterBaseHitBox = monsterBaseController.GetComponentInChildren<MonsterBaseHitBox>();

    }
}

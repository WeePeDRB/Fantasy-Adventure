using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinAnimator : HeroBaseAnimator
{
    //
    // FIELDS
    //
    
    // ANIMATOR 
    // References
    private Animator animator;
    private PaladinController paladinController;

    // Animator parameters
    private const string IS_MOVING = "IsMoving"; // Parameter name
    private bool isMoving; // Parameter value

    //
    // FUNCTIONS
    //

    // INITIAL SET UP FOR ANIMATOR
    protected override void InstantiateAnimator()
    {
        animator = GetComponent<Animator>();
        paladinController = GetComponentInParent<PaladinController>();
    }

    // HANDLING PALADIN ANIMATION
    // Paladin movement
    protected override void MoveAnimate()
    {
        Vector2 inputVector = GameInput.GetMovementVectorNormalized();
         if(inputVector != Vector2.zero) isMoving = true;
         else isMoving = false;
         animator.SetBool(IS_MOVING, isMoving);
    }

    // Paladin dead
    protected override void DeadAnimate()
    {

    }

    // Paladin dash
    protected override void DashSkillAnimate()
    {
        animator.SetTrigger("Dash");
    }

    // Paladin special
    // This function will handle the animation
    protected override void SpecialSkillAnimate()
    {
        animator.SetTrigger("Special");
    }
    // This function will handle the special skill effect
    protected void SpecialSkillActivate()
    {
        Debug.Log("This is special skill activate in animator");
        paladinController.SpecialSkillActivate();
    }

    //Paladin ultimate
    protected override void UltimateSkillAnimate()
    {
        animator.SetTrigger("Ultimate");
    }

    // SUPPORT FUNCTION
    protected override void ReturnNormalState()
    {
        Debug.Log("return normal state in paladin animator");
        paladinController.ReturnNormalState();
    }

    // Start is called before the first frame update
    void Start()
    {
        //
        InstantiateAnimator();

        // 
        paladinController.OnPaladinDash += DashSkillAnimate;
        paladinController.OnPaladinSpecial += SpecialSkillAnimate;
        paladinController.OnPaladinUltimate += UltimateSkillAnimate;
    }

    // Update is called once per frame
    void Update()
    {
        MoveAnimate();
    }
}



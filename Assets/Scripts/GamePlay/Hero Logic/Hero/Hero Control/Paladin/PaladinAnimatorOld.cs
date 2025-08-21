using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinAnimatorOld : HeroBaseAnimator
{
    //
    // FIELDS
    //
    
    // ANIMATOR 
    // References
    private Animator animator;
    private PaladinControllerOld paladinController;

    private PaladinSpecialSkill paladinSpecialSkill;
    private PaladinUltimateSkill paladinUltimateSkill;

    // Animator parameters
    private const string MOVE = "Move"; 
    private const string DEAD = "Dead";
    private const string DASH = "Dash";
    private const string SPECIAL = "Special";
    private const string ULTIMATE = "Ultimate";

    //
    // FUNCTIONS
    //

    // INITIAL SET UP FOR ANIMATOR
    protected override void InitializeAnimator()
    {
        animator = GetComponent<Animator>();
        paladinController = GetComponentInParent<PaladinControllerOld>();
        paladinSpecialSkill = paladinController.GetComponentInChildren<PaladinSpecialSkill>();
        paladinUltimateSkill = paladinController.GetComponentInChildren<PaladinUltimateSkill>();
    }

    // HANDLING PALADIN ANIMATION
    // Paladin movement
    protected override void MoveAnimate()
    {
        Vector2 inputVector = GameInput.GetMovementVectorNormalized();
        if(inputVector != Vector2.zero) animator.SetBool(MOVE, true);
        else animator.SetBool(MOVE, false);
    }
    // Paladin dead
    protected override void DeadAnimate()
    {
        animator.SetTrigger(DEAD);
    }

    // Paladin dash
    protected override void DashSkillAnimate()
    {
        animator.SetTrigger(DASH);
    }
    // Paladin special
    protected override void SpecialSkillAnimate()
    {
        animator.SetTrigger(SPECIAL);
    }
    private void SpecialSkillActivate()
    {
        paladinSpecialSkill.SkillActivate();    
    }
    //Paladin ultimate
    protected override void UltimateSkillAnimate()
    {
        animator.SetTrigger(ULTIMATE);
    }
    private void UltimateSkillActivate()
    {
        paladinUltimateSkill.SkillActivate();
    }

    // SUPPORT FUNCTION
    protected override void ReturnNormalState()
    {
        paladinController.ReturnNormalState();
    }

    private void Start()
    {
        //
        InitializeAnimator();

        // 
        paladinController.OnHeroDash += DashSkillAnimate;
        paladinController.OnHeroSpecial += SpecialSkillAnimate;
        paladinController.OnHeroUltimate += UltimateSkillAnimate;
        paladinController.OnHeroDead += DeadAnimate;
    }

    private void Update()
    {
        MoveAnimate();
    }
}



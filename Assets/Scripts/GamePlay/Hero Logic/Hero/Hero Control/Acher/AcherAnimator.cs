using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcherAnimatorold : HeroBaseAnimator
{
    //
    // FIELDS
    //

    // ANIMATOR
    // References
    private Animator animator;
    private AcherControllerold acherController;

    private AcherSpecialSkill acherSpecialSkill;
    private AcherUltimateSkill acherUltimateSkill;

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
        acherController = GetComponentInParent<AcherControllerold>();
        acherSpecialSkill = acherController.GetComponentInChildren<AcherSpecialSkill>();
        acherUltimateSkill = acherController.GetComponentInChildren<AcherUltimateSkill>();
    }

    // HANDLING ACHER ANIMATION
    // Acher movement
    protected override void MoveAnimate()
    {
        Vector2 inputVector = GameInput.GetMovementVectorNormalized();
        if(inputVector != Vector2.zero) animator.SetBool(MOVE, true);
        else animator.SetBool(MOVE, false);
    }
    // Acher dead
    protected override void DeadAnimate()
    {
        animator.SetTrigger(DEAD);
    }
    // Acher dash
    protected override void DashSkillAnimate()
    {
        animator.SetTrigger(DASH);
    }
    // Acher special
    protected override void SpecialSkillAnimate()
    {
        animator.SetTrigger(SPECIAL);
    }
    private void SpecialSkillActivate()
    {
        acherSpecialSkill.SkillActivate();
    }
    // Acher ultimate
    protected override void UltimateSkillAnimate()
    {
        animator.SetBool(ULTIMATE, true);
        StartCoroutine(HyperInstinctCountDown());
    }
    private IEnumerator HyperInstinctCountDown()
    {
        yield return new WaitForSeconds(10f);
        animator.SetBool(ULTIMATE, false);
    }
    private void UltimateSkillActivate()
    {
        acherUltimateSkill.SkillActivate();
    }

    // SUPPORT FUNCTION
    protected override void ReturnNormalState()
    {
        acherController.ReturnNormalState();
    }

    private void Start()
    {
        //
        InitializeAnimator();

        //
        acherController.OnHeroDash += DashSkillAnimate;
        acherController.OnHeroSpecial += SpecialSkillAnimate;
        acherController.OnHeroUltimate += UltimateSkillAnimate;
        acherController.OnHeroDead += DeadAnimate;
    }

    private void Update()
    {
        MoveAnimate();
    }
}

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
    private const string IS_MOVING = "Move"; 
    private const string DEAD = "Dead";
    private const string DASH = "Dash";
    private const string SPECIAL = "Special";
    private const string ULTIMATE = "Ultimate";
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
        if(inputVector != Vector2.zero) animator.SetBool(IS_MOVING, true);
        else animator.SetBool(IS_MOVING, false);
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
    // This function will handle the animation
    protected override void SpecialSkillAnimate()
    {
        animator.SetTrigger(SPECIAL);
    }
    // This function will handle the special skill effect
    protected void SpecialSkillActivate()
    {
            
    }

    //Paladin ultimate
    protected override void UltimateSkillAnimate()
    {
        animator.SetTrigger(ULTIMATE);
    }

    // SUPPORT FUNCTION
    protected override void ReturnNormalState()
    {
        paladinController.ReturnNormalState();
    }

    // Start is called before the first frame update
    void Start()
    {
        //
        InstantiateAnimator();

        // 
        paladinController.OnHeroDash += DashSkillAnimate;
        paladinController.OnHeroSpecial += SpecialSkillAnimate;
        paladinController.OnHeroUltimate += UltimateSkillAnimate;
        paladinController.OnHeroDead += DeadAnimate;
    }

    // Update is called once per frame
    void Update()
    {

        MoveAnimate();

    }
}



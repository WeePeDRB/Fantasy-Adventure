using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinAnimator : HeroAnimator
{
    
    // Controller reference 
    protected PaladinController paladinController;

    // SKill reference
    protected HeroSkill skill1;
    protected HeroSkill skill2;
    protected HeroSkill skill3;
    // Initialize data
    protected override void InitializeData()
    {
        // Animator
        animator = GetComponent<Animator>();

        // Paladin controller
        paladinController = GetComponentInParent<PaladinController>();

        // Skill 
        skill1 = paladinController.GetComponentInChildren<PaladinSkill1>();
        skill2 = paladinController.GetComponentInChildren<PaladinSkill2>();
        skill3 = paladinController.GetComponentInChildren<PaladinSkill3>();
    }

    // Animation handle
    // Hero movement
    protected override void MoveAnimate()
    {
        // Get input data
        Vector2 inputVector = GameInput.GetMovementVectorNormalized();

        // Set animation
        if (inputVector != Vector2.zero) animator.SetBool(IS_MOVING, true);
        else animator.SetBool(IS_MOVING, false);
    }
    // Hero Skill
    protected override void Skill1Animate()
    {
        // Set animation
        animator.SetTrigger(SKILL1);
    }
    protected void Skill1Activate()
    {
        skill1.SkillActivate();
    }

    protected override void Skill2Animate()
    {
        // Set animation
        animator.SetTrigger(SKILL2);
    }
    protected void Skill2Activate()
    {
        skill2.SkillActivate();
    }

    protected override void Skill3Animate()
    {
        // Set animation
        animator.SetTrigger(SKILL3);
    }
    protected void Skill3Activate()
    {
        skill3.SkillActivate();
    }

    // Hero dead
    protected override void DeadAnimate()
    {
        // Set animation
        animator.SetTrigger(IS_DEAD);
    }

    // 
    protected override void ReturnNormalState()
    {
        paladinController.ReturnToNormalState();
    }

    // 
    private void Start()
    {
        // Initialize data
        InitializeData();

        // Events subscription
        paladinController.OnUseSkill1 += Skill1Animate;
        paladinController.OnUseSkill2 += Skill2Animate;
        paladinController.OnUseSkill3 += Skill3Animate;

        paladinController.OnDead += DeadAnimate;
    }

    private void Update()
    {
        MoveAnimate();
    }
}

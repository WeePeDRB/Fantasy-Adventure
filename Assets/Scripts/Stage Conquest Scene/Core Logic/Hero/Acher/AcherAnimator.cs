using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcherAnimator : HeroAnimator
{
    // Controller reference 
    protected AcherController acherController;

    // SKill reference
    protected HeroSkill skill1;
    protected HeroSkill skill2;
    protected HeroSkill skill3;
    private float hyperInstictDuration = 10f;
    // Initialize data
    protected override void InitializeData()
    {
        // Animator
        animator = GetComponent<Animator>();

        // Paladin controller
        acherController = GetComponentInParent<AcherController>();

        // Skill 
        skill1 = acherController.GetComponentInChildren<AcherSkill1>();
        skill2 = acherController.GetComponentInChildren<AcherSkill2>();
        skill3 = acherController.GetComponentInChildren<AcherSkill3>();
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
        animator.SetBool(SKILL3, true);

        // Count down
        StartCoroutine(Skill3CountDown());
    }
    private IEnumerator Skill3CountDown()
    {
        yield return new WaitForSeconds(hyperInstictDuration);
        animator.SetBool(SKILL3, false);
    }

    // Hero dead
    protected override void DeadAnimate(HeroDead heroDead)
    {
        // Set animation
        animator.SetTrigger(IS_DEAD);
    }

    // 
    protected override void ReturnNormalState()
    {
        acherController.ReturnToNormalState();
    }

    // 
    private void Start()
    {
        // Initialize data
        InitializeData();

        // Events subscription
        acherController.OnUseSkill1 += Skill1Animate;
        acherController.OnUseSkill2 += Skill2Animate;
        acherController.OnUseSkill3 += Skill3Animate;

        acherController.OnDead += DeadAnimate;
    }

    private void Update()
    {
        MoveAnimate();
    }
}

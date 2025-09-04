using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcherController : HeroController
{
    // Hero skill management
    // Skill 1
    public event Action OnUseSkill1;
    // Skill 2
    public event Action OnUseSkill2;
    // Skill 3
    public event Action OnUseSkill3;
    private float hyperInstictDuration = 10f;
    // This is a flag to check whether the Archer hero has activated the third skill and entered the 'Hyper Instinct' state.
    private bool hyperInstict = false; 
    public bool HyperInstict
    {
        get { return hyperInstict; }
    }

    // Acher movement handle
    protected override void HandleMovement()
    {
        // Hero moving
        base.HandleMovement();

        // Hero dashing
        if (behaviorState == HeroBehaviorState.Dashing)
        {
            // Dash speed
            float speed = 10f;
            // Dash distance
            float distance = 1.2f;
            // Calculate destination
            Vector3 destination = transform.position + transform.forward * distance;

            // Dashing to destination 
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        }
    }

    // Acher skills handle
    protected override void HandleSkill1()
    {
        // Check if can use skill
        if (canUseSkill1)
        {
            // Check for hero behavior state
            if (behaviorState == HeroBehaviorState.Moving)
            {
                // Change the behavior state
                behaviorState = HeroBehaviorState.Dashing;

                // Invoke the dash event
                OnUseSkill1?.Invoke();

                //Set the dashing flag
                canUseSkill1 = false;

                //Reset the skill and special effect
                StartCoroutine(ResetSkill(skill1.SkillCooldown, 1));
            }
        }
    }

    protected override void HandleSkill2()
    {
        // Check if can use skill
        if (canUseSkill2)
        {
            // Check for hero behavior state
            if (behaviorState == HeroBehaviorState.Moving)
            {
                // Change the behavior state
                behaviorState = HeroBehaviorState.Casting;

                // Invoke the dash event
                OnUseSkill2?.Invoke();

                //Set the dashing flag
                canUseSkill2 = false;

                //Reset the skill and special effect
                StartCoroutine(ResetSkill(skill2.SkillCooldown, 2));
            }
        }
    }

    protected override void HandleSkill3()
    {
        // Check if can use skill
        if (canUseSkill3)
        {
            // Check for hero behavior state
            if (behaviorState == HeroBehaviorState.Moving)
            {
                // Invoke the dash event
                OnUseSkill3?.Invoke();

                //Set the dashing flag
                canUseSkill3 = false;

                //Reset the skill and special effect
                StartCoroutine(ResetSkill(skill3.SkillCooldown, 3));

                // Hyper instinct flag
                hyperInstict = true;
                // Start the count down coroutine 
                StartCoroutine(HyperInstinctCountDown());
            }
        }
    }
    private IEnumerator HyperInstinctCountDown()
    {
        yield return new WaitForSeconds(hyperInstictDuration);
        hyperInstict = false;
    }
    
    //
    private void Start()
    {
        // Initialize data
        InitializeData();

        // Events subscription
        GameInput.OnDashAction += HandleSkill1;
        GameInput.OnSpecialAction += HandleSkill2;
        GameInput.OnUltimateAction += HandleSkill3;

    }

    private void Update()
    {
        if (healthState == HeroHealthState.Alive)
        {
            HandleMovement();
            UpdateSpecialEffect();
            AmorRegeneration();
        }
    }
}

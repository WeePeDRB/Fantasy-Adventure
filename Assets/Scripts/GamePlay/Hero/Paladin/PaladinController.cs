using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PaladinController : HeroBaseController
{
    //
    // FIELDS
    //

    // PALADIN SKILLS
    // Dash skill 
    private float dashDistance; // How far is the dash
    private float dashSpeed; // How fast is the dash
    private Vector3 dashTarget; // The position that player will dash to

    // Special skill
    

    // EVENTS


    //
    // FUNCTIONS
    //

    // INITIAL VALUES FOR PALADIN
    // Paladin stats 
    public override void InstantiateStats()
    {
        heroStats = new HeroStats(  heroData.maxHealth, heroData.speed, heroData.level, heroData.maxAmor, 
                                    heroData.resistance, heroData.damageAmplifier, heroData.abilityHaste    );
    }

    // Paladin effect status
    public override void InstantiateEffectStatus()
    {
        heroEffectStatus = new HeroEffectStatus();
        heroEffectStatus.hero = this;
    }

    // Paladin inventory
    public override void InstantiateCharacterInventory()
    {

    }

    // Paladin dash values
    public override void InstantiateDash(   float instantiateDashDistance, float instantiateDashSpeed
                                            , float instantiateSpecialEffectDuration)
    {
        dashDistance = instantiateDashDistance;
        dashSpeed = instantiateDashSpeed;
        canDash = true;
    }

    

    // HANDLING PALADIN BEHAVIOR
    // Paladin movement function
    protected override void HandleMovement()
    {
        if ( !isDead )
        {
            if ( heroBehaviorState == HeroBehavior.Normal)
            {
                //Handle Input
                Vector2 inputVector = GameInput.GetMovementVectorNormalized();
                Vector3 moveDirVector = new Vector3(inputVector.x, 0, inputVector.y);
                //Move
                transform.position += moveDirVector * heroStats.Speed * Time.deltaTime;
                
                //Rotation
                float rotateSpeed = 10f;
                transform.forward = Vector3.Slerp(transform.forward, moveDirVector, Time.deltaTime * rotateSpeed);
            }
            else if (heroBehaviorState == HeroBehavior.Dashing)
            {
                transform.position = Vector3.MoveTowards(transform.position, dashTarget, dashSpeed * Time.deltaTime);
            }    
        }
    }

    // Paladin hurt function
    public override void Hurt(float damageTaken)
    {
        float damageAfterResistance = 0;
        float damageLeft = damageTaken;

        // Take damage 
        if (heroStats.Amor >= damageTaken)
        {
            heroStats.Amor -= damageTaken;
        }
        else 
        {
            if (heroStats.Amor > 0)
            {
                damageLeft -= heroStats.Amor;
                heroStats.Amor = 0;
            }

            damageAfterResistance = damageLeft - (damageLeft * heroStats.Resistance / 100f);
            heroStats.Health -= damageAfterResistance;
        }

        
        //
        canAmorRegen = false;
        // Check if there is already a coroutine
        if (regenCooldownCoroutine != null) StopCoroutine(regenCooldownCoroutine);
        regenCooldownCoroutine = StartCoroutine(AmorRegenCountDown());
    }

    // Paladin dead function
    protected override void Dead()
    {

    }

    // HANDLING PALADIN SKILLS
    // Handle the dash skill 
    protected override void HandleDashSkill()
    {
        if ( canDash )
        {
            // Change the behavior state
            heroBehaviorState = HeroBehavior.Dashing;

            // Invoke the dash event
            HandleOnHeroDash();

            // Set up the position for the dash
            dashTarget = transform.position + transform.forward * dashDistance;
            
            //Set the dashing flag
            canDash = false;

            //Reset the skill and special effect
            StartCoroutine(ResetDashSkill(heroData.dashSkill.skillCooldown));
        }
    }

    // Handle the special skill
    // This function will invoke the event and start the cooldown coroutine
    protected override void HandleSpecialSkill()
    {
        if ( canSpecial )
        {
            // Change the behavior state
            heroBehaviorState = HeroBehavior.Casting;

            // Invoke the special event
            HandleOnHeroSpecial();

            // Set special flag
            canSpecial = false;

            // Reset the skill
            StartCoroutine(ResetSpecialSkill(heroData.specialSkill.skillCooldown));
        }
    }
    // This function will handle the special skill effect
    public void SpecialSkillActivate()
    {
        
    }

    // Handle the ultimate skill
    // This function will invoke the event and start the cooldown coroutine
    protected override void HandleUltimateSkill()
    {
        if ( canUltimate )
        {
            // Change the behavior state
            heroBehaviorState = HeroBehavior.Casting;

            // Invoke the ultimate event
            HandleOnHeroUltimate();

            // Set ultimate flag
            canUltimate = false;

            // Reset the skill
            StartCoroutine(ResetUltimateSkill(heroData.ultimateSkill.skillCooldown));
        }
    }

    // This function will handle the ultimate skill effect
    public void UltimateSkillActivate()
    {

    }

    // SUPPORT FUNCTIONS
    private void TestCharacterStats()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("This is player current exp: " + heroStats.Exp);
            Debug.Log("This is player require exp: " + heroStats.ExpRequire);
            Debug.Log("");
        }
    }

    private void Awake()
    {
        InstantiateStats();
        InstantiateEffectStatus();
        InstantiateDash(5,18,3);

        canSpecial = true;
        canUltimate = true;

        //Set a subscriber for gameinput
        GameInput.OnDashAction += HandleDashSkill;
        GameInput.OnSpecialAction += HandleSpecialSkill;
        GameInput.OnUltimateAction += HandleUltimateSkill;

        //
    }

    private void Update()
    {
        HandleMovement();
        UpdateSpecialEffect();
        AmorRegen();
        TestCharacterStats();
    }

    protected override void UsetItem1()
    {
    }

    protected override void UsetItem2()
    {
    }

    protected override void UsetItem3()
    {
    }
}
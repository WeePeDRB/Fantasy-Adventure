using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        float damageAfter = damageTaken - (damageTaken * heroStats.Resistance / 100);
        heroStats.Health -= damageAfter;
    }

    // Paladin dead function
    protected override void Dead()
    {

    }

    // Paladin amor regeneration
    protected override void AmorRegen()
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
            RaiseOnHeroDash();

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
            RaiseOnHeroSpecial();

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
            RaiseOnHeroUltimate();

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
    private void OnCollisionEnter(Collision collision)
    {
        if (heroBehaviorState == HeroBehavior.Dashing && collision.gameObject.CompareTag("Wall"))
        {
            heroBehaviorState = HeroBehavior.Normal;
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

    private void TestCharacterStats()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("This is hero health :" + heroStats.Health);
            Debug.Log("This is hero resistance :" + heroStats.Resistance);
            Debug.Log("This is hero damage amplifier :" + heroStats.DamageAmplifier);
        }
    }

    private void Update()
    {
        HandleMovement();
        UpdateSpecialEffect();
        TestCharacterStats();
    }

}
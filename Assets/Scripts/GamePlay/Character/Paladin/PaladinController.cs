using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinController : CharacterBaseController
{
    //
    // FIELDS
    //

    // PALADIN SKILLS
    // Dash skill 
    private float dashDistance; // How far is the dash
    private float dashSpeed; // How fast is the dash
    private float dashCooldown; // Dash skill cool down
    private Vector3 dashTarget; // The position that player will dash to

    // Special skill
    

    // EVENTS
    public event Action OnPaladinDash;
    public event Action OnPaladinSpecial;
    public event Action OnPaladinUltimate;

    //
    // FUNCTIONS
    //

    // INITIAL VALUES FOR PALADIN
    // Paladin stats and status
    public override void InstantiateStatandStatus()
    {
        // Instantiate stats
        characterStats = new CharacterStats(100,7,100,1);

        // Instantiate status
        effectStatus = new EffectStatus();        
    }

    // Paladin inventory
    public override void InstantiateCharacterInventory()
    {

    }

    // Paladin dash values
    public override void InstantiateDash(   float instantiateDashDistance, float instantiateDashSpeed, 
                                            float instantiateDashCooldown, float instantiateSpecialEffectDuration)
    {
        dashDistance = instantiateDashDistance;
        dashSpeed = instantiateDashSpeed;
        dashCooldown = instantiateDashCooldown;
        canDash = true;
        isDashing = false; 
    }

    

    // HANDLING PALADIN BEHAVIOR
    // Paladin movement function
    protected override void HandleMovement()
    {
        if ( !isDead )
        {
            if ( !isDashing )
            {
                //Handle Input
                Vector2 inputVector = GameInput.GetMovementVectorNormalized();
                Vector3 moveDirVector = new Vector3(inputVector.x, 0, inputVector.y);
                //Move
                transform.position += moveDirVector * characterStats.Speed * Time.deltaTime;
                
                //Rotation
                float rotateSpeed = 10f;
                transform.forward = Vector3.Slerp(transform.forward, moveDirVector, Time.deltaTime * rotateSpeed);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, dashTarget, dashSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, dashTarget) < 0.1f)        
                {
                    isDashing = false;
                }
            }    
        }
    }

    // Paladin hurt function
    public override void Hurt(float damageTaken)
    {

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
            // Invoke the dash event
            OnPaladinDash?.Invoke();

            // Set up the position for the dash
            dashTarget = transform.position + transform.forward * dashDistance;
            
            //Set the dashing flag
            isDashing = true;
            canDash = false;

            //Reset the skill and special effect
            StartCoroutine(ResetDashSkill(5f));
        }
    }

    // Handle the special skill
    // This function will invoke the event and start the cooldown coroutine
    protected override void HandleSpecialSkill()
    {
        if ( canSpecial )
        {
            // Invoke the special event
            OnPaladinSpecial?.Invoke();
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
            // Invoke the ultimate event
            OnPaladinUltimate?.Invoke();
        }
    }
    // This function will handle the ultimate skill effect
    public void UltimateSkillActivate()
    {

    }
    // SUPPORT FUNCTIONS
    private void OnCollisionEnter(Collision collision)
    {
        if (isDashing && collision.gameObject.CompareTag("Wall"))
        {
            isDashing = false;
        }
    }


    private void Awake()
    {
        //
        Instance = this;
        InstantiateStatandStatus();
        InstantiateDash(5,18,5,3);

        canSpecial = true;
        canUltimate = true;

        //Set a subscriber
        GameInput.OnDashAction += HandleDashSkill;
        GameInput.OnSpecialAction += HandleSpecialSkill;
        GameInput.OnUltimateAction += HandleUltimateSkill;
    }

    private void Update()
    {
        HandleMovement();
    }
}
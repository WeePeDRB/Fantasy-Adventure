using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBaseController : MonoBehaviour
{
    //
    // Character stats
    //
    // Basic stats
    protected float maxHealth;
    protected float health;
    protected float speed;
    protected float maxAmor;
    protected float amor;
    protected int level;

    // Special stats
    
    // This stat will block a percentage of the damage received 
    // by the player, with a value ranging from 1 to 100.
    protected float resistance; 
    
    //This stat represents the percentage of time reduced for 
    // skill cooldowns.
    protected float abilityHaste;

    // This stat increases the damage dealt by weapons.
    protected float damageAmplifier;

    //
    // Character inventory system
    //
    protected IWeapon primaryWeapon; // Primary weapon for each character
    protected List<IWeapon> weapons; // Character weapon list
    protected int maxWeapon; // Ammount of weapon
    protected List<IItem> items; // Character item list
    protected int maxItem; // Ammount of item


    //
    // Fields for the dash skill 
    //
    protected bool canDash;  
    protected bool isDashing;
    protected float dashDistance; // How far is the dash
    protected float dashSpeed; // How fast is the dash
    protected float dashCooldown; // Dash skill cool down
    protected Vector3 dashTarget; // The position that player will dash to


    //
    // Character instance
    //
    public static CharacterBaseController Instance { get; private set; }
    

    //
    // Initial stats for character
    //
    public virtual void InstantiateCharacter(    float instantiateMaxHealth, float instantiateSpeed, 
                                                    float instantiateMaxAmor, int instantiateLevel          )
    {
        maxHealth = instantiateMaxHealth;
        health = maxHealth;
        speed = instantiateSpeed;
        maxAmor = instantiateMaxAmor;
        amor = maxAmor;
        level = instantiateLevel;
    }

    //
    //
    //
    public virtual void InstantiateCharacterWeapon()
    {

    }

    //
    // Initial for character dash skill
    //
    public virtual void InstantiateDash(     float instantiateDashDistance, float instantiateDashSpeed, 
                                                float instantiateDashCooldown, float instantiateSpecialEffectDuration   )
    {
        dashDistance = instantiateDashDistance;
        dashSpeed = instantiateDashSpeed;
        dashCooldown = instantiateDashCooldown;
        canDash = true;
        isDashing = false; 

    }

    //
    // Take the player input and move the character
    //
    protected virtual void HandleMovement()
    {
        if (!isDashing)
        {
            //Handle Input
            Vector2 inputVector = GameInput.GetMovementVectorNormalized();
            Vector3 moveDirVector = new Vector3(inputVector.x, 0, inputVector.y);
            //Move
            transform.position += moveDirVector * speed * Time.deltaTime;
            
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


    //
    // Handle the dash skill 
    //
    protected virtual void HandleDashSkill(object sender, EventArgs e)
    {
        Debug.Log("This is the sender: " + sender.GetType());
        if (!isDashing && canDash)
        {
            //Set the destination for the dash skill
            dashTarget = transform.position + transform.forward * dashDistance;
            //Set the dashing flag
            isDashing       = true;
            canDash = false;

            //Reset the skill and special effect
            Invoke(nameof(ResetDashSkill), dashCooldown); 
        }
    }
    //
    // Handle the special skill
    //
    protected abstract void HandleSpecialSkill(object sender, EventArgs e);


    //
    // Handle the ultimate skill
    //
    protected abstract void HandleUltimateSkill(object sender, EventArgs e);


    //
    // Take place when player get hit
    //
    protected virtual void Hurt()
    {

    }


    //
    //
    //
    protected virtual void Dead()
    {

    }

    //
    //
    //
    protected virtual void AmorRegen()
    {

    }

    //
    //
    //

    //
    // Support functions
    //
    protected void ResetDashSkill() => canDash = true;


    //
    //
    //
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There are more than one player instance !");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}

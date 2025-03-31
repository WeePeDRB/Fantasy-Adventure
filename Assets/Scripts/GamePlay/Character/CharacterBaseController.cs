using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CharacterBaseController : MonoBehaviour
{
    //
    //  FIELDS
    //

    // CHARACTER STATS
    // Basic stats
    protected float maxHealth; // Maximum health 
    protected float health; // Current health
    protected float speed; // Movement speed
    protected float maxAmor; // Maximum amor
    protected float amor; // Current amor
    protected int level; // Character level

    // Special stats
    protected float resistance; // This stat will block a percentage of the damage received by the player, with a value ranging from 1 to 100.
    protected float abilityHaste; // This stat represents the percentage of time reduced for skill cooldowns.
    protected float damageAmplifier; // This stat increases the damage dealt by weapons.
    
    

    // CHARACTER INVENTORY SYSTEM
    // Weapon system
    protected IWeapon primaryWeapon; // Primary weapon for each character
    protected List<IWeapon> weapons; // Weapon list
    protected int maxWeapon; // Ammount of weapon
    
    // Item system
    protected List<IItem> items; // Item list
    protected int maxItem; // Ammount of item
    


    // Fields for the dash skill 
    protected bool canDash;  
    protected bool isDashing;
    protected float dashDistance; // How far is the dash
    protected float dashSpeed; // How fast is the dash
    protected float dashCooldown; // Dash skill cool down
    protected Vector3 dashTarget; // The position that player will dash to


    // Character instance
    public static CharacterBaseController Instance { get; private set; }

    

    //
    // FUNCTIONS
    //

    // INITIAL VALUES FOR PLAYER
    // Character stats
    public virtual void InstantiateCharacter(   float instantiateMaxHealth, float instantiateSpeed, 
                                                float instantiateMaxAmor, int instantiateLevel          )
    {
        // Character basic stats
        maxHealth = instantiateMaxHealth;
        health = maxHealth;
        speed = instantiateSpeed;
        maxAmor = instantiateMaxAmor;
        amor = maxAmor;
        level = instantiateLevel;

        // Character special stats
        resistance = 0f;
        abilityHaste = 0f;
        damageAmplifier = 0f;
    }

    // Character inventory
    public virtual void InstantiateCharacterInventory()
    {

    }

    // Character dash values
    public virtual void InstantiateDash(    float instantiateDashDistance, float instantiateDashSpeed, 
                                            float instantiateDashCooldown, float instantiateSpecialEffectDuration   )
    {
        dashDistance = instantiateDashDistance;
        dashSpeed = instantiateDashSpeed;
        dashCooldown = instantiateDashCooldown;
        canDash = true;
        isDashing = false; 
    }



    // SET UP COMMON FUNCTIONS FOR CHARACTER
    // Character movement function
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

    // Character hurt function
    public virtual void Hurt(float damageTaken)
    {
        // Calculate damage taken due to resistance stat
        damageTaken -= damageTaken * resistance / 100;
        health -= damageTaken;
        if (health <= 0f)
        {
            Dead();
        }
    }

    // Character dead function
    protected virtual void Dead()
    {

    }

    // Character amor regeneration
    protected virtual void AmorRegen()
    {

    }

    // Handle the dash skill 
    protected abstract void HandleDashSkill();

    // Handle the special skill
    protected abstract void HandleSpecialSkill();

    // Handle the ultimate skill
    protected abstract void HandleUltimateSkill();



    // MODIFY CHARACTER STATS
    // Modify health
    public void ModifyHealth(float healthValue)
    {
        health += healthValue;
    }

    // Modify speed
    public void ModifySpeed(float speedValue)
    {
        speed += speedValue;
    }

    // Modify resistance
    public void ModifyResistance(float resistanceValue)
    {
        resistance += resistanceValue;
    }

    // Modify ability haste
    public void ModifyAbilityHaste(float abilityHasteValue)
    {
        abilityHaste += abilityHasteValue;
    }

    // Modify damage
    public void ModifyDamage(float damageAmplifierValue)
    {
        damageAmplifier += damageAmplifierValue;
    }



    // SUPPORT FUNCTIONS
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

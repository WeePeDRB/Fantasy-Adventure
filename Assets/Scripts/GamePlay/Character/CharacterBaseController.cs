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
    public CharacterStats characterStats;
    
    

    // CHARACTER INVENTORY SYSTEM
    // Weapon system
    protected IWeapon primaryWeapon; // Primary weapon for each character
    protected List<IWeapon> weapons; // Weapon list
    protected int maxWeapon; // Ammount of weapon
    
    // Item system
    protected List<IItem> items; // Item list
    protected int maxItem; // Ammount of item
    


    // CHARACTER EFFECT STATUS
    // EffectStatus 
    protected EffectStatus effectStatus; // Effect manager



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

    // Character hurt function
    public virtual void Hurt(float damageTaken)
    {
        // Calculate damage taken due to resistance stat
        damageTaken -= damageTaken * characterStats.Resistance / 100;
        characterStats.Health -= damageTaken;
        if (characterStats.Health == 0f)
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



    // SUPPORT FUNCTIONS
    // Access status effect
    public void ReceiveSpecialEffect(SpecialEffectBase specialEffect)
    {
        effectStatus.ReceiveEffect(specialEffect);
    }

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

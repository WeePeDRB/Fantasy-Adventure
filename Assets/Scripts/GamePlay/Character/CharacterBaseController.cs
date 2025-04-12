using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CharacterBaseController : MonoBehaviour
{
    //
    // FIELDS
    //

    // CHARACTER BEHAVIOR STATE
    protected CharacterBehavior characterBehaviorState; 

    // CHECKING FLAGS
    protected bool canDash;  
    protected bool canSpecial;
    protected bool canUltimate;
    protected bool isDead;

    // CHARACTER INVENTORY SYSTEM
    // Weapon system
    protected IWeapon primaryWeapon; // Primary weapon for each character
    protected List<IWeapon> weapons; // Weapon list
    protected int maxWeapon; // Ammount of weapon
    
    // Item system
    protected List<IItem> items; // Item list
    protected int maxItem; // Ammount of item

    // CHARACTER INSTANCE
    public static CharacterBaseController Instance { get; protected set; }

    // CHARACTER STATS
    public CharacterStats characterStats;

    // CHARACTER EFFECT STATUS
    public EffectStatus effectStatus; 

    // CHARACTER SKILLS


    //
    // FUNCTIONS
    //

    // INITIAL VALUES FOR CHARACTER
    // Character stats and status
    public abstract void InstantiateStatandStatus();

    // Character inventory
    public abstract void InstantiateCharacterInventory();

    // Character dash values
    public abstract void InstantiateDash(    float instantiateDashDistance, float instantiateDashSpeed, 
                                            float instantiateDashCooldown, float instantiateSpecialEffectDuration   );


    // HANDLING CHARACTER BEHAVIOR
    // Character movement function
    protected abstract void HandleMovement();

    // Character hurt function
    public abstract void Hurt(float damageTaken);

    // Character dead function
    protected abstract void Dead();

    // Character amor regeneration
    protected abstract void AmorRegen();

    // HANDLING CHARACTER SKILLS
    // Handle the dash skill 
    protected abstract void HandleDashSkill();

    // Handle the special skill
    protected abstract void HandleSpecialSkill();

    // Handle the ultimate skill
    protected abstract void HandleUltimateSkill();


    // SUPPORT FUNCTIONS
    // Set character state to normal
    public void ReturnNormalState()
    {
        Debug.Log("return normal state in paladin");
        characterBehaviorState = CharacterBehavior.Normal;
    }

    // Access status effect
    public virtual void ReceiveSpecialEffect(SpecialEffectBase specialEffect)
    {
        effectStatus.ReceiveEffect(specialEffect);
    }

    // Reset dash skill
    protected IEnumerator ResetDashSkill(float dashSkillCooldown)
    {
        yield return new WaitForSeconds(dashSkillCooldown);
        canDash = true;
    }

    // Reset special skill
    protected IEnumerator ResetSpecialSkill(float specialSkillCooldown)
    {
        yield return new WaitForSeconds(specialSkillCooldown);
        canSpecial = true;
    }

    // Reset ultimate skill
    protected IEnumerator ResetUltimateSkill(float ultimateSkillCooldown)
    {
        yield return new WaitForSeconds(ultimateSkillCooldown);
        canUltimate = true;
    }
}

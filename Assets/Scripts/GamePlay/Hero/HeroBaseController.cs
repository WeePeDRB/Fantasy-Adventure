using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class HeroBaseController : MonoBehaviour
{
    //
    // FIELDS
    //

    // Hero data
    [SerializeField] protected SO_Hero heroData;

    // HERO BEHAVIOR STATE
    protected HeroBehavior heroBehaviorState; 

    // CHECKING FLAGS
    protected bool canDash;  
    protected bool canSpecial;
    protected bool canUltimate;
    protected bool isDead;

    // HERO INVENTORY SYSTEM
    // Weapon system
    protected IWeapon primaryWeapon; // Primary weapon for each hero
    protected List<IWeapon> weapons; // Weapon list
    protected int maxWeapon; // Ammount of weapon
    
    // Item system
    protected List<IItem> items; // Item list
    protected int maxItem; // Ammount of item

    // HERO STATS
    public HeroStats heroStats;

    // HERO EFFECT STATUS
    protected HeroEffectStatus heroEffectStatus; 

    // HERO SKILLS

    //
    // PROPERTIES
    //
    public SO_Hero HeroData
    {
        get { return heroData; }
    }

    public HeroBehavior HeroBehavior
    {
        get { return heroBehaviorState; }
    }

    //
    // FUNCTIONS
    //

    // INITIAL VALUES FOR HERO
    // Hero stats 
    public abstract void InstantiateStats();

    // Hero effect status
    public abstract void InstantiateEffectStatus();

    // Hero inventory
    public abstract void InstantiateCharacterInventory();

    // Hero dash values
    public abstract void InstantiateDash(    float instantiateDashDistance, float instantiateDashSpeed, 
                                            float instantiateDashCooldown, float instantiateSpecialEffectDuration   );


    // HANDLING HERO BEHAVIOR
    // Hero movement function
    protected abstract void HandleMovement();

    // Hero hurt function
    public abstract void Hurt(float damageTaken);

    // Hero dead function
    protected abstract void Dead();

    // Hero amor regeneration
    protected abstract void AmorRegen();

    // HANDLING HERO SKILLS
    // Handle the dash skill 
    protected abstract void HandleDashSkill();

    // Handle the special skill
    protected abstract void HandleSpecialSkill();

    // Handle the ultimate skill
    protected abstract void HandleUltimateSkill();


    // SUPPORT FUNCTIONS
    // Set hero state to normal
    public void ReturnNormalState()
    {
        heroBehaviorState = HeroBehavior.Normal;
    }

    // Access status effect
    public virtual void ReceiveSpecialEffect(SpecialEffectBase specialEffect)
    {
        heroEffectStatus.ReceiveEffect(specialEffect);
    }
    public virtual void UpdateSpecialEffect()
    {
        if (heroEffectStatus.IsDictionaryEmpty())
        {
            return;
        }
        heroEffectStatus.UpdateEffects(Time.deltaTime);
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

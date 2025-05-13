using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class HeroBaseController : MonoBehaviour
{
    //
    // FIELDS
    //

    // HERO DATA
    [SerializeField] protected SO_Hero heroData;

    // HERO STATE
    protected HeroMovementState heroMovementState; 
    protected HeroHealthState heroHealthState;

    // CHECKING FLAGS
    protected bool canAmorRegen;
    protected bool canDash;  
    protected bool canSpecial;
    protected bool canUltimate;

    // HERO STATS
    protected HeroStats heroStats;

    // HERO EFFECT STATUS
    protected HeroEffectStatus heroEffectStatus; 

    // HERO PHYSICS
    protected Rigidbody heroRigidBody;
    protected CapsuleCollider heroCollider;

    // COROUTINE VALUE
    protected Coroutine regenCooldownCoroutine;
    
    // HERO SKILL EVENTS
    public  event Action OnHeroDash;
    public  event Action OnHeroSpecial;
    public  event Action OnHeroUltimate;

    // LEVEL UP EVENTS
    public event Action OnLevelUp;

    // DEAD EVENTS
    public event Action OnHeroDead;

    // HERO INVENTORY SYSTEM
    // Weapon system
    protected IWeapon primaryWeapon; // Primary weapon for each hero
    protected List<IWeapon> weapons; // Weapon list
    protected int maxWeapon; // Ammount of weapon
    
    // Blessing system
    protected List<BlessingBase> blessings; // Blessing list
    protected int maxBlessing; // Ammount of blessing

    // Inventory
    protected int coin; 
    
    //
    // PROPERTIES
    //
    public SO_Hero HeroData
    {
        get { return heroData; }
    }

    public HeroStats HeroStats
    {
        get { return heroStats; }
    }

    public HeroMovementState HeroMovementState
    {
        get { return heroMovementState; }
    }

    //
    // FUNCTIONS
    //

    // INITIAL VALUES FOR HERO
    //
    public abstract void InitilizeValue();

    // Hero stats 
    public abstract void InitializeStats();

    // Hero effect status
    public abstract void InitializeEffectStatus();

    // Hero inventory
    public abstract void InitializeCharacterBlessing();

    // Hero dash values
    public abstract void InitializeDash(    float instantiateDashDistance, float instantiateDashSpeed, 
                                             float instantiateSpecialEffectDuration   );


    // HANDLING HERO BEHAVIOR
    // Hero movement function
    protected abstract void HandleMovement();

    // Hero hurt function
    public abstract void Hurt(float damageTaken);

    // Hero dead function
    protected abstract void Dead();

    // Hero amor regeneration
    protected virtual void AmorRegen()
    {
        if (canAmorRegen) if (heroStats.Amor < heroStats.MaxAmor) heroStats.Amor+= 5f * Time.deltaTime;
    }
    protected virtual IEnumerator AmorRegenCountDown()
    {
        yield return new WaitForSeconds(10f);
        canAmorRegen = true;
    }

    // HANDLING HERO SKILLS
    // Handle the dash skill 
    protected abstract void HandleDashSkill();

    // Handle the special skill
    protected abstract void HandleSpecialSkill();

    // Handle the ultimate skill
    protected abstract void HandleUltimateSkill();

    // LEVEL UP
    protected virtual void LevelUp()
    {
        if (heroStats.Exp == heroStats.ExpRequire)
        {
            // Update exp status
            heroStats.Level ++;
            heroStats.ExpRequire += heroStats.Level * 100;
            heroStats.Exp = 0;

            // Invoke level up event
            OnLevelUp?.Invoke();
        }
    }
    
    // SUPPORT FUNCTIONS
    // Set hero state to normal
    public void ReturnNormalState()
    {
        heroMovementState = HeroMovementState.Normal;
    }

    // Special effect handling
    // Receive special effect
    public virtual void ReceiveSpecialEffect(SpecialEffectBase specialEffect)
    {
        heroEffectStatus.ReceiveEffect(specialEffect);
    }
    // Update special effect
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

    // Invoke event
    protected void HandleOnHeroDash()
    {
        OnHeroDash?.Invoke();
    }
    protected void HandleOnHeroSpecial()
    {
        OnHeroSpecial?.Invoke();
    }
    protected void HandleOnHeroUltimate()
    {
        OnHeroUltimate?.Invoke();
    }
    protected void HandleOnHeroDead()
    {
        OnHeroDead?.Invoke();
    }
}

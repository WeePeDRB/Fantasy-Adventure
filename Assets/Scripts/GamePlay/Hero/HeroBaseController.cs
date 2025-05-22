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

    // RECEIVE UPGRADE
    public event EventHandler<WeaponEventArgs> OnReceiveWeapon;
    public event EventHandler<BlessingEventArgs> OnReceiveBlessing;
    public event EventHandler<WeaponDataEventArgs> OnWeaponMaxLevel;
    public event EventHandler<BlessingDataEventArgs> OnBlessingMaxLevel;
    public event EventHandler<WeaponListEventArgs> OnWeaponListFull;
    public event EventHandler<BlessingListEventArgs> OnBlessingListFull;

    // HERO INVENTORY SYSTEM
    // Weapon system
    protected HeroWeaponSystem heroWeaponSystem;
    // Hero effect status
    protected HeroSpecialEffectSystem heroSpecialEffectSystem; 
    // Hero blessing status
    protected HeroBlessingSystem heroBlessingSystem;

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
    protected abstract void InitilizeValue();

    // Hero stats 
    protected abstract void InitializeStats();

    // Hero effect system
    protected abstract void InitializeEffectSystem();

    // Hero blessing system
    protected abstract void InitializeBlessingSystem();

    // Hero weapon system
    protected abstract void InitializeWeaponSystem();

    // Hero dash values
    public abstract void InitializeDash(float instantiateDashDistance, float instantiateDashSpeed,
                                             float instantiateSpecialEffectDuration);


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
            InvokeOnlevelUp();
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
        Debug.Log("Hero receive effect");
        heroSpecialEffectSystem.ReceiveEffect(specialEffect);
    }
    // Update special effect
    public virtual void UpdateSpecialEffect()
    {
        if (heroSpecialEffectSystem.IsDictionaryEmpty())
        {
            return;
        }
        heroSpecialEffectSystem.UpdateEffects(Time.deltaTime);
    }

    // Receive upgrade
    protected void ReceiveWeapon(object sender, WeaponDataEventArgs weaponDataEventArgs)
    {
        //
        SO_Weapon _weaponData = weaponDataEventArgs.weaponData;

        if (heroWeaponSystem.IsWeaponExist(_weaponData))
        {
            // Weapon level up
            heroWeaponSystem.WeaponLevelUp(_weaponData);

            // Check if weapon reach max level
            WeaponBase weapon = heroWeaponSystem.GetWeapon(_weaponData);
            // If reach max level send data to upgrade manager 
            if (weapon.WeaponLevel == 5) OnWeaponMaxLevel?.Invoke(this, new WeaponDataEventArgs { weaponData = _weaponData });
        }
        else
        {
            // Instantiate weapon game object
            GameObject weapon = Instantiate(_weaponData.weaponPrefab, transform.position, transform.rotation, transform);

            if (weapon.TryGetComponent(out WeaponBase weaponBase))
            {
                weaponBase.InitializeWeapon(_weaponData);
                heroWeaponSystem.ReceiveWeapon(_weaponData, weaponBase);
                if (heroWeaponSystem.IsWeaponQuantityMax())
                {
                    OnWeaponListFull?.Invoke(this, new WeaponListEventArgs { weaponDataList = heroWeaponSystem.GetWeaponList()});
                }
            }
            else
            {
                Debug.LogError("The weapon prefab don't have WeaponBase component !");
            }
        }
        OnReceiveWeapon?.Invoke(this, new WeaponEventArgs { weapon = heroWeaponSystem.GetWeapon(_weaponData), weaponData = _weaponData});
    }
    protected void ReceiveBlessing(object sender, BlessingDataEventArgs blessingDataEventArgs)
    {
        SO_Blessing _blessingData = blessingDataEventArgs.blessingData;

        if (heroBlessingSystem.IsBlessingExist(_blessingData))
        {
            // Blessing level up
            heroBlessingSystem.BlessingLevelUp(_blessingData, this);

            //
            BlessingBase blessing = heroBlessingSystem.GetBlessing(_blessingData);
            if (blessing.BlessingLevel == 5) OnBlessingMaxLevel?.Invoke(this, new BlessingDataEventArgs { blessingData = _blessingData });
        }
        else
        {
            BlessingBase blessing = UpgradeFactory.CreateBlessing(_blessingData);
            heroBlessingSystem.ReceiveBlessing(_blessingData, blessing, this);

            if (heroBlessingSystem.IsBlessingQuantityMax())
            {
                OnBlessingListFull?.Invoke(this, new BlessingListEventArgs { blessingDataList = heroBlessingSystem.GetBLessingList() });
            }
        }
        OnReceiveBlessing?.Invoke(this, new BlessingEventArgs { blessing = heroBlessingSystem.GetBlessing(_blessingData), blessingData = _blessingData });
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
    protected void InvokeOnHeroDash()
    {
        OnHeroDash?.Invoke();
    }
    protected void InvokeOnHeroSpecial()
    {
        OnHeroSpecial?.Invoke();
    }
    protected void InvokeOnHeroUltimate()
    {
        OnHeroUltimate?.Invoke();
    }
    protected void InvokeOnHeroDead()
    {
        OnHeroDead?.Invoke();
    }
    protected void InvokeOnlevelUp()
    {
        OnLevelUp?.Invoke();
    }
}

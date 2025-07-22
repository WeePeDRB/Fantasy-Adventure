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

    // Hero state
    protected HeroMovementState heroMovementState; 
    protected HeroHealthState heroHealthState;


    // Hero data management
    protected HeroStats heroStats; // Hero stats manage system
    protected HeroWeaponSystem heroWeaponSystem; // Hero weapon manage system
    protected HeroSpecialEffectSystem heroSpecialEffectSystem; // Hero special effect manage system
    protected HeroBlessingSystem heroBlessingSystem; // Hero blessing manage system
    protected int coin;

    // Hero physics system
    protected Rigidbody heroRigidBody;
    protected CapsuleCollider heroCollider;

    // Coroutine 
    protected Coroutine regenCooldownCoroutine;
    
    // Hero events
    public  event Action OnHeroDash; // Hero activate dash event
    public  event Action OnHeroSpecial; // Hero activate special event
    public  event Action OnHeroUltimate; // Hero activate ultimate event
    public event Action OnLevelUp; // Hero level up event
    public event Action OnHeroDead; // Hero dead event
    public event EventHandler<WeaponEventArgs> OnReceiveWeapon; // Hero receive weapon event
    public event EventHandler<WeaponDataEventArgs> OnWeaponMaxLevel; // Hero weapon reach max level event
    public event EventHandler<WeaponListEventArgs> OnWeaponListFull; // Hero weapon list full event
    public event EventHandler<BlessingEventArgs> OnReceiveBlessing; // Hero receive blessing event
    public event EventHandler<BlessingDataEventArgs> OnBlessingMaxLevel; // Hero blessing reach max level event
    public event EventHandler<BlessingListEventArgs> OnBlessingListFull; // Hero blessing list full event

    // Checking flags
    protected bool canAmorRegen;
    protected bool canDash;  
    protected bool canSpecial;
    protected bool canUltimate;

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
    
    public HeroHealthState HeroHealthState
    {
        get { return heroHealthState; }
    }

    public HeroSpecialEffectSystem HeroSpecialEffectSystem
    {
        get { return heroSpecialEffectSystem; }
    }

    //
    // FUNCTIONS
    //

    // Initialize hero data

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
    protected virtual void InitializePrimaryWeapon(SO_Weapon weaponData)
    {
        // Instantiate weapon game object
        GameObject weapon = Instantiate(weaponData.weaponPrefab, transform.position, transform.rotation, transform);

        if (weapon.TryGetComponent(out WeaponBase weaponBase))
        {
            weaponBase.InitializeWeapon(weaponData);
            heroWeaponSystem.ReceiveWeapon(weaponData, weaponBase);
            if (heroWeaponSystem.IsWeaponQuantityMax())
            {
                OnWeaponListFull?.Invoke(this, new WeaponListEventArgs { weaponDataList = heroWeaponSystem.GetWeaponList() });
            }
        }
        else
        {
            Debug.LogError("The weapon prefab don't have WeaponBase component !");
        }

        OnReceiveWeapon?.Invoke(this, new WeaponEventArgs { weapon = heroWeaponSystem.GetWeapon(weaponData)});
    }

    // Hero dash data
    public abstract void InitializeDash(float instantiateDashDistance, float instantiateDashSpeed,
                                             float instantiateSpecialEffectDuration);

    // Hero special data
    public abstract void InitializeSpecial();

    // Hero ultimate data
    public abstract void InitializeUltimate();

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
    public void GainExp(int expValue)
    {
        heroStats.Exp += expValue;
        LevelUp();
    }

    protected virtual void LevelUp()
    {
        if (heroStats.Exp == heroStats.ExpRequire)
        {
            // Update exp status
            heroStats.Level++;
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
        heroSpecialEffectSystem.ReceiveEffect(specialEffect);
    }
    // Update special effect
    public virtual void UpdateSpecialEffect()
    {
        if (heroSpecialEffectSystem.IsDictionaryEmpty())
        {
            return;
        }
        heroSpecialEffectSystem.UpdateEffectsTime(Time.deltaTime);
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
        OnReceiveWeapon?.Invoke(this, new WeaponEventArgs { weapon = heroWeaponSystem.GetWeapon(_weaponData)});
    }
    protected void ReceiveBlessing(object sender, BlessingDataEventArgs blessingDataEventArgs)
    {
        SO_Blessing blessingData = blessingDataEventArgs.blessingData;

        if (heroBlessingSystem.IsBlessingExist(blessingData))
        {
            // Blessing level up
            heroBlessingSystem.BlessingLevelUp(blessingData, this);

            //
            BlessingBase blessing = heroBlessingSystem.GetBlessing(blessingData);
            if (blessing.BlessingLevel == 5) OnBlessingMaxLevel?.Invoke(this, new BlessingDataEventArgs { blessingData = blessingData });
        }
        else
        {
            BlessingBase blessing = UpgradeFactory.CreateBlessing(blessingData);
            heroBlessingSystem.ReceiveBlessing(blessingData, blessing, this);

            if (heroBlessingSystem.IsBlessingQuantityMax())
            {
                OnBlessingListFull?.Invoke(this, new BlessingListEventArgs { blessingDataList = heroBlessingSystem.GetBLessingList() });
            }
        }
        OnReceiveBlessing?.Invoke(this, new BlessingEventArgs { blessing = heroBlessingSystem.GetBlessing(blessingData)});
    }

    // Reset dash skill
    protected IEnumerator ResetDashSkill(float dashSkillCooldown)
    {
        float coolDown;
        if (heroStats.AbilityHaste != 0) coolDown = dashSkillCooldown - (dashSkillCooldown * heroStats.AbilityHaste / 100);
        else coolDown = dashSkillCooldown;
        yield return new WaitForSeconds(coolDown);
        canDash = true;
    }

    // Reset special skill
    protected IEnumerator ResetSpecialSkill(float specialSkillCooldown)
    {
        float coolDown;
        if (heroStats.AbilityHaste != 0) coolDown = specialSkillCooldown - (specialSkillCooldown * heroStats.AbilityHaste / 100);
        else coolDown = specialSkillCooldown;
        yield return new WaitForSeconds(coolDown);
        canSpecial = true;
    }

    // Reset ultimate skill
    protected IEnumerator ResetUltimateSkill(float ultimateSkillCooldown)
    {
        float coolDown;
        if (heroStats.AbilityHaste != 0) coolDown = ultimateSkillCooldown - (ultimateSkillCooldown * heroStats.AbilityHaste / 100);
        else coolDown = ultimateSkillCooldown;
        yield return new WaitForSeconds(coolDown);
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

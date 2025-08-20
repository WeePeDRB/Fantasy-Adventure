using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroController : MonoBehaviour
{
    // Hero stats management
    [SerializeField] protected SO_HeroStats statsData;
    public SO_HeroStats StatsData
    {
        get { return statsData; }
    }
    protected HeroStatsController statsController;
    public HeroStatsController StatsController
    {
        get { return statsController; }
    }

    // Hero weapon management
    protected HeroWeaponController weaponController;
    public HeroWeaponController WeaponController
    {
        get { return weaponController; }
    }

    // Hero blessing management
    protected HeroBlessingController blessingController;
    public HeroBlessingController BlessingController
    {
        get { return blessingController; }
    }

    // Hero special effect management
    protected HeroSpecialEffectController specialEffectController;
    public HeroSpecialEffectController SpecialEffectController
    {
        get { return specialEffectController; }
    }

    // Hero physics management
    protected Rigidbody rigidBody;
    protected CapsuleCollider bodyCollider;

    // Hero state management
    protected HeroBehaviorState behaviorState;
    public HeroBehaviorState BehaviorState
    {
        get { return behaviorState; }
    }
    protected HeroHealthState healthState;
    public HeroHealthState HealthState
    {
        get { return healthState; }
    }

    // Hero amor regeneration
    protected bool canAmorRegen;
    protected Coroutine regenCooldownCoroutine;

    // Hero skill management
    // Skill 1 
    [SerializeField] protected HeroSkill skill1;
    protected bool canUseSkill1;
    public event Action OnUseSkill1;
    // Skill 2
    [SerializeField] protected HeroSkill skill2;
    protected bool canUseSkill2;
    public event Action OnUseSkill2;
    // Skill 3
    [SerializeField] protected HeroSkill skill3;
    protected bool canUseSkill3;
    public event Action OnUseSkill3;

    // Hero level up
    public event Action OnLevelUp;

    // Hero dead
    public event Action OnDead;

    // Initialize data
    protected abstract void InitializeData();

    // Handle logic

    // Hero weapon
    protected void ReceiveWeapon(WeaponDataEventArgs eventArgs)
    {
        if (eventArgs == null)
        {
            Debug.LogWarning("Weapon data is null !");
            return;
        }

        // Get data from event args
        SO_Weapon weaponData = eventArgs.weaponData;

        // Check if weapon exist
        if (weaponController.IsWeaponExist(weaponData))
        {
            weaponController.ReceiveWeapon(weaponData, true);
        }
        else
        {
            GameObject weaponGO = Instantiate(weaponData.weaponPrefab, transform.position, transform.rotation, transform);
            weaponController.ReceiveWeapon(weaponData,false, weaponGO);
        }
    }

    // Hero blessing
    protected void ReceiveBlessing(BlessingDataEventArgs eventArgs)
    {
        if (eventArgs == null)
        {
            Debug.LogWarning("Weapon data is null !");
            return;
        }

        // Get data from event args
        SO_Blessing blessingData = eventArgs.blessingData;

        //
        blessingController.ReceiveBlessing(blessingData, this);
    }

    // Hero special effect
    protected void ReceiveSpecialEffect(SpecialEffectBase effect)
    {
        specialEffectController.ReceiveEffect(effect);
    }
    protected void UpdateSpecialEffect()
    {
        if (specialEffectController.IsDictionaryEmpty()) return;
        specialEffectController.UpdateEffect(Time.deltaTime);
    }

    // Hero movement
    protected abstract void HandleMovement();

    // Hero damage handle
    protected abstract void Hurt();
    protected abstract void Dead();

    // Hero amor regenertation
    protected virtual void AmorRegeneration()
    {
        if (canAmorRegen)
        {
            if (statsController.CurrentAmor < statsController.MaxAmor)
            {
                statsController.CurrentAmor += 5f * Time.deltaTime;
                if (statsController.CurrentAmor >= statsController.MaxAmor) canAmorRegen = false;
            }
        }
    }

    protected virtual IEnumerator AmorRegenCountDown()
    {
        yield return new WaitForSeconds(10f);
        canAmorRegen = true;
    }

    // Hero skill handle
    protected abstract void HandleSkill1();
    protected abstract void HandleSkill2();
    protected abstract void HandleSkill3();
    protected virtual IEnumerator ResetSkill(float skillCooldown, int flag)
    {
        // Check if the hero has a CooldownReduction stat.
        // If yes, reduce the skill cooldown based on the percentage of cooldown reduction.
        // If not, keep the original cooldown.
        float coolDown = (statsController.CooldownReduction != 0f)
            ? skillCooldown - (skillCooldown * statsController.CooldownReduction / 100)
            : skillCooldown;
        yield return new WaitForSeconds(coolDown);

        // Reactivate the correct skill based on the given flag
        switch (flag)
        {
            case 1:
                canUseSkill1 = true;
                break;
            case 2:
                canUseSkill2 = true;
                break;
            case 3:
                canUseSkill3 = true;
                break;
            default:
                Debug.LogWarning($"[ResetSkill] Unexpected flag value: {flag}");
                break;
        }
    }

    // Hero level handle    
    protected void GainExp(int expValue)
    {
        // Check if the given exp value is valid (must be greater than 0).
        if (expValue <= 0) Debug.LogWarning($"Invalid value for Exp gem: {expValue}");

        // Add the exp value to the hero's current Exp. 
        statsController.Exp += expValue;

        // Check if the hero has reached the required Exp to trigger the Level up function.
        if (statsController.Exp >= statsController.ExpRequire) LevelUp();
    }
    protected void LevelUp()
    {
        // Increase hero level
        statsController.Level++;

        // Update required Exp for the next level
        statsController.ExpRequire += statsController.Level * 30;

        if (statsController.Exp == statsController.ExpRequire)
        {
            // Reset current Exp after leveling up
            statsController.Exp = 0;
        }
        else
        {
            // Add the remain exp after leveling up
            int remainExp = statsController.Exp - statsController.ExpRequire;
            statsController.Exp = remainExp;
        }
    }
}

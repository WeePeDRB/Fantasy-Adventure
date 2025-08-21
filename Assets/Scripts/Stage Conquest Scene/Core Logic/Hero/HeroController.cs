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
    // Skill 2
    [SerializeField] protected HeroSkill skill2;
    protected bool canUseSkill2;
    // Skill 3
    [SerializeField] protected HeroSkill skill3;
    protected bool canUseSkill3;

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
    protected virtual void HandleMovement()
    {
        // Hero moving
        if (behaviorState == HeroBehaviorState.Moving)
        {
            // Get value from input system
            Vector2 inputVector = GameInput.GetMovementVectorNormalized();
            Vector3 moveDirVector = new Vector3(inputVector.x, 0, inputVector.y);

            // Hero movement
            transform.position += moveDirVector * statsController.Speed * Time.deltaTime;

            // Hero rotate
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirVector, Time.deltaTime * rotateSpeed);
        }
    }

    // Hero damage-taking logic
    protected virtual void Hurt(float damageTaken)
    {
        // Check if hero still alive
        if (healthState == HeroHealthState.Alive)
        {
            float damageAfterResistance = 0;
            float damageLeft = damageTaken;

            // Calculate damange taken  
            // Take damage when current amor > incomming damange 
            if (statsController.CurrentAmor >= damageTaken)
            {
                statsController.CurrentAmor -= damageTaken;
            }
            else
            {
                // If current amor < incomming damange 
                if (statsController.CurrentAmor > 0)
                {
                    // Calculate the final damage dealt after deducting the Hero's armor.
                    damageLeft -= statsController.CurrentAmor;
                    statsController.CurrentAmor = 0;
                }

                // Calculate the remaining damage after applying the Hero's resistance stat.
                damageAfterResistance = damageLeft - (damageLeft * statsController.Resistance / 100f);

                // Apply damange
                statsController.CurrentHealth -= damageAfterResistance;

                // Check if hero's health reach 0 -> Dead
                if (statsController.CurrentHealth == 0) Dead();
            }

            // Set amor regen to false
            canAmorRegen = false;

            // Check if there is already a coroutine
            // -> Stop the count down coroutine to reset it
            if (regenCooldownCoroutine != null) StopCoroutine(regenCooldownCoroutine);

            // Reset the amor regen countdown coroutine
            regenCooldownCoroutine = StartCoroutine(AmorRegenCountDown());
        }
    }

    protected virtual void Dead()
    {
        // Set health state to "Dead"
        healthState = HeroHealthState.Dead;

        // Invoke death event to execute related logic
        OnDead?.Invoke();

        // Disable physics system
        rigidBody.useGravity = false;
        bodyCollider.enabled = false;
    }

    // Hero amor regenertation
    protected virtual void AmorRegeneration()
    {
        if (canAmorRegen)
        {
            if (statsController.CurrentAmor < statsController.MaxAmor)
            {
                statsController.CurrentAmor += 5f * Time.deltaTime;
                if (statsController.CurrentAmor > statsController.MaxAmor)
                {
                    canAmorRegen = false;
                    statsController.CurrentAmor = statsController.MaxAmor;
                }
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
    public void ReturnToNormalState()
    {
        // Reset behavior state
        behaviorState = HeroBehaviorState.Moving;
        Debug.Log("return moving");
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

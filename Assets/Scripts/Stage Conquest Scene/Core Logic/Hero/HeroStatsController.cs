using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class HeroStatsController
{
    // Hero health: the hero health point 
    protected float maxHealth; // Maximum health
    protected float currentHealth; // Current health
    public float MaxHealth
    {
        get { return currentHealth; }
        set { maxHealth = Mathf.Max(value, 50f); }
    }
    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0f, maxHealth); }
    }

    // Hero amor: Armor absorbs incoming damage instead of Health.  
    // Once Armor reaches 0, any further damage will be deducted from Health.
    protected float maxAmor;
    protected float currentAmor;
    public float CurrentAmor
    {
        get { return currentAmor; }
        set { currentAmor = Mathf.Clamp(value, 0f, maxAmor); }
    }
    public float MaxAmor
    {
        get { return maxAmor; }
        set { maxAmor = Mathf.Clamp(value, 0f, maxAmor); }
    }



    // Hero speed:determines how fast the hero can move across the map
    protected float speedBase; // Hero base speed
    protected float speedAddition; // This stat will be directly affected by external factors such as buffs, debuffs, blessings, etc.
    public float SpeedBase
    {
        get { return speedBase; }
        set { speedBase = Mathf.Clamp(value, 1f, 10f); }
    }
    public float SpeedAddition
    {
        get { return speedAddition; }
        set { speedAddition = Mathf.Clamp(value, -10f, 10f); }
    }
    public float Speed  // This will be the stat used for movement-related logic (calculated from speedBase and speedAddition). 
                        // The reason for this is to prevent Speed from being directly affected by external factors, which could cause logic errors.
    {
        get
        {
            float value = speedBase + speedAddition;
            return Mathf.Max(value, 1f, 10f);
        }
    }

    // Hero resistance: reflects the amount of incoming damage that will be reduced when the Hero takes damage.
    protected float resistanceBase;
    protected float resistanceAddition;
    public float ResistanceBase
    {
        get { return resistanceBase; }
        set { resistanceBase = Mathf.Clamp(value, 0f, 100f); }
    }
    public float ResistanceAddition
    {
        get { return resistanceAddition; }
        set { resistanceAddition = Mathf.Clamp(value, -100f, 100f); }
    }
    public float Resistance
    {
        get
        {
            float value = resistanceBase + resistanceAddition;
            return Mathf.Clamp(value, -40f, 100f);
        }
    }

    // Hero damage amplifier: the amount of additional damage applied when the Hero lands a critical hit.
    protected float damageAmplifierBase;
    protected float damageAmplifierAddition;
    public float DamageAmplifierBase
    {
        get { return damageAmplifierBase; }
        set { damageAmplifierBase = Mathf.Clamp(value, 0f, 20f); }
    }
    public float DamageAmplifierAddition
    {
        get { return damageAmplifierAddition; }
        set { damageAmplifierAddition = Mathf.Max(value, -100f, 100f); }
    }
    public float DamageAmplifier
    {
        get
        {
            float value = damageAmplifierBase + damageAmplifierAddition;
            return Mathf.Clamp(value, -50f, 100f);
        }
    }

    // Hero cooldown reduction: this stat reflects the amount of time reduced from the Hero's skill cooldowns.
    protected float cooldownReductionBase;
    protected float cooldownReductionAddition;
    public float CooldownReductionBase
    {
        get { return cooldownReductionBase; }
    }
    public float CooldownReductionAddition
    {
        get { return cooldownReductionAddition; }
    }
    public float CooldownReduction
    {
        get
        {
            float value = cooldownReductionBase + cooldownReductionAddition;
            return Mathf.Clamp(value, 0f, 40f);
        }
    }

    // Hero attack speed: determines how quickly the Hero can perform attacks with their weapons. 
    // A higher value reduces the delay between consecutive attacks.
    protected float attackSpeedBase;
    protected float attackSpeedAddition;
    public float AttackSpeedBase
    {
        get { return attackSpeedBase; }
        set { attackSpeedBase = Mathf.Clamp(value, 0f, 5f); }
    }
    public float AttackSpeedAddition
    {
        get { return attackSpeedAddition; }
        set { attackSpeedAddition = Mathf.Clamp(value, 0f, 5f); }
    }
    public float AttackSpeed
    {
        get
        {
            float value = attackSpeedBase + attackSpeedAddition;
            return Mathf.Clamp(value, 0f, 5f);
        }
    }

    // Hero critical chance: the probability that the Hero's attack will deal critical damage.
    protected float criticalChanceBase;
    protected float criticalChanceAddition;
    public float CriticalChanceBase
    {
        get { return criticalChanceBase; }
        set { criticalChanceBase = Mathf.Clamp(value, 0f, 100f); }
    }
    public float CriticalChanceAddition
    {
        get { return criticalChanceAddition; }
        set { criticalChanceAddition = Mathf.Clamp(value, -100f, 100f); }
    }
    public float CriticalChance
    {
        get
        {
            float value = criticalChanceBase + criticalChanceAddition;
            return Mathf.Clamp(value, -100f, 100f);
        }
    }

    // Hero level
    protected int level;
    public int Level
    {
        get { return level; }
        set { level = Math.Max(1, value); }
    }

    protected int exp;
    protected int expRequire;
    public int Exp
    {
        get { return exp; }
        set { exp = Math.Max(0, value); }
    }
    public int ExpRequire
    {
        get { return expRequire; }
        set { expRequire = Math.Max(0, value); }
    }

    // Initialize data
    public HeroStatsController(SO_HeroStats statsData)
    {
        // Stats
        maxHealth = statsData.maxHealth;
        currentHealth = maxHealth;

        maxAmor = statsData.maxAmor;
        currentAmor = maxAmor;

        speedBase = statsData.speed;

        resistanceBase = statsData.resistance;

        damageAmplifierBase = statsData.damageAmplifier;

        cooldownReductionBase = statsData.cooldownReduction;

        attackSpeedBase = statsData.attackSpeed;

        criticalChanceBase = statsData.criticalChance;

        // Info
        level = 1;
        exp = 0;
        expRequire = 30;
    }
}


using System;
using UnityEngine;

public class MonsterStatsController
{
    // Monster health
    private float maxHealth; // Maximum health
    private float currentHealth; // Current health
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

    // Monster speed
    private float speedBase;
    private float speedAddition;
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
    public float Speed
    {
        get
        {
            float value = speedBase + speedAddition;
            return Mathf.Max(value, 1f, 10f);
        }
    }

    // Monster resistance
    private float resistanceBase;
    private float resistanceAddition;
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

    // Monster attack speed
    private float attackSpeedBase;
    private float attackSpeedAddition;
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

    // Monster attack damage
    private float attackDamageBase;
    private float attackDamageAddition;
    public float AttackDamageBase
    {
        get { return attackDamageBase; }
        set { attackDamageBase = MathF.Max(0f, value); }
    }
    public float AttackDamageAddition
    {
        get { return attackDamageAddition; }
        set { attackDamageAddition = MathF.Max(0f, value); }
    }
    public float AttackDamage
    {
        get
        {
            float value = attackDamageBase + attackDamageAddition;
            return MathF.Max(0f, value);
        }
    }

    // Monster level
    private int level;
    public int Level
    {
        get { return level; }
        set { level = Math.Max(1, value); }
    }

    // Initialize data
    public MonsterStatsController(SO_MonsterStats statsData, int stage = 1)
    {
        // Info
        level = stage;

        // Stats
        maxHealth = statsData.maxHealth;
        currentHealth = maxHealth;

        speedBase = statsData.speed;

        resistanceBase = statsData.resistance;

        attackSpeedBase = statsData.attackSpeed;

    }
}

using System;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public abstract class CharacterStatsOld
{
    //
    // FIELDS
    //

    // Basic stats
    protected float maxHealth; // Maximum health 
    protected float health; // Current health
    protected int level; // Character level
    protected float speedBase; // Movement speed
    protected float speedAddition;

    // Special stats
    // This stat will block a percentage of the damage received from monster, with a value ranging from 1 to 100.
    protected float resistanceBase;
    protected float resistanceAddition;

    // This stat increases the damage dealt by weapons
    protected float damageAmplifierBase;
    protected float damageAmplifierAddition;

    //
    // PROPERTIES
    //

    // Basic stats
    public float Health
    {
        get { return health; }
        set { health = Mathf.Clamp(value, 0f, maxHealth); } // Ensure health is within the range [0, maxHealth]
    }
    
    public int Level
    {
        get { return level; }
        set { level = Mathf.Max(1, value); } // Ensure level is at least 1
    }

    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = Mathf.Max(value, 50f); }
    }

    public float Speed
    {
        get
        {
            float value = speedBase + speedAddition;
            return Mathf.Max(1, value);
        }
    }
    public float SpeedBase
    {
        get { return speedBase; }
        set { speedBase = Mathf.Max(1, value); }
    }
    public float SpeedAddition
    {
        get { return speedAddition; }
        set { speedAddition = Mathf.Max(0,value); }
    }

    // Special stats
    public float Resistance
    {
        get
        {
            float value = resistanceBase + resistanceAddition;
            return value;
        }
    }
    public float ResistanceBase
    {
        get { return resistanceBase; }
        set { resistanceBase = Mathf.Clamp(value, 0f, 100f); }
    }
    public float ResistanceAddition
    {
        get { return resistanceAddition; }
        set { resistanceAddition = Mathf.Clamp(value, 0f, 100f); }
    }

    public float DamageAmplifier
    {
        get
        {
            float value = damageAmplifierBase + damageAmplifierAddition;
            return Math.Max(value, 0f);
        }

    }
    public float DamageAmplifierBase
    {
        get { return damageAmplifierBase; }
        set { damageAmplifierBase = Mathf.Max(value, 0f); }
    }
    public float DamageAmplifierAddition
    {
        get { return damageAmplifierAddition; }
        set { damageAmplifierAddition = Mathf.Max(value, 0f); }
    }
}

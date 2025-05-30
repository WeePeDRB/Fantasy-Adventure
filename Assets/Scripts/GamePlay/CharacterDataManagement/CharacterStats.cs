using System;
using UnityEngine;

[Serializable]
public abstract class CharacterStats
{
    //
    // FIELDS
    //

    // Basic stats
    protected float maxHealth; // Maximum health 
    protected float health; // Current health
    protected float speed; // Movement speed
    protected int level; // Character level

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
    public float Speed
    {
        get { return speed; }
        set { speed = Mathf.Max(0f, value); } // Ensure speed can't be negative
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

    // Special stats
    public float Resistance
    {
        get
        {
            float value = resistanceBase + resistanceAddition;
            return Mathf.Clamp(value, 0f, 100f);
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

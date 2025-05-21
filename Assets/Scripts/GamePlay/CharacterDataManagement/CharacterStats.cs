using UnityEngine;

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
    protected float resistance; // This stat will block a percentage of the damage received from monster, with a value ranging from 1 to 100.
    protected float damageAmplifier; // This stat increases the damage dealt by weapons

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
        set { maxHealth = Mathf.Max(value, 50f);}
    }

    // Special stats
    public float Resistance
    {
        get { return resistance; }
        set { resistance = Mathf.Clamp(value, 0f, 100f); } // Ensure resistance is between 0% and 100%
    }
    public float DamageAmplifier
    {
        get { return damageAmplifier; }
        set { damageAmplifier = Mathf.Max(0f, value); } // Ensure damage amplifier can't be negative
    } 
}

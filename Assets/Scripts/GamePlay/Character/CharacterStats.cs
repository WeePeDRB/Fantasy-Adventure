using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    //
    //  FIELDS
    //

    private float maxHealth; // Maximum health 
    private float health; // Current health
    private float speed; // Movement speed
    private float maxAmor; // Maximum amor
    private float amor; // Current amor
    private int level; // Character level

    private float resistance; // This stat will block a percentage of the damage received by the player, with a value ranging from 1 to 100.
    private float abilityHaste; // This stat represents the percentage of time reduced for skill cooldowns.
    private float damageAmplifier; // This stat increases the damage dealt by weapons



    //
    // CONSTRUCTOR
    //

    public CharacterStats(  float instantiateMaxHealth, float instantiateSpeed, 
                            float instantiateMaxAmor, int instantiateLevel)
    {
        maxHealth = instantiateMaxHealth;
        health = maxHealth;
        speed = instantiateSpeed;
        maxAmor = instantiateMaxAmor;
        amor = maxAmor;
        level = instantiateLevel;

        // Initialize special stats
        resistance = 0f;
        abilityHaste = 0f;
        damageAmplifier = 0f;
    }



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
    }
    public float MaxAmor
    {
        get { return maxAmor; }
    }

    // Special stats
    public float Resistance
    {
        get { return resistance; }
        set { resistance = Mathf.Clamp(value, 0f, 100f); } // Ensure resistance is between 0% and 100%
    }
    public float AbilityHaste
    {
        get { return abilityHaste; }
        set { abilityHaste = Mathf.Max(0f, value); } // Ensure ability haste can't be negative
    }
    public float DamageAmplifier
    {
        get { return damageAmplifier; }
        set { damageAmplifier = Mathf.Max(0f, value); } // Ensure damage amplifier can't be negative
    }



    //
    // FUNCTIONS
    //

    // MODIFY CHARACTER STATS
    // Modify health (using setter)
    public void ModifyHealth(float healthValue)
    {
        Health += healthValue; // Uses the property setter, ensuring health is within valid range
    }

    // Modify speed (using setter)
    public void ModifySpeed(float speedValue)
    {
        Speed += speedValue; // Uses the property setter
    }

    // Modify resistance (using setter)
    public void ModifyResistance(float resistanceValue)
    {
        Resistance += resistanceValue; // Uses the property setter
    }

    // Modify ability haste (using setter)
    public void ModifyAbilityHaste(float abilityHasteValue)
    {
        AbilityHaste += abilityHasteValue; // Uses the property setter
    }

    // Modify damage (using setter)
    public void ModifyDamage(float damageAmplifierValue)
    {
        DamageAmplifier += damageAmplifierValue; // Uses the property setter
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStats : CharacterStats
{
    //
    //  FIELDS
    //

    // Basic stats
    private float maxAmor; // Maximum amor
    private float amor; // Current amor


    // Special stats
    private float abilityHaste; // This stat represents the percentage of time reduced for skill cooldowns.

    //
    // CONSTRUCTOR
    //
    public HeroStats(   float MaxHealth,    float Speed,            int Level,          float MaxAmor,
                        float Resistance,   float DamageAmplifier,  float AbilityHaste                  )
    {
        // Instantiate basic stats
        maxHealth = MaxHealth;
        health = maxHealth;
        speed = Speed;
        level = Level;
        maxAmor = MaxAmor;
        amor = maxAmor;
        // Instantiate special stats
        resistance = Resistance;
        damageAmplifier = DamageAmplifier;
        abilityHaste = AbilityHaste;
    }

    //
    // PROPERTIES 
    //

    // Basic stats
    public float MaxAmor
    {
        get { return maxAmor; }
    }
    public float Amor
    {
        get { return amor; }
        set { amor = Mathf.Clamp(value, 0f, maxAmor); } // Ensure amor is within the range [0, maxAmor]
    }

    // Special stats
    public float AbilityHaste
    {
        get { return abilityHaste; }
        set { abilityHaste = Mathf.Max(0f, value); } // Ensure ability haste can't be negative
    }
}

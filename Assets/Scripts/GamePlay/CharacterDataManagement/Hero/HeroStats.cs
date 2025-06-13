using System;
using UnityEngine;

[Serializable]
public class HeroStats : CharacterStats
{
    //
    //  FIELDS
    //
    
    // Basic stats
    private float maxAmor; // Maximum amor
    private float amor; // Current amor
    private int expRequire; // Experience require to level up
    private int exp; // Current experience


    // Special stats
    private float abilityHaste; // This stat represents the percentage of time reduced for skill cooldowns.

    //
    // CONSTRUCTOR
    //
    public HeroStats(SO_Hero heroData)
    {
        // Instantiate basic stats
        maxHealth = heroData.maxHealth;
        health = maxHealth;
        speed = heroData.speed;
        level = heroData.level;
        maxAmor = heroData.maxAmor;
        amor = maxAmor;
        expRequire = 50;
        exp = 0;
        // Instantiate special stats
        resistanceBase = heroData.resistance;
        damageAmplifierBase = heroData.damageAmplifier;
        abilityHaste = heroData.abilityHaste;
    }

    //
    // PROPERTIES 
    //

    // Basic stats
    public float MaxAmor
    {
        get { return maxAmor; }
        set { maxAmor = Mathf.Clamp(value, 50f, maxAmor);}
    }
    public float Amor
    {
        get { return amor; }
        set { amor = Mathf.Clamp(value, 0f, maxAmor); } // Ensure amor is within the range [0, maxAmor]
    }
    public int ExpRequire
    {
        get { return expRequire; }
        set { expRequire = Math.Max(0,value); }
    }
    public int Exp
    {
        get { return exp; }
        set { exp = Math.Max(0,value); }
    }
    // Special stats
    public float AbilityHaste
    {
        get { return abilityHaste; }
        set { abilityHaste = Mathf.Max(0f, value); } // Ensure ability haste can't be negative
    }
}

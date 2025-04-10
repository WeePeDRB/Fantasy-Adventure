using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats 
{
    //
    // FIELDS
    //

    //Basic stats
    protected float damage; // Monster damage
    protected float maxHealth; // Maximum health;
    protected float health; // Current health
    protected float speed; // Movement speed
    protected float attackSpeed; // Attack speed
    protected int level; // Monster level
    
    // Special stats
    protected float resistance; // This stat will block a percentage of the damage received from player, with a value ranging from 1 to 100.


    //
    // PROPERTIES 
    //

    // Basic stats
    public float Damage
    {
        get { return damage; }
    
    }

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

    public float MaxHealth
    {
        get { return maxHealth; }
    }

    public float AttackSpeed
    {
        get { return attackSpeed; }
    }

    // Special stats
    public float Resistance
    {
        get { return resistance; }
        set { resistance = Mathf.Clamp(value, 0f, 100f); } // Ensure resistance is between 0% and 100%
    }



    // 
    // FUNCTIONS
    //


    public void  InitialMonsterStats(    float damage,       float maxHealth,    float speed, 
                                        float attackSpeed,  float resistance,   int level )
    {
        this.level = level;
        this.damage = damage + level * 5;
        this.maxHealth = maxHealth + level * 50;
        this.health = maxHealth;
        this.speed = speed;
        this.attackSpeed = attackSpeed;
        this.resistance = resistance;
    }
    // // MODIFY CHARACTER STATS
    //  // Modify health (using setter)
    // public void ModifyHealth(float healthValue)
    // {
    //     Health += healthValue; // Uses the property setter, ensuring health is within valid range
    // }

    // // Modify speed (using setter)
    // public void ModifySpeed(float speedValue)
    // {
    //     Speed += speedValue * speed / 100; // Uses the property setter
    // }
}

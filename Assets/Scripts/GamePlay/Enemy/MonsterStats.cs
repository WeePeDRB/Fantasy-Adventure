using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats 
{
    //
    // FIELDS
    //

    protected float maxHealth; // Maximum health;
    protected float health; // Current health
    protected float speed; // Movement speed
    protected float attackSpeed; // Attack speed
    
    // Special stats
    protected float resistance; // This stat will block a percentage of the damage received by the player, with a value ranging from 1 to 100.




    //
    // CONSTRUCTOR
    //

    public MonsterStats(    float maxHealth,    float speed, 
                            float attackSpeed,  float resistance )
    {
        this.maxHealth = maxHealth;
        this.speed = speed;
        this.attackSpeed = attackSpeed;
        this.resistance = resistance;
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

    public float MaxHealth
    {
        get { return maxHealth; }
    }

}

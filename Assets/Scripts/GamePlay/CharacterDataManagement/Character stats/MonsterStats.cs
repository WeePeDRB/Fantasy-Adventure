using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : CharacterStats
{
    //
    // FIELDS
    //

    //Basic stats
    private float damage; // Monster damage
    private float attackSpeed; // Attack speed
    private int expDrop;

    //
    // CONSTRUCTOR
    //
    public MonsterStats (   float MaxHealth,     float Speed,       int Level,     float Damage,
                            float AttackSpeed,   float Resistance,  float DamageAmplifier)
    {
        // Instantiate basic stats
        maxHealth = MaxHealth;
        health = maxHealth;
        speed = Speed;
        level = Level;
        damage = Damage;
        attackSpeed = AttackSpeed;

        // Instantiate special stats
        resistance = Resistance;
        damageAmplifier = DamageAmplifier;
    }

    //
    // PROPERTIES 
    //

    // Basic stats
    public float Damage
    {
        get { return damage; }
    }
    public float AttackSpeed
    {
        get { return attackSpeed; }
    }
    public int ExpDrop
    {
        get { return expDrop; }
    }
    // 
    // FUNCTIONS
    //

    public void  LevelUp( int Level )
    {
        level = Level;
        damage = damage + level * 5;
        maxHealth = maxHealth + level * 50;
        health = maxHealth;
    }
}

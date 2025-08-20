using System;

[Serializable]
public class MonsterStats : CharacterStatsOld
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
    public MonsterStats(SO_Monster monsterData)
    {
        // Instantiate basic stats
        maxHealth = monsterData.maxHealth;
        health = maxHealth;
        speedBase = monsterData.speed;
        level = monsterData.level;
        damage = monsterData.damage;
        attackSpeed = monsterData.attackSpeed;

        // Instantiate special stats
        resistanceBase = monsterData.resistance;
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

    public void LevelUp(int level)
    {
        this.level = level;
        damage = damage + level * 5;
        maxHealth = maxHealth + level * 50;
        health = maxHealth;
    }
}

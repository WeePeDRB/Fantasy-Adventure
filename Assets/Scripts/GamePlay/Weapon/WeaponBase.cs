using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    //
    // FIELDS
    //

    // Essential data
    protected string id;
    protected int weaponLevel;
    protected float weaponAttackDamage;
    protected float weaponAttackSpeed;

    //
    protected HeroBaseController heroBaseController;

    //
    // PROPERTIES
    //
    public string ID { get { return id; } }
    public int WeaponLevel { get { return weaponLevel; } }
    public float WeaponAttackDamage { get { return weaponAttackDamage; } }
    public float WeaponAttackSpeed { get { return weaponAttackSpeed; } }

    // 
    // FUNCTIONS
    //

    // Initialize stats for weapon
    public virtual void InitializeWeapon(SO_Weapon weaponData)
    {
        id = weaponData.id;
        weaponAttackSpeed = weaponData.weaponAttackSpeed;
        weaponAttackDamage = weaponData.weaponDamage;
        weaponLevel = weaponData.weaponLevel;
    }

    // Weapon level up
    public virtual void WeaponLevelUp()
    {
        if (weaponLevel < 5)
        {
            weaponLevel++;
            WeaponPowerUp();
        }
    }
    public abstract void WeaponPowerUp();


    // WEAPON ATTACK LOGIC
    public abstract IEnumerator AttackCoroutine();
    
}

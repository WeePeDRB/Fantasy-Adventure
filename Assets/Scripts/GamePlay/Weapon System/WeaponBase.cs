using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBaseOld : MonoBehaviour
{
    //
    // FIELDS
    //

    // Essential data
    protected string id;
    protected string weaponName;
    protected string weaponDescription;
    protected int weaponLevel;
    protected float weaponAttackDamage;
    protected float weaponAttackSpeed;
    protected Sprite weaponSprite;

    //
    protected HeroBaseController heroBaseController;

    //
    // PROPERTIES
    //
    public string ID { get { return id; } }
    public string WeaponName { get { return weaponName; }}
    public string WeaponDescription { get { return weaponDescription; }}
    public int WeaponLevel { get { return weaponLevel; } }
    public float WeaponAttackDamage { get { return weaponAttackDamage; } }
    public float WeaponAttackSpeed { get { return weaponAttackSpeed; } }
    public Sprite WeaponSprite { get { return weaponSprite; }}

    // 
    // FUNCTIONS
    //

    // Initialize data for weapon
    public virtual void InitializeWeapon(SO_Weapon weaponData)
    {
        id = weaponData.id;
        weaponName = weaponData.weaponName;
        weaponDescription = weaponData.weaponDescription;
        weaponLevel = weaponData.weaponLevel;
        weaponAttackSpeed = weaponData.weaponAttackSpeed;
        weaponAttackDamage = weaponData.weaponDamage;
        weaponSprite = weaponData.weaponSprite;
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

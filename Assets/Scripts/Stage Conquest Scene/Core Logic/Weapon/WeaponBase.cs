using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    // Weapon data
    protected string id;
    public string ID { get { return id; } }

    protected string weaponName;
    public string WeaponName { get { return weaponName; } }

    protected string weaponDescription;
    public string WeaponDescription { get { return weaponDescription; } }

    protected int weaponLevel;
    public int WeaponLevel { get { return weaponLevel; } }
    protected int weaponMaxLevel;

    protected float weaponAttackDamage;
    public float WeaponAttackDamage { get { return weaponAttackDamage; } }

    protected float weaponAttackSpeed;
    public float WeaponAttackSpeed { get { return weaponAttackSpeed; } }

    protected Sprite weaponSprite;
    public Sprite WeaponSprite { get { return weaponSprite; } }

    // Initialize data for weapon
    public void InitializeWeapon(SO_Weapon weaponData)
    {
        id = weaponData.id;
        weaponName = weaponData.name;
        weaponDescription = weaponData.weaponDescription;
        weaponLevel = weaponData.weaponLevel;
        weaponAttackSpeed = weaponData.weaponAttackSpeed;
        weaponAttackDamage = weaponData.weaponDamage;
        weaponSprite = weaponData.weaponSprite;
    }

    // Weapon level up
    public void WeaponLevelUp()
    {
        weaponLevel++;
        WeaponLevelUp();
    }

    // Check if weapon reach max level
    public bool IsMaxLevel()
    {
        if (weaponLevel >= weaponMaxLevel) return true;
        return false;
    }

    // Weapon power up
    public abstract void WeaponPowerUp();

    // Weapon attack
    protected abstract IEnumerator AttackCoroutine();
    protected abstract void ApplyDamage();
}

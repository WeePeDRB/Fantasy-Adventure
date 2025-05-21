using System.Collections.Generic;
using System;

[Serializable]
public class HeroWeaponSystem
{
    //
    // FIELDS
    //
    private Dictionary<string, WeaponBase> activeWeapons;
    private List<SO_Weapon> weaponList;

    //
    // CONSTRUCTOR
    // 

    public HeroWeaponSystem()
    {
        activeWeapons = new Dictionary<string, WeaponBase>();
        weaponList = new List<SO_Weapon>();
    }

    //
    // FUNCTIONS
    //

    // Dictionary logic
    public void ReceiveWeapon(SO_Weapon weaponData, WeaponBase weapon)
    {
        activeWeapons.Add(weaponData.id, weapon);
        weaponList.Add(weaponData);
    }

    public void WeaponLevelUp(SO_Weapon weaponData)
    {
        WeaponBase weapon = GetWeapon(weaponData);
        weapon.WeaponLevelUp();
    }

    // Weapon dictionary check
    public bool IsWeaponExist(SO_Weapon weaponData)
    {
        if (activeWeapons.ContainsKey(weaponData.id))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsWeaponQuantityMax()
    {
        if (weaponList.Count == 2) return true;
        return false;
    }

    // Get data
    public WeaponBase GetWeapon(SO_Weapon weaponData)
    {
        return activeWeapons[weaponData.id];
    }
    public List<SO_Weapon> GetWeaponList()
    {
        return weaponList;
    }
}

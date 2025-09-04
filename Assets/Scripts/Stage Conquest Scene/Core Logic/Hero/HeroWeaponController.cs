using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HeroWeaponController
{
    // Dictionary containing weapon with the key as the ID and the value as the weapon.
    private Dictionary<string, WeaponBase> activeWeapons;

    // Number of weapons currently owned
    private List<WeaponBase> weaponList = new List<WeaponBase>();

    // Maximum number of weapons that can be owned
    private int maxWeapon;

    // Receive weapon events
    public event Action<Weapon> OnReceiveWeapon;
    public event Action<Weapon> OnWeaponReachMaxLevel; // Hero weapon reach max level event
    public event Action<WeaponList> OnWeaponListFull; // Hero weapon list full event

    // Initialize data
    public HeroWeaponController()
    {
        activeWeapons = new Dictionary<string, WeaponBase>();
        weaponList = new List<WeaponBase>();
        maxWeapon = 4;
    }

    // Receive weapon
    public void ReceiveWeapon(SO_Weapon weaponData, bool checkingWeapon, GameObject weaponPre = null)
    {
        // Check if the weapon exists in the dictionary.
        // If yes -> Run the logic to level up a weapon.
        if (checkingWeapon)
        {
            // Get weapon
            WeaponBase weapon = GetWeapon(weaponData);

            // Weapon level up
            weapon.WeaponLevelUp();

            // Check if weapon reach max level 
            // If yes -> Invoke the weapon max level event
            if (weapon.IsMaxLevel()) OnWeaponReachMaxLevel?.Invoke(new Weapon { weapon = weapon });
        }
        // If no -> Initialize the weapon and add it to the dictionary.
        else
        {
            // Instantiate weapon game object from prefab
            GameObject weaponGO = weaponPre;

            // Try Get WeaponBase component from game object 
            // If positive -> Initialize weapon data and add to dictionary
            if (weaponGO.TryGetComponent(out WeaponBase weapon))
            {
                weapon.InitializeWeapon(weaponData);
                activeWeapons.Add(weaponData.id, weapon);
                weaponList.Add(weapon);

                // Check if weapon dictionary is full
                if (weaponList.Count >= maxWeapon)
                {
                    // Send weapon list to upgrade controller
                    OnWeaponListFull?.Invoke(new WeaponList { weaponList = weaponList });
                }
            }
            // If negative -> Log warning
            else
            {
                Debug.LogWarning("The weapon prefab don't have WeaponBase component !");
            }
        }

        // Invoke the weapon receive event to synchronize the logic.
        OnReceiveWeapon?.Invoke(new Weapon { });
    }

    // Check if weapon exist in dictionary
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

    // Get weapon
    private WeaponBase GetWeapon(SO_Weapon weaponData)
    {
        return activeWeapons[weaponData.id];
    }

}

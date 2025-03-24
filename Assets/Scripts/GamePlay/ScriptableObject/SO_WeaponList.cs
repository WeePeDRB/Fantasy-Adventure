using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SO_WeaponList : ScriptableObject
{
    // SO_Weapon list
    public List<SO_Weapon> weaponDataList;

    public SO_Weapon GetWeaponByID(int id)
    {
        return weaponDataList.Find(weapon => weapon.id == id);
    }
}

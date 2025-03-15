using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class SO_Weapon : ScriptableObject
{
    public Transform weaponPrefab;
    public string weaponName;
    public int weaponLevel;
    public float weaponAttackSpeed;
    public float weaponDamage;
}

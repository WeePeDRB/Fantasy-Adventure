using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class SO_Weapon : ScriptableObject
{
    // Weapon private  id
    public int id;

    // Weapon projectile prefab for the instantiate
    public Transform projectilePrefab;

    // Weapon stats
    public int weaponLevel;
    public float weaponAttackSpeed;
    public float weaponDamage;
    
}

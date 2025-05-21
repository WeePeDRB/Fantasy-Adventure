using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class SO_Weapon : ScriptableObject
{
    // Weapon private  id
    public string id;

    // Weapon projectile prefab for the instantiate
    public GameObject weaponPrefab;

    // Essential data
    public Sprite weaponSprite;
    public string weaponName;
    public string weaponDescription;
    public int weaponLevel;
    public float weaponAttackSpeed;
    public float weaponDamage;
    
}

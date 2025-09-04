using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class SO_Weapon : ScriptableObject
{
    // Weapon data
    public string id;
    public string weaponName;
    public string weaponDescription;
    public int weaponLevel;
    public float weaponDamage;
    public float weaponAttackSpeed;
    public GameObject weaponPrefab;
    public Sprite weaponSprite;
    
}

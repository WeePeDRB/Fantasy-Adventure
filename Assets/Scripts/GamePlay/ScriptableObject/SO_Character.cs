using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SO_Character : ScriptableObject
{
    // Character private id
    public int id;

    // Character prefab for the instantiate
    public GameObject characterPrefab;

    // Character Basic Stats
    public float maxHealth;
    public float speed;
    public float maxAmor;
    public int level;
    public IWeapon primaryWeapon;
    public int maxWeapon;
    public int maxItem;

    // Skill cooldown for the 
    public float dashSkillCooldown;
    public float specialSkillCooldown;
    public float ultimateSkillCooldown;
}

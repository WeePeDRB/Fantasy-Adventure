using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SO_Character : ScriptableObject
{
    // Character private id
    public int id;

    // Character class
    public string characterClass;

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

    // Skill information
    public SO_CharacterSkill dashSkill;
    public SO_CharacterSkill specialSkill;
    public SO_CharacterSkill ultimateSkill;
}

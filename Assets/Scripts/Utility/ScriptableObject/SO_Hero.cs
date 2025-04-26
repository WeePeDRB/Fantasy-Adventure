using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SO_Hero : ScriptableObject
{
    // Hero private id
    public int id;

    // Hero Icon
    public Sprite heroPortrait;

    // Hero class
    public string heroClass;

    // Hero prefab for the instantiate
    public GameObject heroPrefab;

    // Hero Basic Stats
    public float maxHealth;
    public float speed;
    public float maxAmor;
    public int level;
    public int exp;
    public int expRequire;

    // Hero Special Stats
    public float resistance;
    public float abilityHaste;
    public float damageAmplifier;

    // Hero weapon data
    public IWeapon primaryWeapon;
    public int maxWeapon;
    public int maxItem;

    // Skill information
    public SO_HeroSkill dashSkill;
    public SO_HeroSkill specialSkill;
    public SO_HeroSkill ultimateSkill;
}

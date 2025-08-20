using UnityEngine;

[CreateAssetMenu()]
public class SO_Hero : ScriptableObject
{
    // Hero private id
    public string id;

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
    public SO_Weapon primaryWeapon;
    public int maxWeapon;
    public int maxItem;

    // Skill information
    public SO_HeroSkillOld dashSkill;
    public SO_HeroSkillOld specialSkill;
    public SO_HeroSkillOld ultimateSkill;
}

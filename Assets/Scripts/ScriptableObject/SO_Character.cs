using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SO_Character : ScriptableObject
{
    //
    public float maxHealth;
    public float speed;
    public float maxAmor;
    public float level;
    public IWeapon primaryWeapon;
    public int maxWeapon;
    public int maxItem;

    // Skill cooldown
    public float dashSkillCooldown;
    public float specialSkillCooldown;
    public float ultimateSkillCooldown;
}

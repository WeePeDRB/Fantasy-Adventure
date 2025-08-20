using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SO_HeroSkill : ScriptableObject
{
    public string skillName;
    public string skillDescription;
    public float skillCooldown;
    public Sprite skillIcon;
    public SkillType skillType;
    public SkillTargetType skillTargetType;
    public List<SkillCategory> skillCategory;
}

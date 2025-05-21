using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu()]
public class SO_HeroSkill : ScriptableObject
{
    // Skill icon
    public Sprite skillSprite;

    // Skill data
    public string skillName;
    public string skillDescription;
    public float skillCooldown;
    public SkillType skillType;
    public SkillTargetType skillTargetType;
    public SkillCategory skillCategory;
}

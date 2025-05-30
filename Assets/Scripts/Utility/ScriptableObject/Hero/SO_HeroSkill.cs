using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu()]
public class SO_HeroSkill : ScriptableObject
{
    // Skill data
    public string skillKeyword;
    public string skillName;
    public string skillDescription;
    public float skillCooldown;
    public Sprite skillSprite;
    public SkillType skillType;
    public SkillTargetType skillTargetType;
    public SkillCategory skillCategory;
}

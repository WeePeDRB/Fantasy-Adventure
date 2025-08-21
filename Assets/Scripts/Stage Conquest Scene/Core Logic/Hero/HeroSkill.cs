using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroSkill : MonoBehaviour
{
    // Skill data
    protected string skillKeyword;
    public string SkillKeyword { get { return skillKeyword; } }

    protected string skillName;
    public string SkillName { get { return skillKeyword; } }

    protected string skillDescription;
    public string SkillDescription { get { return skillKeyword; } }

    protected float skillCooldown;
    public float SkillCooldown { get { return skillCooldown; } }

    protected Sprite skillSprite;
    public Sprite SkillSprite { get { return skillSprite; } }

    protected SkillType skillType;
    public SkillType SkillType { get { return skillType; } }

    protected SkillTargetType skillTargetType;
    public SkillTargetType SkillTargetType { get { return skillTargetType; } }

    protected List<SkillCategory> skillCategory;
    public List<SkillCategory> SkillCategory { get { return skillCategory; } }

    // Initialize data
    public void InitializeData(SO_HeroSkill heroSkill)
    {
        if (heroSkill == null)
        {
            Debug.LogError("Skill data is null");
            return;
        }

        skillName = heroSkill.skillName;
        skillDescription = heroSkill.skillDescription;
        skillCooldown = heroSkill.skillCooldown;
        skillSprite = heroSkill.skillIcon;
        skillType = heroSkill.skillType;
        skillTargetType = heroSkill.skillTargetType;
        skillCategory = heroSkill.skillCategory;
    }

    // Activate skill
    public abstract void SkillActivate();
}

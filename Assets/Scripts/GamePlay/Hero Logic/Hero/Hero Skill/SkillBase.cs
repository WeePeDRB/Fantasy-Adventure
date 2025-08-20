using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SkillBase : MonoBehaviour
{
    //
    // FIELDS
    //
    protected string skillKeyword;
    protected string skillName;
    protected string skillDescription;
    protected float skillCooldown;
    protected Sprite skillSprite;
    protected SkillType skillType;
    protected SkillTargetType skillTargetType;
    protected SkillCategory skillCategory;

    //
    // PROPERTIES
    // 
    public string SkillKeyword { get { return skillKeyword; } }
    public string SkillName { get { return skillKeyword; } }
    public string SkillDescription { get { return skillKeyword; } }
    public float SkillCooldown { get { return skillCooldown; }}
    public Sprite SkillSprite { get { return skillSprite; }}
    public SkillType SkillType { get { return skillType; }}
    public SkillTargetType SkillTargetType { get { return skillTargetType; }}
    public SkillCategory SkillCategory { get { return skillCategory; }}

    //
    // FUNCTIONS
    //

    // Initialize skill
    public virtual void InitializeSkillData(SO_HeroSkillOld heroSkill)
    {
        if (heroSkill == null)
        {
            Debug.LogError("Skill data is null");
            return;
        }

        skillKeyword = heroSkill.skillKeyword;
        skillName = heroSkill.skillName;
        skillDescription = heroSkill.skillDescription;
        skillCooldown = heroSkill.skillCooldown;
        skillSprite = heroSkill.skillSprite;
        skillType = heroSkill.skillType;
        skillTargetType = heroSkill.skillTargetType;
        skillCategory = heroSkill.skillCategory;
    }

    protected abstract void InitializeSkillUniqueData();

    // Activate skill
    public abstract void SkillActivate();
}

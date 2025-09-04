using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcherSkill3 : HeroSkill
{
    // Acher reference
    private AcherController acherController;

    // Skill data
    [SerializeField] private SO_HeroSkill heroSkillData;

    // Special effect
    [SerializeField] private SO_SpecialEffect speedBoostData;
    private Se_SpeedBoost speedBoost;

    // Initialize data
    protected override void InitializeData(SO_HeroSkill heroSkill)
    {
        base.InitializeData(heroSkill);

        // Acher reference
        acherController = GetComponentInParent<AcherController>();

        // Initialize special effect
        speedBoost = new Se_SpeedBoost(speedBoostData);
    }
    public override void SkillActivate()
    {
        // Apply special effect
        acherController.SpecialEffectController.ReceiveEffect(speedBoost);
        
        // Invoke play VFX event
        PlaySkillVFX();
    }

    // 
    private void Start()
    {
        InitializeData(heroSkillData);

        // Skill 3 event
        acherController.OnUseSkill3 += SkillActivate;
    }
}

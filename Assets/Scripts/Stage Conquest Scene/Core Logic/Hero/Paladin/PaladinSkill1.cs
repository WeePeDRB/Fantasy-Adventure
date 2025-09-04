using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinSkill1 : HeroSkill
{
    // Paladin reference
    private PaladinController paladinController;

    // Skill data
    [SerializeField] private SO_HeroSkill heroSkillData;

    // Special effect
    [SerializeField] private SO_SpecialEffect resistanceBoostData;
    private Se_ResistanceBoost resistanceBoost;

    // Initialize data
    protected override void InitializeData(SO_HeroSkill heroSkill)
    {
        base.InitializeData(heroSkill);

        // Paladin reference
        paladinController = GetComponentInParent<PaladinController>();

        // Initialize special effects
        resistanceBoost = new Se_ResistanceBoost(resistanceBoostData);
    }

    public override void SkillActivate()
    {
        // Receive special effect
        paladinController.SpecialEffectController.ReceiveEffect(resistanceBoost);

        // Invoke play VFX event
        PlaySkillVFX();
    }

    //
    private void Start()
    {
        InitializeData(heroSkillData);
    }
}

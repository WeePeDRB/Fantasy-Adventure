using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcherSkill1 : HeroSkill
{
    // Acher reference
    private AcherController acherController;

    // Skill data
    [SerializeField] private SO_HeroSkill heroSkillData;

    // Special effect
    [SerializeField] private SO_SpecialEffect damageAmpBoostData;
    private Se_DamageAmpBoost damageAmpBoost;

    [SerializeField] private SO_SpecialEffect hyperDamageAmpBoostData;
    private Se_DamageAmpBoost hyperDamageAmpBoost;

    // Initialize data
    protected override void InitializeData(SO_HeroSkill heroSkill)
    {
        base.InitializeData(heroSkill);

        // Acher reference
        acherController = GetComponentInParent<AcherController>();

        // Initialize special effects
        damageAmpBoost = new Se_DamageAmpBoost(damageAmpBoostData);
        hyperDamageAmpBoost = new Se_DamageAmpBoost(hyperDamageAmpBoostData);
    }

    public override void SkillActivate()
    {
        // Receive special effect
        // Check for "Hyper instict" flag to customize the special effect appropriately
        if (!acherController.HyperInstict)
        {
            Debug.Log("Normal damage amp");
            acherController.SpecialEffectController.ReceiveEffect(damageAmpBoost);
        }
        else
        {
            Debug.Log("Special damage amp");
            acherController.SpecialEffectController.ReceiveEffect(hyperDamageAmpBoost);

        }
    }
    //
    private void Start()
    {
        InitializeData(heroSkillData);
    }
}

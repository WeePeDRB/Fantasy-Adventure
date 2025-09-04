using System;
using System.Collections.Generic;
using UnityEngine;

public class PaladinSkill3 : HeroSkill
{
    // Paladin reference
    private PaladinController paladinController;

    // Skill data
    [SerializeField] private SO_HeroSkill heroSkillData;

    // Special effect
    [SerializeField] private SO_SpecialEffect healthRegenData;
    private Se_HealthRegen healthRegen;
    [SerializeField] private SO_SpecialEffect resistanceBoostData;
    private Se_ResistanceBoost resistanceBoost;

    [SerializeField] private SO_SpecialEffect alliesResistanceBoostData;
    private Se_ResistanceBoost alliesResistanceBoost;

    // AOE interact
    private List<HeroController> herosInRange;

    // Initialize data
    protected override void InitializeData(SO_HeroSkill heroSkill)
    {
        base.InitializeData(heroSkill);

        // Paladin reference
        paladinController = GetComponentInParent<PaladinController>();

        // Initialize hero list
        herosInRange = new List<HeroController>();

        // Initialize special effecs
        healthRegen = new Se_HealthRegen(healthRegenData);
        resistanceBoost = new Se_ResistanceBoost(resistanceBoostData);
        alliesResistanceBoost = new Se_ResistanceBoost(alliesResistanceBoostData);
    }

    public override void SkillActivate()
    {
        // Apply effect on paladin
        paladinController.SpecialEffectController.ReceiveEffect(resistanceBoost);
        paladinController.SpecialEffectController.ReceiveEffect(healthRegen);

        // Apply effect on ally
        foreach (HeroController heroController in herosInRange)
        {
            // Receive resistance boost
            heroController.SpecialEffectController.ReceiveEffect(alliesResistanceBoost);
            // Receive health regen
            heroController.SpecialEffectController.ReceiveEffect(healthRegen);
        }

        // Invoke play VFX event
        PlaySkillVFX();
    }
    // Character list checking
    // Collider checking
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            HeroController heroController = collider.gameObject.GetComponent<HeroController>();
            herosInRange.Add(heroController);
            heroController.OnDead += OnHeroDead;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            HeroController heroController = collider.gameObject.GetComponent<HeroController>();
            herosInRange.Add(heroController);
            heroController.OnDead -= OnHeroDead;
        }
    }

    // Check if hero is dead
    private void OnHeroDead(HeroDead hero)
    {
        hero.heroController.OnDead -= OnHeroDead;

        for (int i = 0; i < herosInRange.Count; i++)
        {
            if (herosInRange[i] == hero.heroController)
            {
                herosInRange.Remove(herosInRange[i]);
                return;
            }
        }
    }

    //
    private void Start()
    {
        InitializeData(heroSkillData);
    }
}

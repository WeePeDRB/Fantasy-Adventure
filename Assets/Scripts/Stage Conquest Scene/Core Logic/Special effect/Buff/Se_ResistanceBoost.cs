using System;
using UnityEngine;

[System.Serializable]
public class Se_ResistanceBoost : SpecialEffectBase
{
    public Se_ResistanceBoost(SO_SpecialEffect specialEffectData)
    {
        id = specialEffectData.id;
        spEffectName = specialEffectData.spEffectName;
        spEffectDescription = specialEffectData.spEffectDescription;
        spEffectDuration = specialEffectData.spEffectDuration;
        spEffectTimeRemaining = spEffectDuration;
        spEffectValue = specialEffectData.spEffectValue;
        spEffectSprite = specialEffectData.spEffectSprite;
        spEffectType = specialEffectData.spEffectType;
        spEffectTarget = specialEffectData.spEffectTarget;
    }

    public Se_ResistanceBoost(SpecialEffectBase specialEffect)
    {
        id = specialEffect.ID;
        spEffectName = specialEffect.SpEffectName;
        spEffectDescription = specialEffect.SpEffectDescription;
        spEffectDuration = specialEffect.SpEffectDuration;
        spEffectTimeRemaining = specialEffect.SpEffectTimeRemaining;
        spEffectValue = specialEffect.SpEffectValue;
        spEffectSprite = specialEffect.SpEffectSprite;
        spEffectType = specialEffect.SpEffectType;
        spEffectTarget = specialEffect.SpEffectTarget;
    }

    // Clone
    public override SpecialEffectBase Clone(SpecialEffectBase specialEffect)
    {
        return new Se_ResistanceBoost(specialEffect);
    }

    // Apply effect to hero
    public override void ApplyEffectOnHero(HeroController heroController)
    {
        heroController.StatsController.ResistanceAddition += spEffectValue;
    }
    // Remove effect to hero
    public override void RemoveEffectFromHero(HeroController heroController)
    {
        if (heroController.StatsController.ResistanceAddition != 0) heroController.StatsController.ResistanceAddition -= spEffectValue;
    }

    // Apply effect to monster
    public override void ApplyEffectOnMonster(MonsterController monsterController)
    {

    }
    // Remove effect to monster
    public override void RemoveEffectFromMonster(MonsterController monsterController)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Se_SpeedBoost : SpecialEffectBase
{
    public Se_SpeedBoost(SO_SpecialEffect specialEffectData)
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

    public Se_SpeedBoost(SpecialEffectBase specialEffect)
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
        return new Se_SpeedBoost(specialEffect);
    }

    // Apply effect to hero
    public override void ApplyEffectOnHero(HeroController heroController)
    {
        float speedBoost = heroController.StatsController.Speed * spEffectValue / 100;
        heroController.StatsController.SpeedAddition += speedBoost;
    }
    // Remove effect to hero
    public override void RemoveEffectFromHero(HeroController heroController)
    {
        float speedBoost = heroController.StatsController.Speed * spEffectValue / 100;
        if (heroController.StatsController.SpeedAddition != 0) heroController.StatsController.SpeedAddition -= speedBoost;
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

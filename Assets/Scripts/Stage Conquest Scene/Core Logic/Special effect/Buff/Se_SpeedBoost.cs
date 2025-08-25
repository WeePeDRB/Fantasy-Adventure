using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Se_SpeedBoost : SpecialEffectBase
{
    //
    // CONSTRUCTOR
    //

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

    //
    // FUNCTIONS
    //

    // 
    public override void ApplyEffectOnHero(HeroController heroController)
    {
        float speedBoost = heroController.StatsController.Speed * spEffectValue / 100;
        heroController.StatsController.SpeedAddition += speedBoost;
    }
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

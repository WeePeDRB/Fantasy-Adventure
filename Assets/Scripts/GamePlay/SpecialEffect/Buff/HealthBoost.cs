using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : SpecialEffectBase
{
    private float healthIncrease;

    //
    // CONSTRUCTOR
    //
    public HealthBoost(SO_SpecialEffect specialEffectData)
    {
        id = specialEffectData.id;
        effectName = specialEffectData.specialEffectName;
        duration = specialEffectData.specialEffectDuration;
        timeRemaining = duration;
        effectTarget = specialEffectData.specialEffectTarget;
        healthIncrease = specialEffectData.specialEffectValue;
    }

    // Apply effect to hero
    public override void ApplyEffectOnHero(HeroBaseController hero)
    {
        hero.HeroStats.Health += healthIncrease;
    }
    // Remove effect to hero
    public override void RemoveEffectOnHero(HeroBaseController hero)
    {
        return;
    }

    // Apply effect to monster
    public override void ApplyEffectOnMonster(MonsterBaseController monster)
    {
        throw new System.NotImplementedException();
    }
    // Remove effect to monster
    public override void RemoveEffectOnMonster(MonsterBaseController monster)
    {
        throw new System.NotImplementedException();
    }
}

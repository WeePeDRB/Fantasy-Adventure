using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : SpecialEffectBase
{
    private float healthIncrease;

    public HealthBoost(float amount, float duration, EffectTarget effectTarget) 
    {
        effectName = "Health Boost";
        this.duration = duration;
        this.effectTarget = effectTarget;
        timeRemaining = duration;
        healthIncrease = amount;
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

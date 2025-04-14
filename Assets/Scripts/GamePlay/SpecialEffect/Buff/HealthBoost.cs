using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : SpecialEffectBase
{
    private float healthIncrease;

    public HealthBoost(float amount, float duration, EffectTarget effectTarget) 
    {
        effectName = "HealthBoost";
        this.duration = duration;
        this.effectTarget = effectTarget;
        timeRemaining = duration;
        healthIncrease = amount;
    }

    // Apply effect to character
    public override void ApplyEffectOnCharacter(HeroBaseController hero)
    {
        hero.heroStats.Health += healthIncrease;
    }

    // Apply effect to monster
    public override void ApplyEffectOnMonster(MonsterBaseController monster)
    {
        throw new System.NotImplementedException();
    }


}

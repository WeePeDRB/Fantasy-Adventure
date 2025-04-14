using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoost : SpecialEffectBase
{
    //
    // FIELDS
    //

    private float damageAmplifier;

    //
    // CONTRUCTOR
    //
    public DamageBoost(float amount, float duration, EffectTarget effectTarget)
    {
        effectName = "DamageBoost";
        this.duration = duration;
        this.effectTarget = effectTarget;
        timeRemaining = duration;
        damageAmplifier = amount;
    }

    //
    // FUNCTIONS
    //

    // Apply effect to player
    public override void ApplyEffectOnCharacter(HeroBaseController hero)
    {
        hero.heroStats.DamageAmplifier = damageAmplifier;
    }


    // Apply effect to monster
    public override void ApplyEffectOnMonster(MonsterBaseController monster)
    {
        throw new System.NotImplementedException();
    }

}

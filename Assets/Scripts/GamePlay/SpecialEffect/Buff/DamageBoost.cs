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

    // Apply effect to hero
    public override void ApplyEffectOnHero(HeroBaseController hero)
    {
        hero.HeroStats.DamageAmplifier = damageAmplifier;
    }
    // Remove effect to hero
    public override void RemoveEffectOnHero(HeroBaseController hero)
    {
        
        hero.HeroStats.DamageAmplifier = hero.HeroData.damageAmplifier;
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

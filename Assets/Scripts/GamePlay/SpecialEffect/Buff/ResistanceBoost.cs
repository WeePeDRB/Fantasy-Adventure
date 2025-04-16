using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistanceBoost : SpecialEffectBase
{
    //
    // FIELDS
    //

    private float resistance;

    //
    // CONTRUCTOR
    //
    public ResistanceBoost(float amount, float duration, EffectTarget effectTarget) 
    {
        effectName = "ResistanceBoost";
        this.duration = duration;
        this.effectTarget = effectTarget;
        timeRemaining = duration;
        resistance = amount;
    }

    //
    // FUNCTIONS
    // 

    // Apply effect to hero
    public override void ApplyEffectOnHero(HeroBaseController hero)
    {
        hero.heroStats.Resistance = resistance;
    }
    // Remove effect to hero
    public override void RemoveEffectOnHero(HeroBaseController hero)
    {
        hero.heroStats.Resistance = hero.HeroData.resistance;
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

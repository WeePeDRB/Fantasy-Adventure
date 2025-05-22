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
    public ResistanceBoost(SO_SpecialEffect specialEffectData)
    {
        id = specialEffectData.id;
        effectName = specialEffectData.specialEffectName;
        duration = specialEffectData.specialEffectDuration;
        timeRemaining = duration;
        effectTarget = specialEffectData.specialEffectTarget;
        resistance = specialEffectData.specialEffectValue;
    }

    //
    // FUNCTIONS
    // 

    // Apply effect to hero
    public override void ApplyEffectOnHero(HeroBaseController hero)
    {
        hero.HeroStats.Resistance = resistance;
    }
    // Remove effect to hero
    public override void RemoveEffectOnHero(HeroBaseController hero)
    {
        hero.HeroStats.Resistance = hero.HeroData.resistance;
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

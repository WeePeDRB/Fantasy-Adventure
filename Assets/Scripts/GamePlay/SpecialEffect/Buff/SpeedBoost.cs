using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : SpecialEffectBase
{
    //
    // CONSTRUCTOR
    //

    public SpeedBoost(SO_SpecialEffect specialEffectData)
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
    public override void ApplyEffectOnHero(HeroBaseController hero)
    {
        float speedBoost = hero.HeroStats.Speed * spEffectValue / 100; 
        hero.HeroStats.SpeedAddition += speedBoost;
    }
    public override void RemoveEffectOnHero(HeroBaseController hero)
    {
        float speedBoost = hero.HeroStats.Speed * spEffectValue / 100; 
        if (hero.HeroStats.SpeedAddition != 0) hero.HeroStats.SpeedAddition -= speedBoost;
    }

    //
    public override void ApplyEffectOnMonster(MonsterBaseController monster)
    {
        
    }

    public override void RemoveEffectOnMonster(MonsterBaseController monster)
    {
        throw new System.NotImplementedException();
    }
}

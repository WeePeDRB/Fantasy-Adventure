using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroEffectStatus : CharacterEffectStatus
{
    //
    // FIELD
    //
    public HeroBaseController hero;

    //
    // FUNCTION
    //

    public override void UpdateEffects(float deltaTime)
    {
        // Effect to remove list
        List<string> effectsToRemove = new List<string>();

        // 
        foreach (var effect in activeEffects.Values)
        {
            if (effect.TimeRemaining <= 0)
            {
                effectsToRemove.Add(effect.EffectName);
                effect.RemoveEffectOnHero(hero);
            }
            else
            {
                effect.UpdateTime(deltaTime);
                effect.ApplyEffectOnHero(hero);
            }
        }    

        //
        foreach (var effectName in effectsToRemove)
        {
            RemoveEffect(effectName);
        }
    }
}

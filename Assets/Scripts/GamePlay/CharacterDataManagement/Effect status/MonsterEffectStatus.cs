using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEffectStatus : CharacterEffectStatus
{
    //
    // FIELD
    //
    public MonsterBaseController monster;

    //
    // FUNCTION
    //
    
    public override void UpdateEffects(float deltaTime)
    {
        // Effect to remove list
        List<string> effectsToRemove = new List<string>();
        
        foreach (var effect in activeEffects.Values)
        {
            if (effect.TimeRemaining <= 0)
            {
                effectsToRemove.Add(effect.EffectName);
                effect.RemoveEffectOnMonster(monster);
            }
            else
            {
                effect.UpdateTime(deltaTime);
                effect.ApplyEffectOnMonster(monster);   
            }
        }    

        //
        foreach (var effectName in effectsToRemove)
        {
            RemoveEffect(effectName);
        }
    }
}

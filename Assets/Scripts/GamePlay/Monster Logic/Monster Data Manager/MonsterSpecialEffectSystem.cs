using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpecialEffectSystem : CharacterSpecialEffectSystem
{
    //
    // FIELD
    //
    public MonsterBaseControllerOld monster;

    //
    // FUNCTION
    //
    
    public override void ReceiveEffect(SpecialEffectBase effect)
    {

        // If an effect already exists in the dictionary, refresh its duration
        if (activeEffects.ContainsKey(effect.ID))
        {
           // activeEffects[effect.ID].Refresh();
        }
        // Else add it to dictionary
        else
        {
            activeEffects.Add(effect.ID, effect);
            if (effect.SpEffectType == EffectType.Instant)
            {
               // effect.ApplyEffectOnMonster(monster);
            }
        }
    }
    
    public override void UpdateEffectsTime(float deltaTime)
    {
        // Effect to remove list
        List<string> effectsToRemove = new List<string>();

        foreach (var effect in activeEffects.Values)
        {
            if (effect.SpEffectTimeRemaining <= 0)
            {
                effectsToRemove.Add(effect.SpEffectName);
               // effect.RemoveEffectOnMonster(monster);
            }
            else
            {
                effect.UpdateTime(deltaTime);
               // effect.ApplyEffectOnMonster(monster);
            }
        }

        //
        foreach (var effectName in effectsToRemove)
        {
            RemoveEffect(effectName);
        }
    }
}

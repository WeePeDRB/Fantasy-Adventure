using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HeroSpecialEffectSystem : CharacterSpecialEffectSystem
{
    //
    // FIELD
    //
    public HeroBaseController hero;
    public event EventHandler<OnReceiveSpecialEffectEventArgs> OnReceiveSpecialEffect;

    //
    // FUNCTION
    //
    public override void ReceiveEffect(SpecialEffectBaseOld effect)
    {

        // If an effect already exists in the dictionary, refresh its duration
        if (activeEffects.ContainsKey(effect.ID))
        {
            activeEffects[effect.ID].Refresh();
        }
        // Else add it to dictionary
        else
        {
            activeEffects.Add(effect.ID, effect);
            if (effect.SpEffectType == EffectType.Instant)
            {
                effect.ApplyEffectOnHero(hero);
            }
        }
    
        OnReceiveSpecialEffect?.Invoke(this, new OnReceiveSpecialEffectEventArgs { specialEffect = effect });
    }


    public override void UpdateEffectsTime(float deltaTime)
    {
        // Effect to remove list
        List<SpecialEffectBaseOld> effectsToRemove = new List<SpecialEffectBaseOld>();

        // 
        foreach (var effect in activeEffects.Values)
        {
            if (effect.SpEffectTimeRemaining <= 0)
            {
                effectsToRemove.Add(effect);
            }
            else
            {
                effect.UpdateTime(deltaTime);
                if (effect.SpEffectType == EffectType.Overtime)
                {
                    effect.ApplyEffectOnHero(hero);
                }
            }
        }

        //
        for (int i = effectsToRemove.Count - 1; i >= 0; i--)
        {
            if (effectsToRemove[i].SpEffectType == EffectType.Instant)
            {
                effectsToRemove[i].RemoveEffectOnHero(hero);
            }
            RemoveEffect(effectsToRemove[i].ID);
            effectsToRemove.Remove(effectsToRemove[i]);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectStatus 
{
    //
    // FIELDS
    //

    // SET UP SPECIAL EFFECT DICTIONARY
    private Dictionary<string, SpecialEffectBase> activeEffects = new Dictionary<string, SpecialEffectBase>();
    private CharacterBaseController character;



    //
    // FUNCTIONS
    //

    // CONTROL EFFECTS   
    // Receive effect function
    public void ReceiveEffect(SpecialEffectBase effect)
    {
        // If an effect already exists in the dictionary, refresh its duration
        if (activeEffects.ContainsKey(effect.effectName))
        {
            activeEffects[effect.effectName].Refresh(effect.duration);
        }
        // Else add it to dictionary
        else
        {
            activeEffects.Add(effect.effectName, effect);
        }
    }

    // Update effect function
    public void UpdateEffects(float deltaTime)
    {
        foreach (var effect in activeEffects.Values)
        {
            if (effect.TimeRemaining <= 0)
            {
                RemoveEffect(effect.effectName);
            }
            else
            {
                effect.UpdateEffect(deltaTime);
                //effect.ApplyEffect(character);
            }
        }
    }

    // Remove effect function
    public void RemoveEffect(string effectName)
    {
        if (activeEffects.ContainsKey(effectName))
        {
            activeEffects.Remove(effectName);
        }
    }

}

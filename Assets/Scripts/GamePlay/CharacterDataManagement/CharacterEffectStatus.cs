using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterEffectStatus 
{
    //
    // FIELDS
    //

    // SET UP SPECIAL EFFECT DICTIONARY
    protected Dictionary<string, SpecialEffectBase> activeEffects = new Dictionary<string, SpecialEffectBase>();

    //
    // FUNCTIONS
    //

    // CONTROL EFFECTS   
    // Receive effect function
    public void ReceiveEffect(SpecialEffectBase effect)
    {
        // If an effect already exists in the dictionary, refresh its duration
        if (activeEffects.ContainsKey(effect.EffectName))
        {
            activeEffects[effect.EffectName].Refresh();
        }
        // Else add it to dictionary
        else
        {
            activeEffects.Add(effect.EffectName, effect);
        }
    }

    // Update effect function
    public abstract void UpdateEffects(float deltaTime);


    // Remove effect function
    public void RemoveEffect(string effectName)
    {
        if (activeEffects.ContainsKey(effectName))
        {
            activeEffects.Remove(effectName);
        }
    }

    // Check if dictionary is empty
    public bool IsDictionaryEmpty()
    {
        if (activeEffects.Count == 0) return true;
        return false;
    }
}

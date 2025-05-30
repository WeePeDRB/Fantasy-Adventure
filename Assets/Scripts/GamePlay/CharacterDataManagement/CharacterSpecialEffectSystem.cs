using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterSpecialEffectSystem
{
    //
    // FIELDS
    //

    protected Dictionary<string, SpecialEffectBase> activeEffects = new Dictionary<string, SpecialEffectBase>();

    //
    // FUNCTIONS
    //

    // CONTROL EFFECTS   
    // Receive effect function
    public abstract void ReceiveEffect(SpecialEffectBase effect);

    // Update effect function
    public abstract void UpdateEffectsTime(float deltaTime);


    // Remove effect function
    public void RemoveEffect(string effectId)
    {
        if (activeEffects.ContainsKey(effectId))
        {
            activeEffects.Remove(effectId);
        }
    }

    // Check if dictionary is empty
    public bool IsDictionaryEmpty()
    {
        if (activeEffects.Count == 0) return true;
        return false;
    }
}

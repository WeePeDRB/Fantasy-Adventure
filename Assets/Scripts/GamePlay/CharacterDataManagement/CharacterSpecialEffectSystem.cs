using System.Collections.Generic;

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
    public void ReceiveEffect(SpecialEffectBase effect)
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

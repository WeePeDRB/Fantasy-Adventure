using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpecialEffectController
{
    // Dictionary containing special effects with the key as the ID and the value as the effect.
    private Dictionary<string, SpecialEffectBase> activeEffects;
    private List<SpecialEffectBase> effectToRemove;

    // Hero controller reference
    private HeroController heroController;

    // Reeceive special effect event
    public event Action<SpecialEffect> OnReceiveSpecialEffect;

    // Initialize data
    public HeroSpecialEffectController(HeroController heroController)
    {
        activeEffects = new Dictionary<string, SpecialEffectBase>();
        effectToRemove = new List<SpecialEffectBase>();
        this.heroController = heroController;
    }

    // Receive special effect
    public void ReceiveEffect(SpecialEffectBase effect)
    {
        // Check if special effect exist in dictionary
        // If yes -> Refresh special effect duration
        if (IsSpecialEffectExist(effect))
        {
            activeEffects[effect.ID].Refesh();
        }
        // If no -> Add effect to dictionary 
        else
        {
            // Adding special effect
            activeEffects.Add(effect.ID, effect);

            // Check the type of special effect and apply it to the Hero.
            // If it is an Instant effect, it will be applied immediately (triggered at the moment it is received).
            if (effect.SpEffectType == EffectType.Instant)
            {
                effect.ApplyEffectOnHero(heroController);
            }

            // Trigger the receive special effect event 
            OnReceiveSpecialEffect?.Invoke(new SpecialEffect { specialEffect = effect });
        }
    }

    // Apply the influence of special effects to the character in real time.
    public void UpdateEffect(float deltaTime)
    {
        // Effect to remove list: A list used to store effects that have expired and will be removed.  
        // This prevents logic error that occurs when removing effects directly from the dictionary.
        effectToRemove.Clear();

        // Check current special effect
        foreach (SpecialEffectBase effect in activeEffects.Values)
        {
            // If the time has expired add it to remove list 
            if (effect.SpEffectTimeRemaining <= 0)
            {
                effectToRemove.Add(effect);
            }
            // If the time is still remaining
            // Update the remaining time of the effect.  
            // To apply the effect here, its type must be Overtime.
            else
            {
                if (effect.SpEffectType == EffectType.Overtime)
                {
                    effect.ApplyEffectOnHero(heroController);
                }
                effect.UpdateTime(deltaTime);
            }
        }

        // Remove the expired effect from dictionary
        foreach (SpecialEffectBase effect in effectToRemove)
        {
            // Check if effect type is "Instant"
            if (effect.SpEffectType == EffectType.Instant)
            {
                // If yes -> Remove the effect from hero
                effect.RemoveEffectFromHero(heroController);
            }
            RemoveEffect(effect.ID);
        }
    }

    // Remove special effect
    private void RemoveEffect(string effectId)
    {
        if (activeEffects.ContainsKey(effectId))
        {
            activeEffects.Remove(effectId);
        }
    }

    // Check if special effect exist in dictonary
    private bool IsSpecialEffectExist(SpecialEffectBase effect)
    {
        if (activeEffects.ContainsKey(effect.ID))
        {
            return true;
        }
        return false;
    }

    // Check if dictionary is empty
    public bool IsDictionaryEmpty()
    {
        if (activeEffects.Count == 0) return true;
        return false;
    }
}

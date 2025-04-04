using System;
using UnityEngine;

public abstract class SpecialEffectBase
{
    //
    // FIELDS
    //

    // SET UP VALUES FOR SPECIAL EFFECT
    // Essential information
    public string effectName; // Effect name
    public float duration; // Effect duration
    protected float timeRemaining; // Time remaining 
   


    //
    // PROPERTIES
    //
    //
    public float TimeRemaining { get { return timeRemaining; } }



    //
    // FUNCTIONS
    //

    // MANAGE EFFECT LIFECYCLE
    // Update effect
    public void UpdateEffect(float deltaTime)
    {
        timeRemaining -= deltaTime;
    }

    // Refresh duration
    public void Refresh(float newDuration)
    {
        timeRemaining = Math.Max(timeRemaining, newDuration); // Cộng dồn hoặc giữ thời gian lâu nhất
    }

    //
    public abstract void ApplyEffectOnCharacter(CharacterBaseController character);


    
}
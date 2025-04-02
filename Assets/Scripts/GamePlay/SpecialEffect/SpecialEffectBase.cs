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
    private float timeRemaining; // Time remaining 
   


    //
    // PROPERTIES
    //
    //
    public float TimeRemaining { get { return timeRemaining; } }



    //
    // CONTRUCTOR
    //
    protected SpecialEffectBase(string name, float duration)
    {
        this.effectName = name;
        this.duration = duration;
        this.timeRemaining = duration;
    }



    //
    // FUNCTIONS
    //

    // MANAGE EFFECT LIFECYCLE
    // Start effect
    public void StartEffectOnCharacter(CharacterBaseController character)
    {
        character.ReceiveSpecialEffect(this);
    }

    public void StartEffectOnMonster(MonsterBaseController monster)
    {
        
    }

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
    public abstract void ApplyEffect(CharacterBaseController character);


    
}
using System;
using UnityEngine;

public abstract class SpecialEffectBase
{
    //
    // FIELDS
    //

    // SET UP VALUES FOR SPECIAL EFFECT
    // Essential information
    protected string effectName; // Effect name
    protected float duration; // Effect duration
    protected float timeRemaining; // Time remaining 
    protected EffectTarget effectTarget; 

    //
    // PROPERTIES
    //
    
    //
    public string EffectName { get { return effectName; } }
    public float TimeRemaining { get { return timeRemaining; } }
    public float Duration { get { return duration; } }
    public EffectTarget EffectTarget { get { return effectTarget; } }

    //
    // FUNCTIONS
    //

    // MANAGE EFFECT LIFECYCLE
    // Update effect
    public void UpdateTime(float deltaTime)
    {
        timeRemaining -= deltaTime;
    }

    // Refresh duration
    public void Refresh()
    {
        timeRemaining = duration; 
    }

    // Apply effect to player
    public abstract void ApplyEffectOnCharacter(HeroBaseController hero);

    // Apply effect to monster
    public abstract void ApplyEffectOnMonster(MonsterBaseController monster);

}
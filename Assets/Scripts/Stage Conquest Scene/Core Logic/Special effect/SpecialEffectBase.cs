using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialEffectBase
{
    // Special effect data
    protected string id;
    public string ID { get { return id; } }

    protected string spEffectName;
    public string SpEffectName { get { return spEffectName; } }

    protected string spEffectDescription;
    public string SpEffectDescription { get { return spEffectDescription; } }

    protected float spEffectDuration;
    public float SpEffectDuration { get { return spEffectDuration; } }

    protected float spEffectTimeRemaining;
    public float SpEffectTimeRemaining { get { return spEffectTimeRemaining; } }

    protected float spEffectValue;
    public float SpEffectValue { get { return spEffectValue; } }

    protected Sprite spEffectSprite;
    public Sprite SpEffectSprite { get { return spEffectSprite; } }

    protected EffectType spEffectType;
    public EffectType SpEffectType { get { return spEffectType; } }

    protected EffectTarget spEffectTarget;
    public EffectTarget SpEffectTarget { get { return spEffectTarget; } }

    // Time update
    public void UpdateTime(float deltaTime)
    {
        spEffectTimeRemaining -= deltaTime;
    }

    // Refest duration
    public void Refesh()
    {
        spEffectTimeRemaining = spEffectDuration;
    }

    // Effect on Hero 
    public abstract void ApplyEffectOnHero(HeroController heroController);
    public abstract void RemoveEffectOnHero(HeroController heroController);

    // Effect on Monster
    
}

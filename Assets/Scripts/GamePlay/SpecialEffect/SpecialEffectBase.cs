using System;
using UnityEngine;

public abstract class SpecialEffectBase
{
    public string effectName;
    public float duration; 
    private float timeRemaining;

    protected SpecialEffectBase(string name, float duration)
    {
        this.effectName = name;
        this.duration = duration;
        this.timeRemaining = duration;
    }

    public void StartEffect(CharacterBaseController character, Action<SpecialEffectBase> onEffectEnd)
    {
        ApplyEffect(character);
        EffectEndCallback = onEffectEnd;
    }

    public void UpdateEffect(float deltaTime, CharacterBaseController character)
    {
        timeRemaining -= deltaTime;
        if (timeRemaining <= 0)
        {
            RemoveEffect(character);
            EffectEndCallback?.Invoke(this);
        }
    }

    protected abstract void ApplyEffect(CharacterBaseController character);
    protected abstract void RemoveEffect(CharacterBaseController character);

    private Action<SpecialEffectBase> EffectEndCallback;
}
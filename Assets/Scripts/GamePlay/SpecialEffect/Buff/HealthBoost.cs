using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : SpecialEffectBase
{
    private float healthIncrease;

    public HealthBoost(float amount, float duration) : base("Health Boost", duration)
    {
        healthIncrease = amount;
    }

    protected override void ApplyEffect(CharacterBaseController character)
    {
        character.ModifyHealth(healthIncrease);
    }

    protected override void RemoveEffect(CharacterBaseController character)
    {

    }
}

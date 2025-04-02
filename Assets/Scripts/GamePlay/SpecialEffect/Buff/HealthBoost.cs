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

    public override void ApplyEffect(CharacterBaseController character)
    {
        
    }


}

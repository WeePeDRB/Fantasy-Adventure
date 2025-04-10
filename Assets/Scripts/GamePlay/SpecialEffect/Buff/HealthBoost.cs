using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : SpecialEffectBase
{
    private float healthIncrease;


    public HealthBoost(float amount, float duration) 
    {
        effectName = "HealthBoost";
        this.duration = duration;
        timeRemaining = duration;
        healthIncrease = amount;
    }

    public override void ApplyEffectOnCharacter(CharacterBaseController character)
    {
        character.characterStats.Health += healthIncrease;
    }


}

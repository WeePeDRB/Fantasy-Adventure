using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistanceBoost : SpecialEffectBase
{
    private float resistanceIncrease;

    public ResistanceBoost(float amount, float duration) 
    {
        effectName = "ResistanceBoost";
        this.duration = duration;
        timeRemaining = duration;
        resistanceIncrease = amount;
    }

    public override void ApplyEffectOnCharacter(CharacterBaseController character)
    {
        throw new System.NotImplementedException();
    }



}

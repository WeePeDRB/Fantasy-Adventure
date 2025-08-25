using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinSkill1 : HeroSkill
{
    // Paladin refernce
    private PaladinController paladinController;

    // Special effect
    [SerializeField] private SO_SpecialEffect effectData;
    private Se_ResistanceBoost resistanceBoost;

    public override void SkillActivate()
    {
        // Refesh special effect remain time
        resistanceBoost.Refesh();

        // Receive special effect
        paladinController.SpecialEffectController.ReceiveEffect(resistanceBoost);
    }

}

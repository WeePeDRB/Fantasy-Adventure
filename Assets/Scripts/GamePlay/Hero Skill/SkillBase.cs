using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    //
    // FIELDS
    //

    protected HeroBaseController heroBaseController;

    //
    // FUNCTIONS
    //

    // Instantiate skill
    protected abstract void InstantiateSkill();

    // Activate skill
    protected abstract void SkillActivate();
}

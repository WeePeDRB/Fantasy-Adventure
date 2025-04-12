using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    //
    // FIELDS
    //

    // Skill data
    protected SO_CharacterSkill skillData;

    //
    // FUNCTIONS
    //

    // Activate skill
    protected abstract void SkillActivate();
}

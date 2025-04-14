using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroBaseAnimator : MonoBehaviour
{   
    //
    // FUNCTIONS
    //

    // INITIAL SET UP FOR ANIMATOR
    protected abstract void InstantiateAnimator();

    // HANDLING CHARACTER ANIMATION
    // Character movement
    protected abstract void MoveAnimate();

    // Character dead
    protected abstract void DeadAnimate(); 

    // Character dash
    protected abstract void DashSkillAnimate();

    // Character special
    protected abstract void SpecialSkillAnimate();

    // Character ultimate
    protected abstract void UltimateSkillAnimate();

    // SUPPORT FUNCTION
    protected abstract void ReturnNormalState();
}

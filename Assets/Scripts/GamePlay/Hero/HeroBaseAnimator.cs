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

    // HANDLING HERO ANIMATION
    // Hero movement
    protected abstract void MoveAnimate();

    // Hero dead
    protected abstract void DeadAnimate(); 

    // Hero dash
    protected abstract void DashSkillAnimate();

    // Hero special
    protected abstract void SpecialSkillAnimate();

    // Hero ultimate
    protected abstract void UltimateSkillAnimate();

    // SUPPORT FUNCTION
    protected abstract void ReturnNormalState();
}

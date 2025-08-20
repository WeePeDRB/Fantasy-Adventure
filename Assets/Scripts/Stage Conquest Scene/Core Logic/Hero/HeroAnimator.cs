using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroAnimator : MonoBehaviour
{
    // Aniamtor
    protected Animator animator;

    // Controller reference 
    protected HeroController characterController;

    // Animator parametters
    protected const string IS_MOVING = "IsMoving";
    protected const string IS_DEAD = "IsDead";

    // Initialize data
    protected abstract void InitializeAnimator();

    // Trigger animation
    protected abstract void MoveAnimate();
    protected abstract void DeadAnimate();
}

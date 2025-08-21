using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroAnimator : MonoBehaviour
{
    // Aniamtor
    protected Animator animator;

    // Animator parametters
    protected const string IS_MOVING = "IsMoving";
    protected const string SKILL1 = "Skill1";
    protected const string SKILL2 = "Skill2";
    protected const string SKILL3 = "Skill3";
    protected const string IS_DEAD = "IsDead";

    // Initialize data
    protected abstract void InitializeData();

    // Animation handle
    // Hero movement
    protected abstract void MoveAnimate();
    // Hero skill
    protected abstract void Skill1Animate();
    protected abstract void Skill2Animate();
    protected abstract void Skill3Animate();
    // Hero dead
    protected abstract void DeadAnimate();
    protected abstract void ReturnNormalState();

}

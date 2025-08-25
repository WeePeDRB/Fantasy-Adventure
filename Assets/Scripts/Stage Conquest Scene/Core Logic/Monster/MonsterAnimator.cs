using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterAnimator : MonoBehaviour
{
    // Animator
    protected Animator animator;

    // Animator paramtters
    protected const string IS_MOVING = "IsMoving";
    protected const string IS_ATTACKING = "IsAttacking";
    protected const string IS_DEAD = "IsDead";

    // Initialize data
    protected abstract void InitializeData();
}

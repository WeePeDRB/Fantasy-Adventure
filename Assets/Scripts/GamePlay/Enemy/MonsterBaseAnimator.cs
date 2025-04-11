using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBaseAnimator : MonoBehaviour
{
    // INITIAL SET UP FOR ANIMATOR
    protected abstract void InstantiateAnimator();

    // HANDLING CHARACTER ANIMATIOn
    // Monster movement
    protected abstract void Move();

    // Monster attack
    protected abstract void Attack();

    // Monster dead
    protected abstract void Dead();
}

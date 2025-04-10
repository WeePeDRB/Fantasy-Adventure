using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonsterAnimator 
{
    // MONSTER BEHAVIOR ANIMTION
    // Monster movement
    public void Move();

    // Monster attack
    public void Attack();

    // Monster dead
    public void Dead();
    
}

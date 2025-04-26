using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimator : MonsterBaseAnimator
{
    //
    // FUNCTIONS
    //

    // Monster attack
    protected void AttackHit()
    {
        monsterBaseController.Attack();
    }

    private void Start()
    {
        // 
        InstantiateAnimator();

        //
        monsterBaseController.OnMonsterAttack += Attack;
        monsterBaseController.OnMonsterDead += Dead;
    }

    private void Update()
    {
        monsterBehaviorState = monsterBaseController.MonsterBeHaviorState;
        Move();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteZombieAnimator : MonsterBaseAnimator
{
    //
    // FUNCTIONS
    //

    // Monster attack
    protected void ApplyDamage()
    {
        //monsterBaseController.ApplyDamage();
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
        monsterMovementState = monsterBaseController.MonsterMovementState;
        Move();
    }
}

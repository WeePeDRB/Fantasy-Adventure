using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchAnimator : MonsterBaseAnimator
{
    //
    // FIELDS
    //
    private WitchController witchController;

    //
    // FUNCTIONS
    //

    // Monster attack    
    public void SpawnProjectile()
    {
        witchController.SpawnProjectile();
    }

    private void Start()
    {
        //
        witchController = GetComponentInParent<WitchController>();

        // 
        InstantiateAnimator();

        //
        monsterBaseController.OnMonsterAttack += Attack;
        monsterBaseController.OnMonsterDead += Dead;
    }

    private void Update()
    {
        monsterBehaviorState = monsterBaseController.MonsterBehaviorState;
        Move();
    }
}

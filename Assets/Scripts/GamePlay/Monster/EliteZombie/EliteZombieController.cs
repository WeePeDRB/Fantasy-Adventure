using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteZombieController : MonsterBaseController
{
    //
    // FUNCTIONS
    //

    // INITIAL SET UP FOR ELITE ZOMBIE
    public override void InstantiateMonster()
    {
        base.InstantiateMonster();

        // Initial stats for Elite zombie
        monsterStats = new MonsterStats(200,4.4f,1,20,0.5f,0,0);
    }

    

    // HANDLING ELITE ZOMBIE BEHAVIOR
    // Elite zombie movement

    // Elite zombie attack

    // Elite zombie get hurt

    // Elite zombie dead

    // SUPPORT FUNCTION

    private void Awake()
    {
        InstantiateMonster();
    }

    private void Update()
    {
        if (monsterHealthState == MonsterHealthState.Alive) 
        {
            HandleRotation();
            DistanceCheck(1.5f);
        }
    }

    private void FixedUpdate()
    {
        if (monsterHealthState == MonsterHealthState.Alive) 
        {
            HandleMovement();
        }
    }
}

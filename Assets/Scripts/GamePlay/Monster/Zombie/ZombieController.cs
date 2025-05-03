using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonsterBaseController
{
    //
    // FUNCTIONS
    //

    // INITIAL SET UP FOR ZOMBIE
    public override void InstantiateMonster()
    {
        base.InstantiateMonster();

        // Initial stats for zombie
        monsterStats = new MonsterStats(100,1,1,10,0.5f,0,0);
    }

    

    // HANDLING ZOMBIE BEHAVIOR
    // Zombie movement

    // Zombie attack

    // Zombie get hurt

    // Zombie dead

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
            DistanceCheck(.8f);
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

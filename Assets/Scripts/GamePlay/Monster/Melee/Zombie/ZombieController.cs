using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MeleeMonsterController
{
    //
    // FUNCTIONS
    //

    // INITIAL SET UP FOR ZOMBIE
    public override void InstantiateMonster()
    {
        base.InstantiateMonster();

        // Initial stats for zombie
        monsterStats = new MonsterStats(monsterData);
    }

    private void Awake()
    {
        InstantiateMonster();
    }

    private void Start()
    {
        InvokeRepeating(nameof(UpdateHeroTarget), UnityEngine.Random.Range(0f, 0.2f), 0.5f);
    }

    private void Update()
    {
        BehaviorController();
    }
    private void FixedUpdate()
    {
        if (monsterHealthState == MonsterHealthState.Alive) 
        {
            if (heroTarget != null)
            {
                HandleMovement();
            }
        }
    }
}

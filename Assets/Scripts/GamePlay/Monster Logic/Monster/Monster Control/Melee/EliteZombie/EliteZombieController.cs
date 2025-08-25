using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteZombieController : MeleeMonsterControllerOld
{
    //
    // FUNCTIONS
    //

    // INITIAL SET UP FOR ELITE ZOMBIE
    public override void InstantiateMonster()
    {
        base.InstantiateMonster();

        // Initial stats for Elite zombie
        monsterStats = new MonsterStats(monsterData);
    }

    private void Awake()
    {
        InstantiateMonster();
    }

    private void Start()
    {
        InvokeRepeating(nameof(UpdateHeroTarget), Random.Range(0f, 0.2f), 0.5f);
    }

    private void Update()
    {
        BehaviorController();
        StandbyStateBreak();
    }
    private void FixedUpdate()
    {
        if (monsterHealthState == MonsterHealthStateOld.Alive) 
        {
            if (heroTarget != null)
            {
                HandleMovement();
            }
        }
    }
}

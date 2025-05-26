using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteZombieController : MeleeMonsterController
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

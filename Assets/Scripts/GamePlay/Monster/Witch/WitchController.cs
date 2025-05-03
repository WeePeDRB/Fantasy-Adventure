using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchController : MonsterBaseController
{
    //
    // FUNCTIONS
    //

    // INITIAL SET UP FOR WITCH
    public override void InstantiateMonster()
    {
        base.InstantiateMonster();

        // Initial stats for witch
        monsterStats = new MonsterStats(60,3,1,40,2f,0,0);
    }

    

    // HANDLING WITCH BEHAVIOR
    // Witch movement

    // Witch attack
    public void GetDataFromPorjectile(object sender, MonsterBaseProjectile.OnProjectileHitEventArgs onProjectileHitEventArgs)
    {
        
    }
    // Witch get hurt

    // Witch dead

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
            DistanceCheck(8.5f);
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

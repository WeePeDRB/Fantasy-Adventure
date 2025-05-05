using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchController : RangedMonsterController
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
    public void GetDataFromPorjectile(object sender, RangedMonsterProjectile.OnProjectileHitEventArgs onProjectileHitEventArgs)
    {
        
    }
    public override void SpawnProjectile()
    {
        GameObject projectileObject = WitchProjectileObjectPool.Instance.GetObject(projectileSpawn);
        WitchProjectile witchProjectile = projectileObject.GetComponent<WitchProjectile>();
        
        witchProjectile.HeroPositionLocate();
        witchProjectile.ResetProjectileState();
    }
    // Witch get hurt

    // Witch dead

    // SUPPORT FUNCTION

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
        if (monsterHealthState == MonsterHealthState.Alive) 
        {
            if (heroTarget != null)
            {
                HandleRotation();
                DistanceCheck(8.5f);
            }
        }
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

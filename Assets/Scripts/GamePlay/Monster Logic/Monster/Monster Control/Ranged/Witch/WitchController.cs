using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchControllerOld : RangedMonsterControllerOld
{
    //
    // FUNCTIONS
    //

    // INITIAL SET UP FOR WITCH
    public override void InstantiateMonster()
    {
        base.InstantiateMonster();
    
        // Initial stats for witch
        monsterStats = new MonsterStats(monsterData);
    }

    

    // HANDLING WITCH BEHAVIOR
    // Witch movement
    // Witch attack
    public void GetDataFromPorjectile(object sender, OnWitchProjectileHitEventArgs onProjectileHitEventArgs)
    {
        WitchProjectileOld witchProjectile = onProjectileHitEventArgs.witchProjectile;
        witchProjectile.OnProjectileHit -= GetDataFromPorjectile;
        witchProjectile.OnProjectileReturn -= GetDataFromPorjectile;
        if (onProjectileHitEventArgs.heroBaseController != null) ApplyDamage(onProjectileHitEventArgs.heroBaseController);
    }
    public override void SpawnProjectile()
    {
        // Get projectile from pool
        GameObject projectileObject = WitchProjectileObjectPool.Instance.GetObject(projectileSpawn);
        WitchProjectileOld witchProjectile = projectileObject.GetComponent<WitchProjectileOld>();
        
        // Initialize for projectile
        witchProjectile.InitializeProjectile(9, heroTarget.transform.position,7);

        // Subscribe to event for data return
        witchProjectile.OnProjectileHit += GetDataFromPorjectile;
        witchProjectile.OnProjectileReturn += GetDataFromPorjectile;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchController : RangeMonsterController
{
    public override void SpawnProjectile()
    {
        // Get projectile from pool
        GameObject projectileObject = WitchProjectileObjectPool.Instance.GetObject(projectileSpawn);
        WitchProjectile witchProjectile = projectileObject.GetComponent<WitchProjectile>();

        // Initialize for projectile
        witchProjectile.InitializeProjectile(9, heroTarget.transform.position, 7);

        // Subscribe to event for data return
        witchProjectile.OnHitHero += GetDataFromProjectile;
        witchProjectile.OnReturnPool += GetDataFromProjectile;
    }

    protected override void GetDataFromProjectile(MonsterProjectile monsterProjectile)
    {
        if (monsterProjectile.monsterProjectile is WitchProjectile)
        {

            WitchProjectile witchProjectile = monsterProjectile.monsterProjectile as WitchProjectile;

            // Unsub the event to prevent unpredictable errors
            witchProjectile.OnHitHero -= GetDataFromProjectile;
            witchProjectile.OnReturnPool -= GetDataFromProjectile;

            // Check if projectile hit hero
            if (monsterProjectile.heroController != null)
            {
                // Apply damage on hero
                ApplyDamage(monsterProjectile.heroController);
            }
        }
    }

}

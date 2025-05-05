using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchProjectile : RangedMonsterProjectile
{
    //
    // FUNCTIONS
    //

    protected override void ReturnProjectile()
    {
        Vector3 toTargetNow = (heroPosition - transform.position).normalized;

        if (Vector3.Dot(heroPosition, toTargetNow) < 0f)
        {
            WitchProjectileObjectPool.Instance.ReturnObject(this.gameObject);
        }
    }

    private void Awake()
    {
        InstantiateProjectile(7f);
    }

    private void Update()
    {
        ReturnProjectile();
        if (projectileMoving == true) 
        {
            MoveToHeroPosition();
        }    
    }
}

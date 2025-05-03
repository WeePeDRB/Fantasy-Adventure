using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchAnimator : MonsterBaseAnimator
{
    //
    // FIELDS
    //
    [SerializeField] private Transform projectileSpawn;
    private WitchController witchController;

    //
    // FUNCTIONS
    //

    // Monster attack    
    public void SpawnProjectile()
    {
        GameObject projectileObject = WitchProjectileObjectPool.Instance.GetObject(projectileSpawn);
        WitchProjectile witchProjectile = projectileObject.GetComponent<WitchProjectile>();
        //witchProjectile.OnProjectileHit += witchController.ApplyDamage()
        witchProjectile.HeroPositionLocate();
        witchProjectile.ResetProjectileState();
    }

    private void Start()
    {
        //
        witchController = GetComponentInParent<WitchController>();

        // 
        InstantiateAnimator();

        //
        monsterBaseController.OnMonsterAttack += Attack;
        monsterBaseController.OnMonsterDead += Dead;
    }

    private void Update()
    {
        monsterMovementState = monsterBaseController.MonsterMovementState;
        Move();
    }
}

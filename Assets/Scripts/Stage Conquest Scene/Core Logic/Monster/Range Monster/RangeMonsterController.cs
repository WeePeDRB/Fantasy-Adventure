using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangeMonsterController : MonsterController
{
    // Projectile
    [SerializeField] protected Transform projectileSpawn;

    // Monster attack
    public override void ApplyDamage(HeroController heroController)
    {
        heroController.Hurt(statsController.AttackDamage);
    }

    // For ranged monsters, behavior control is based on detecting heroes within range.
    // -> When a hero enters its range, the monster will switch to the "Attacking" state.
    protected override void InRange()
    {
        base.InRange();

        // Change monster's state
        behaviorState = MonsterBehaviorState.Attacking;
    }
    protected override void OutOfRange()
    {
        base.OutOfRange();

        // Change monster's state
        behaviorState = MonsterBehaviorState.Idling;
    }

    // Spawn projectile 
    public abstract void SpawnProjectile();
    protected abstract void GetDataFromProjectile(MonsterProjectile monsterProjectile);
}

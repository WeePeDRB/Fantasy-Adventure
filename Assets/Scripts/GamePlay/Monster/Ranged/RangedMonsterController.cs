using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonsterController : MonsterBaseController
{
    //
    // FIELDS
    //

    // PROJECTILE 
    [SerializeField] protected Transform projectileSpawn;

    //
    // FUNCTIONS
    //

    // HANDLING MONSTER BEHAVIOR

    // Monster behavior controller
    protected override void InRange()
    {
        isPlayerInsideAttackHitBox = true;
        monsterBehaviorState = MonsterBehaviorState.Attack;
    }
    protected override void OutOfRange()
    {
        isPlayerInsideAttackHitBox = false;
        monsterBehaviorState = MonsterBehaviorState.Standby;
    }
    protected override void BehaviorController()
    {
        if (monsterHealthState == MonsterHealthState.Alive)
        {
            if (monsterBehaviorState == MonsterBehaviorState.Attack)
            {
                if (isReadyToAttack)
                {
                    Attack();
                }
            }
            else if (monsterBehaviorState == MonsterBehaviorState.Standby)
            {
                if (isAttacking) return;
                else if (!isAttacking)
                {
                    monsterBehaviorState = MonsterBehaviorState.Move;
                }
            }
        }
    }

    protected override void Attack()
    {
        HandleOnMonsterAttack();
        isAttacking = true;
        isReadyToAttack = false;
    }

    public override IEnumerator AttackRecover()
    {
        isAttacking = false;
        yield return new WaitForSeconds(monsterStats.AttackSpeed);
        isReadyToAttack = true;
    }

    public override void ApplyDamage(HeroBaseController heroHit)
    {
        heroHit.Hurt(monsterStats.Damage);
    }

    public virtual void SpawnProjectile() { }
}


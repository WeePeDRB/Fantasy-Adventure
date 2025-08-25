using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonsterControllerOld : MonsterBaseControllerOld
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
        monsterBehaviorState = MonsterBehaviorStateOld.Attack;
    }
    protected override void OutOfRange()
    {
        isPlayerInsideAttackHitBox = false;
        monsterBehaviorState = MonsterBehaviorStateOld.Standby;
    }
    protected override void BehaviorController()
    {
        if (monsterHealthState == MonsterHealthStateOld.Alive)
        {
            if (monsterBehaviorState == MonsterBehaviorStateOld.Attack)
            {
                if (isReadyToAttack)
                {
                    Attack();
                }
            }
            else if (monsterBehaviorState == MonsterBehaviorStateOld.Standby)
            {
                if (isAttacking) return;
                else 
                {
                    monsterBehaviorState = MonsterBehaviorStateOld.Move;
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


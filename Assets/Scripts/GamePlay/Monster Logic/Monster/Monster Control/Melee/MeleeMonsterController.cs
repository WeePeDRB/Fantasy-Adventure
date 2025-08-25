using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonsterControllerOld : MonsterBaseControllerOld
{
    //
    // FUNCTIONS
    //

    // HANDLING MONSTER BEHAVIOR

    // Monster behavior controller
    protected override void InRange()
    {
        isPlayerInsideAttackHitBox = true;
    }
    protected override void OutOfRange()
    {
        isPlayerInsideAttackHitBox = false;
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
        if (isPlayerInsideAttackHitBox)
        {
            heroHit.Hurt(monsterStats.Damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            monsterBehaviorState = MonsterBehaviorStateOld.Attack;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            monsterBehaviorState = MonsterBehaviorStateOld.Standby;
        }
    }

}

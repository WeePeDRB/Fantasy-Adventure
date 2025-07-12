using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonsterController : MonsterBaseController
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
                    else 
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
        if (isPlayerInsideAttackHitBox)
        {
            heroHit.Hurt(monsterStats.Damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            monsterBehaviorState = MonsterBehaviorState.Attack;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            monsterBehaviorState = MonsterBehaviorState.Standby;
        }
    }

}

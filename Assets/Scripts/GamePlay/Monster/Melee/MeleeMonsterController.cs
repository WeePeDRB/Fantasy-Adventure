using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonsterController : MonsterBaseController
{
    //
    // FUNCTIONS
    //

    // HANDLING MONSTER BEHAVIOR
    // Monster attack 

    // Checking hit box
    protected override void InRange()
    {
        isPlayerInsideAttackHitBox = true;
    }
    protected override void OutOfRange()
    {
        isPlayerInsideAttackHitBox = false;
    }

    protected override IEnumerator AttackCoroutine()
    {
        while (monsterHealthState == MonsterHealthState.Alive)
        {
            monsterBehaviorState = MonsterBehaviorState.Attack;
            yield return new WaitForSeconds(.1f);
            if (isReadyToAttack) Attack();
        }
    }
    protected override void Attack()
    {
        HandleOnMonsterAttack();
        isReadyToAttack = false;
    }

    public override IEnumerator AttackRecover()
    {
        monsterBehaviorState = MonsterBehaviorState.Idle;
        yield return new WaitForSeconds(monsterStats.AttackSpeed);
        ReadyToAttack();
    }

    protected override void ReadyToAttack()
    {
        isReadyToAttack = true;
        if (isPlayerInsideAttackHitBox) monsterBehaviorState = MonsterBehaviorState.Idle;
        else monsterBehaviorState = MonsterBehaviorState.Move;
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
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(AttackCoroutine());
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

}

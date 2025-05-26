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
    // Monster attack 
    // Checking hit box
    protected override void InRange()
    {
        isPlayerInsideAttackHitBox = true;
        if (attackCoroutine == null) attackCoroutine = StartCoroutine(AttackCoroutine());
    }
    protected override void OutOfRange()
    {
        isPlayerInsideAttackHitBox = false;
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
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
        if (isPlayerInsideAttackHitBox)
        {
            monsterBehaviorState = MonsterBehaviorState.Idle;
        }
        else
        {
            monsterBehaviorState = MonsterBehaviorState.Move;
        }
        
    }

    public override void ApplyDamage(HeroBaseController heroHit)
    {
        heroHit.Hurt(monsterStats.Damage);
    }

    public virtual void SpawnProjectile() { }
}


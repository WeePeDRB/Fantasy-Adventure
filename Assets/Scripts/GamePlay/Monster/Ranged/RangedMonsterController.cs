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
        ReadyToAttack();
    }
    protected override void OutOfRange()
    {
        isPlayerInsideAttackHitBox = false;
        if (monsterAttackState == MonsterAttackState.Attack) return;
        else
        {
            if (isPlayerInsideAttackRange == true)
            {
                monsterMovementState = MonsterMovementState.Rotate;
            }
            else if (isPlayerInsideAttackRange == false)
            {
                monsterMovementState = MonsterMovementState.Move;
            }
        }
    }

    // Attack process
    protected override void ReadyToAttack()
    {
        // Set behavior state
        monsterAttackState = MonsterAttackState.ReadyToAttack;
        monsterMovementState = MonsterMovementState.Rotate;

        // Handling coroutine
        if (readyAttackCoroutine == null)
        {
            readyAttackCoroutine = StartCoroutine(ReadyToAttackCoroutine());
        }
    }
    protected override IEnumerator ReadyToAttackCoroutine()
    {
        float elapsedTime = 0f;
        float waitTime = monsterStats.AttackSpeed * 0.3f;

        while (elapsedTime < waitTime)
        {
            if (monsterMovementState == MonsterMovementState.Move) 
            {   
                StopCoroutine(readyAttackCoroutine);
                readyAttackCoroutine = null;
                yield break;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        monsterAttackState = MonsterAttackState.Attack;
        
        // Start coroutine
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(AttackCoroutine());
        }  
    }
    protected override IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(monsterStats.AttackSpeed * 0.3f);
        HandleOnMonsterAttack();
    }
    public override void ApplyDamage(HeroBaseController heroHit)
    {
        if (isPlayerInsideAttackHitBox)
        {
            heroHit.Hurt(monsterStats.Damage);
        }
    }
    public override void ResetAttack()
    {
        // 
        readyAttackCoroutine = null;
        attackCoroutine = null;
        
        //
        if (isPlayerInsideAttackHitBox == true)
        {
            ReadyToAttack();
            return;
        }
        else
        {
            if (isPlayerInsideAttackRange)
            {
                monsterMovementState = MonsterMovementState.Rotate;
            }
            else
            {
                monsterMovementState = MonsterMovementState.Move;
            }
        }
    }   
    public virtual void SpawnProjectile(){}
}

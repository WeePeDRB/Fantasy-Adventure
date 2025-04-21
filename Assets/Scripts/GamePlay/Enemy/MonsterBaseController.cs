using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBaseController : MonoBehaviour
{ 
    //
    // FIELDS
    //

    // HERO DATA
    [SerializeField] protected SO_Monster monsterData;

    // CHECKING FLAGS
    protected bool isMoving;
    protected bool isPlayerInside;
    protected bool isAttacking;
    protected bool isDead;

    // REFERENCE 
    protected MonsterBaseHitBox monsterBaseHitBox;
    protected HeroBaseController player;

    // COROUTINE VALUE
    protected Coroutine attackCoroutine;

    // MONSTER STATS
    protected MonsterStats monsterStats;

    // MONSTER EFFECT STATUS
    protected MonsterEffectStatus effectStatus;

    //
    // PROPERTIES
    //
    public SO_Monster MonsterData
    {
        get { return monsterData; }
    }
    public MonsterStats MonsterStats
    {
        get { return monsterStats; }
    }

    // INITIAL SET UP FOR MONSTER
    public abstract void InstantiateMonster();

    // HANDLING MONSTER BEHAVIOR
    // Monster movement
    protected abstract void HandleMovement();

    // Monster attack
    protected abstract void IsReadyToAttack();
    protected abstract IEnumerator AttackCoroutine();
    public abstract void Attack();
    protected abstract void IsOutOfRange();

    // Monster get hurt
    public abstract void Hurt(float damageTaken);

    // Monster dead
    protected abstract void Dead();
    protected virtual void DropExp()
    {
        
    }

    // SUPPORT FUNCTION
    // Get special effect
    public abstract void ReceiveSpecialEffect(SpecialEffectBase specialEffect);
}

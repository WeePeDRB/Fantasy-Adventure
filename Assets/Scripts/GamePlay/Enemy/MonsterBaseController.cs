using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBaseController : MonoBehaviour
{ 
    // INITIAL SET UP FOR MONSTER
    public abstract void InstantiateMonster();


    // MONSTER BEHAVIOR
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


    // SUPPORT FUNCTION
    // Get special effect
    public abstract void ReceiveSpecialEffect(SpecialEffectBase specialEffect);
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonsterController 
{ 
    // INITIAL SET UP FOR MONSTER
    public void InstantiateMonster();


    // MONSTER BEHAVIOR
    // Monster movement
    public void HandleMovement();

    // Monster attack
    public void IsReadyToAttack();
    public IEnumerator AttackCoroutine();
    public void Attack();
    public void IsOutOfRange();

    // Monster get hurt
    public void Hurt(float damageTaken);

    // Monster dead
    public void Dead();


    // SUPPORT FUNCTION
    // Get special effect
    public void ReceiveSpecialEffect(SpecialEffectBase specialEffect);
}

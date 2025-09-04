using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchProjectile : Projectile
{
    // Events 
    public event Action<MonsterProjectile> OnHitHero;

    // This event was created with the purpose of unsubscribing both events 
    // when the projectile misses its target and returns to the pool.
    public event Action<MonsterProjectile> OnReturnPool;

    // Return the projectile to object pool
    protected override void ReturnObject()
    {
        OnReturnPool?.Invoke(new MonsterProjectile { heroController = null, monsterProjectile = this });
        // Invoke this event to 
        WitchProjectileObjectPool.Instance.ReturnObject(gameObject);
    }

    // Check if the projectile hit hero
    private void OnTriggerEnter(Collider collider)
    {
        // If yes
        if (collider.gameObject.CompareTag("Player"))
        {
            // Stop the return coroutine
            StopCoroutine(returnCoroutine);
            returnCoroutine = null;

            // Invoke the event to send projectile's data
            OnHitHero?.Invoke(new MonsterProjectile { heroController = collider.gameObject.GetComponent<HeroController>(), monsterProjectile = this });

            // Return the projectile to object pool
            WitchProjectileObjectPool.Instance.ReturnObject(gameObject);
        }
    }

    private void Update()
    {
        MoveToPosition();
    }
}

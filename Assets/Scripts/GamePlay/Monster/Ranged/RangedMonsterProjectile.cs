using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedMonsterProjectile : MonoBehaviour
{
    //
    // FIELDS
    //

    // PROJECTILE MOVEMENT
    protected Vector3 heroPosition = new Vector3();
    protected bool projectileMoving;
    protected float projectileSpeed;

    // REFERENCES
    protected HeroBaseController heroBaseController;

    // EVENTS
    // Events for monster behavior
    public event EventHandler OnProjectileHit; // Event occurs when projectile hit player
    public event EventHandler OnProjectileReachDestination; // Event occurs when projectile reach its destination

    // Custom class for event args
    public class OnProjectileHitEventArgs : EventArgs
    {
        public HeroBaseController heroBaseController;
        public RangedMonsterProjectile monsterBaseProjectile;
    }
    public class OnProjectileReachDestinationEventArgs : EventArgs
    {
        public RangedMonsterProjectile monsterBaseProjectile;
    }

    //
    // FUNCTIONS
    //

    // INITIAL SET UP FOR PROJECTILE
    // First set up for projectile
    protected virtual void InstantiateProjectile(float speed)
    {
        // Set variables
        heroBaseController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseController>();
        projectileSpeed = speed;
        projectileMoving = false;
    }
    // Reset after get projectile out from pool
    public virtual void ResetProjectileState()
    {
        projectileMoving = true;
    }

    // PROJECTILE MOVEMENT
    // Projectile movement
    protected void MoveToHeroPosition()
    {
        Vector3 direction = (heroPosition - transform.position).normalized;
        transform.Translate(direction * projectileSpeed * Time.deltaTime, Space.World);    
    }
    // Locate hero position to reach
    public Vector3 HeroPositionLocate()
    {
        // Set moving flag
        projectileMoving = true;
        return heroPosition = heroBaseController.gameObject.transform.position;
    }

    // PROJECTILE STATE CONTROL
    protected abstract void ReturnProjectile();

    // COLLIDER CHECK FUNCTIONS
    // Check if projectile hit hero
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            OnProjectileHit?.Invoke(
                this, 
                new OnProjectileHitEventArgs
                { 
                    heroBaseController = collider.gameObject.GetComponent<HeroBaseController>(), 
                    monsterBaseProjectile = this
                }
            );
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchProjectile : Projectile
{
    //
    // FIELDS
    //
    public event EventHandler<OnProjectileHitEventArgs> OnProjectileHit;
    public event EventHandler<OnProjectileHitEventArgs> OnProjectileReturn;

    // Custom class for event args
    public class OnProjectileHitEventArgs : EventArgs
    {
        public HeroBaseController heroBaseController;
        public WitchProjectile witchProjectile;
    }

    //
    // FUNCTIONS
    //

    protected override void ReturnObject()
    {
        OnProjectileReturn?.Invoke(this, new OnProjectileHitEventArgs{ heroBaseController = null, witchProjectile = this});
        WitchProjectileObjectPool.Instance.ReturnObject(this.gameObject);
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            StopCoroutine(returnCoroutine);
            returnCoroutine = null;
            OnProjectileHit?.Invoke(this, new OnProjectileHitEventArgs{ heroBaseController = collider.gameObject.GetComponent<HeroBaseController>(), witchProjectile = this});
            WitchProjectileObjectPool.Instance.ReturnObject(this.gameObject);
        }
    }

    private void Update()
    {
        MoveToPosition();
    }
}


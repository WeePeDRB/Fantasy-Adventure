using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchProjectileOld : Projectile
{
    //
    // FIELDS
    //
    public event EventHandler<OnWitchProjectileHitEventArgs> OnProjectileHit;
    public event EventHandler<OnWitchProjectileHitEventArgs> OnProjectileReturn;

    //
    // FUNCTIONS
    //

    protected override void ReturnObject()
    {
        OnProjectileReturn?.Invoke(this, new OnWitchProjectileHitEventArgs { heroBaseController = null, witchProjectile = this });
        WitchProjectileObjectPool.Instance.ReturnObject(this.gameObject);
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {

            StopCoroutine(returnCoroutine);
            returnCoroutine = null;
            OnProjectileHit?.Invoke(this, new OnWitchProjectileHitEventArgs{ heroBaseController = collider.gameObject.GetComponent<HeroBaseController>(), witchProjectile = this});
            WitchProjectileObjectPool.Instance.ReturnObject(this.gameObject);
        }
    }

    private void Update()
    {
        MoveToPosition();
    }
}



using System;
using System.Collections;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    //
    // FIELDS
    //

    // PROJECTILE MOVEMENT

    protected Vector3 heroPosition;
    protected Vector3 moveDirection;
    protected float projectileSpeed;
    protected float returnTime;
    protected Coroutine returnCoroutine;

    //
    // FUNCTIONS
    //

    // Set up for projectile
    public virtual void InitializeProjectile(float speed, Vector3 position, float returnTime)
    {
        // Set variables
        this.returnTime = returnTime;
        projectileSpeed = speed;
        heroPosition = new Vector3 (position.x, 1f, position.z);
        moveDirection = (heroPosition - transform.position).normalized;

        // Start the return coroutine for projectile
        returnCoroutine = StartCoroutine(ReturnPoolCoroutine());
    }

    // Move projectile
    protected virtual void MoveToPosition()
    {
        transform.Translate(moveDirection * projectileSpeed * Time.deltaTime, Space.World);
    }

    // Return object coroutine
    protected IEnumerator ReturnPoolCoroutine()
    {
        yield return new WaitForSeconds(returnTime);
        ReturnObject();
    }

    // Return object when not hit hero
    protected abstract void ReturnObject();
}




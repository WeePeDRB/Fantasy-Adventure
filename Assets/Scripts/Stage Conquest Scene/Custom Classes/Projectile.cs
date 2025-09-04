
using System;
using System.Collections;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    // Projectile movement
    protected Vector3 targetPosition;
    protected Vector3 moveDirection;
    protected float projectileSpeed;
    protected float returnTime;
    protected Coroutine returnCoroutine;

    // Set up for projectile
    // This function is used to configure the basic parameters of a projectile before firing
    // , including "Speed", "Target", and "Return Time"
    // It is called each time before launching the projectile.
    public virtual void InitializeProjectile(float speed, Vector3 position, float returnTime)
    {
        // Set variables
        this.returnTime = returnTime;
        projectileSpeed = speed;
        targetPosition = new Vector3(position.x, 1f, position.z);

        // Calculate direction for the projectile
        moveDirection = (targetPosition - transform.position).normalized;

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

    // Return object when not hit target
    protected abstract void ReturnObject();
}




using System;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowProjectile : Projectile
{
    //
    // FIELDS
    //
    public event EventHandler<OnProjectileHitEventArgs> OnProjectileHit;
    public event EventHandler<OnProjectileHitEventArgs> OnProjectileReturn;

    // Custom class for event args
    public class OnProjectileHitEventArgs : EventArgs
    {
        public MonsterBaseControllerOld monsterBaseController;
        public ArrowProjectile arrowProjectile;
    }

    //
    // FUNCTIONS
    //
    public override void InitializeProjectile(float speed, Vector3 position, float returnTime)
    {
        // Set variables
        this.returnTime = returnTime;
        projectileSpeed = speed;
        heroPosition = new Vector3 (position.x, 1f, position.z);
        moveDirection = (heroPosition - transform.position).normalized;

        // 
        transform.rotation = Quaternion.LookRotation(-moveDirection);      // 
        transform.rotation = Quaternion.FromToRotation(Vector3.down, moveDirection); // 


        // Start the return coroutine for projectile
        returnCoroutine = StartCoroutine(ReturnPoolCoroutine());
    }

    protected override void ReturnObject()
    {
        OnProjectileReturn?.Invoke(this, new OnProjectileHitEventArgs { monsterBaseController = null, arrowProjectile = this});
        ArrowObjectPool.Instance.ReturnObject(this.gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            OnProjectileHit?.Invoke(this, new OnProjectileHitEventArgs { monsterBaseController = collider.gameObject.GetComponent<MonsterBaseControllerOld>(), arrowProjectile = this});
        }
    }

    private void Update()
    {
        MoveToPosition();
    }
}
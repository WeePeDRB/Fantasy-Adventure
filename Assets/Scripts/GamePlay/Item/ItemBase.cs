using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    //
    // FIELDS
    //
    protected Rigidbody rb;

    // Item behavior
    public virtual void LaunchItemRandomDirection(float upwardForce = 5f, float horizontalForce = 1.5f)
    {
        if (rb != null)
        {
            // Random hướng ngang trên mặt phẳng XZ
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

            // Kết hợp lực ngang + lực lên
            Vector3 force = randomDirection * horizontalForce + Vector3.up * upwardForce;

            // Bắn item
            rb.AddForce(force, ForceMode.Impulse);
        }
    }
}

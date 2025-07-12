using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : ItemBase
{
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    // Collider check
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CoinObjectPool.Instance.ReturnObject(this.gameObject);
        }
    }
}

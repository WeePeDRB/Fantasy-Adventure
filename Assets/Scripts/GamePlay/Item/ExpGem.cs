using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpGem : ItemBase
{

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            LaunchItemRandomDirection();
        }
    }

    // Collider check
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ExpGemObjectPool.Instance.ReturnObject(this.gameObject);
        }
    }
}

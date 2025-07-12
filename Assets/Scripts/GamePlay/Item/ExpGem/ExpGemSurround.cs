using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpGemSurround : MonoBehaviour
{
    private ExpGem expGem;

    private void Start()
    {
        expGem = GetComponentInParent<ExpGem>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            expGem.GetData(collider.gameObject);
        }
    }
}

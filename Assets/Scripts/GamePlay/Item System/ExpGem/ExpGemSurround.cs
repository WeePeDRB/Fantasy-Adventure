using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpGemSurround : MonoBehaviour
{
    private ExpGemOld expGem;

    private void Start()
    {
        expGem = GetComponentInParent<ExpGemOld>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            expGem.GetData(collider.gameObject);
        }
    }
}

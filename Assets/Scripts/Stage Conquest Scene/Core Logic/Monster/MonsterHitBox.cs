using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHitBox : MonoBehaviour
{
    // Hero tag
    private const string HERO_TAG = "Hero";

    // Monster hit box events
    public event Action OnHeroEnterRange;
    public event Action OnHeroExitRange;

    // Collider check functions
    private void OTriggerEnter(Collider collider)
    {
        // Check if hero enter monster's range
        if (collider.gameObject.CompareTag(HERO_TAG))
        {
            OnHeroEnterRange?.Invoke();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        // Check if hero exit monster's range
        if (collider.gameObject.CompareTag(HERO_TAG))
        {
            OnHeroExitRange?.Invoke();
        }
    }
}

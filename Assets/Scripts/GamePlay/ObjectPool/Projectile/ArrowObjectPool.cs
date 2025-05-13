using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowObjectPool : ObjectPool
{
    // Class instance
    public static ArrowObjectPool Instance;

    // Projectile data
    [SerializeField] private SO_Weapon weaponData;
    [SerializeField] private int arrowQuantity;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        InstantiatePoolValue(weaponData.weaponPrefab, arrowQuantity);
        CreatePool();
    }
}

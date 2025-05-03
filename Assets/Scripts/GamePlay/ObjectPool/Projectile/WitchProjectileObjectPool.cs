using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchProjectileObjectPool : ObjectPool
{
    // Class instance
    public static WitchProjectileObjectPool Instance;

    // Projectile data
    [SerializeField] private SO_Projectile projectileData;
    [SerializeField] private int projectileQuantity;

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
        InstantiatePoolValue(projectileData.projectilePrefab, projectileQuantity);
        CreatePool();
    }
}

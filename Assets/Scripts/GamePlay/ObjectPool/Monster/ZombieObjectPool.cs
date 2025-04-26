using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieObjectPool : ObjectPool
{
    // Class instance
    public static ZombieObjectPool Instance;

    // Monster data
    [SerializeField] private SO_Monster zombieData;
    [SerializeField] private int zombieQuantity;

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
        InstantiatePoolValue(zombieData.monsterPrefab, zombieQuantity);
        CreatePool();
    }
}

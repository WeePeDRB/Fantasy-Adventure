using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieObjetPool : ObjectPool
{
    // Class instance
    public static ZombieObjetPool Instance;

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

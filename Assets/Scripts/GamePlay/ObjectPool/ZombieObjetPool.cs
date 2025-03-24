using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieObjetPool : ObjectPool
{
    // Class instance
    public ZombieObjetPool Instance;

    // Monster data
    [SerializeField] private SO_Monster monsterData;

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
        InstantiatePoolValue(monsterData, 20);
        CreatePool();
    }
}

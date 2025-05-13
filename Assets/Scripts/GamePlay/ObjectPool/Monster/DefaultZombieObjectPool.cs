using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultZombieObjectPool : ObjectPool
{
    //
    // FIELDS
    //

    // Class instance
    public static DefaultZombieObjectPool Instance;

    // Monster data
    [SerializeField] private SO_Monster zombieData;
    [SerializeField] private int zombieQuantity;

    //
    // FUNCTIONS
    //
    
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

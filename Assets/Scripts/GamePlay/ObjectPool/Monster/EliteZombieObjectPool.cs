using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteZombieObjectPool : ObjectPool
{
    //
    // FIELDS
    //

    // Class instance
    public static EliteZombieObjectPool Instance;

    // Monster data
    [SerializeField] private SO_Monster eliteZombieData;
    [SerializeField] private int eliteZombieQuantity;

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
        InstantiatePoolValue(eliteZombieData.monsterPrefab, eliteZombieQuantity);
        CreatePool();
    }
}

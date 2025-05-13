using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchObjectPool : ObjectPool
{
    //
    // FIELDS
    //

    // Class instance
    public static WitchObjectPool Instance;

    // Monster data
    [SerializeField] private SO_Monster witchData;
    [SerializeField] private int witchQuantity;

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
        InstantiatePoolValue(witchData.monsterPrefab, witchQuantity);
        CreatePool();
    }
}

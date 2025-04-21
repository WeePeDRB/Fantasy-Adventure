using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpGemObjectPool : ObjectPool
{
    // Class instance
    public static ExpGemObjectPool Instance;

    // Coin data
    [SerializeField] private SO_Item itemData;
    [SerializeField] private int ExpGemQuantity;

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
        InstantiatePoolValue(itemData.itemPrefab, ExpGemQuantity);
        CreatePool();
    }
}

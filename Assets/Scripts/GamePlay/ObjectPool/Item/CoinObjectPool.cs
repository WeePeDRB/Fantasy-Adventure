using UnityEngine;

public class CoinObjectPool : ObjectPool
{
    // Class instance
    public static CoinObjectPool Instance;

    // Coin data
    [SerializeField] private SO_Item itemData;
    [SerializeField] private int coinQuantity;

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
        InstantiatePoolValue(itemData.itemPrefab, coinQuantity);
        CreatePool();
    }
}

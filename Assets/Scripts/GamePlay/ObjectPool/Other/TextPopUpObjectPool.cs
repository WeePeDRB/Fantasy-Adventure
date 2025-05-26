using UnityEngine;

public class TextPopUpObjectPool : ObjectPool
{
    // Class instance
    public static TextPopUpObjectPool Instance;

    [SerializeField] private GameObject textGO;
    [SerializeField] private int textQuantity;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        InstantiatePoolValue(textGO, textQuantity);
        CreatePool();
    }
}

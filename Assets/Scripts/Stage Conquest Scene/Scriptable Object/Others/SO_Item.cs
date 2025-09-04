using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SO_Item : ScriptableObject
{
    // Item private id
    public string id;

    // Item prefab for the instantiate
    public GameObject itemPrefab;
    
}

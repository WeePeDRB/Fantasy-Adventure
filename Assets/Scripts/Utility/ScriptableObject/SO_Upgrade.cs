using System.Collections;
using System.Collections.Generic;
using GLTF.Schema;
using UnityEngine;

[CreateAssetMenu()]
public class SO_Upgrade : ScriptableObject
{
    // Upgrade id
    public string id;

    // Upgrade data
    public Sprite upgradeSprite;
    public string upgradeName;
    public string upgradeDescription;
}

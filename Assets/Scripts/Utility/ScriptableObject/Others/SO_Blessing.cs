using UnityEngine;

[CreateAssetMenu()]
public class SO_Blessing : ScriptableObject
{
    // Blessing id
    public string id;

    // Essential data
    public Sprite blessingSprite;
    public string blessingName;
    public string blessingDescription;
    public int blessingLevel;
    public float blessingValue;
}

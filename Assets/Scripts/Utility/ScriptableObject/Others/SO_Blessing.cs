using UnityEngine;

[CreateAssetMenu()]
public class SO_Blessing : ScriptableObject
{
    // Blessing data
    public string id;
    public string blessingName;
    public string blessingDescription;
    public int blessingLevel;
    public float blessingValue;
    public Sprite blessingSprite;
}

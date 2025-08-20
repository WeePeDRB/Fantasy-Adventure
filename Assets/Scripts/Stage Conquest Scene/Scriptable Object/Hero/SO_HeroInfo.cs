using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SO_HeroInfo : ScriptableObject
{
    public string id; // Private id
    public Sprite characterPortrait;
    public GameObject characterPrefab;
}

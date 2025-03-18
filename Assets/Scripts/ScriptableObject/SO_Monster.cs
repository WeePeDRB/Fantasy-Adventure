using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SO_Monster : ScriptableObject
{
    // Enemy private id
    public int id;

    // Enemy prefab for the instantiate
    public GameObject monsterPrefab;

    // Enemy stats
    public float maxHealth;
    public float speed;
    public float maxAmor;
    public float level;

    // Enemy rank
    public bool eliteMonster;
    public bool bossMonster;
}

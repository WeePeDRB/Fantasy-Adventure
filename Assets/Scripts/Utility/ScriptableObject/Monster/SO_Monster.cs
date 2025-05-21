using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SO_Monster : ScriptableObject
{
    // Monster private id
    public string id;

    // Monster prefab for the instantiate
    public GameObject monsterPrefab;

    // Monster basic stats
    public float damage;
    public float maxHealth;
    public float speed;
    public float attackSpeed;
    public int level;
    public int expDrop;

    // Monster special stats
    public float resistance;

    // Monster rank
    public bool eliteMonster;
    public bool bossMonster;
}

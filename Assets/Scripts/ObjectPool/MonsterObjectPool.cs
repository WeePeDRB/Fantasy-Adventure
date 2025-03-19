using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObjectPool : MonoBehaviour
{
    // Pool instance
    public static MonsterObjectPool Instance;

    // This monster pool will have multiple monster in it
    private Dictionary<int, Queue<GameObject>> monsterPool;

    // Monster data list
    [SerializeField] private SO_MonsterList monsterDataList;

    // Monster data
    private SO_Monster monsterData;
    
    // Max monster quantity each wave
    private int maxMonsterQuantity;

    // Instantiate monster pool
    private void InstantiateMonsterPool()
    {
        for(int i = 0; i < maxMonsterQuantity; i ++)
        {
            GameObject monster = Instantiate(monsterData.monsterPrefab);
            monster.SetActive(false);
        }
    }

    // Get monster from pool
    public GameObject GetMonster()
    {
        if (monsterPool.Count > 0)
        {

        }
        return new GameObject();
    }
}

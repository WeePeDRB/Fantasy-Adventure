using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    // Flag to check if still in combat
    private bool isInCombat;
    // Flag to enable/disable monster spawning 
    private bool isSpawningMonster;


    // Monster data list
    [SerializeField] private SO_MonsterList monsterDataList;
    // Monster data
    private SO_Monster monsterData;
    // Monster spawn position
    private Vector3 monsterSpawnPosition;
    // Max monster quantity each wave
    private int maxMonsterQuantity;
    // Current active monster
    private int activeMonsterQuantity;
    // 
    private Queue<GameObject> monsterPool;


    // Instantiate monster pool
    private void InstantiateMonsterPool()
    {
        for(int i = 0; i < maxMonsterQuantity; i ++)
        {
            GameObject monster = Instantiate(monsterData.monsterPrefab);
            monster.SetActive(false);
            monsterPool.Enqueue(monster);
        }
    }

    // Get monster from pool
    public GameObject GetMonster()
    {
        if (monsterPool.Count > 0)
        {
            return monsterPool.Dequeue();
        }
        return new GameObject();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObjectPool : MonoBehaviour
{
    // Pool instance
    public static MonsterObjectPool Instance;

    //
    private Dictionary<int, Queue<GameObject>> monsterPool;
    [SerializeField] private SO_MonsterList monsterDataList;
    private SO_Monster monsterData;
    private int maxMonsterQuantity;



    // Instantiate monster pool
    private void InstantiateMonsterPool()
    {
        for(int i = 0; i < maxMonsterQuantity; i ++)
        {
            GameObject monster = Instantiate(monsterData.monsterPrefab);
            MonsterBaseController monsterController = monster.GetComponent<MonsterBaseController>();
            monsterController.InstantiateCharacter(monsterData.maxHealth, monsterData.speed, monsterData.attackSpeed);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    //
    // Flags to control the manager behavior
    //
    // This flag is used to check if combat time is not over
    private bool isInCombat; 
    // This flag is used to check if the monster quantity has decreased and more
    // monsters need to be spawned
    private bool isSpawningMonster;

    // 
    private int maxMonsterQuantity;
    private int activeMonsterQuantity;
    private Queue<GameObject> monsterQueue;

    // Monster pool
    [SerializeField] private GameObject zombieObjectPool;

    // Monster position spawn
    private Transform monsterSpawnPosition;

    //
    // Logic handler for the gameplay time event
    //
    // Handler for combat start
    private void CombatStart()
    {
        isInCombat = true;
         Debug.Log("Start spawning monster !");
    }
    // Handler for combat end
    private void CombatEnd()
    {
        isInCombat = false;
        Debug.Log("End spawning monster !");
    }


    // Set up the monster object pool
    private void CreateMonsterPool()
    {
        Instantiate(zombieObjectPool);
    }


    // Check monster spawn status 
    private void MonsterQuantityManagerment()
    {
        activeMonsterQuantity = monsterQueue.Count;
        if (activeMonsterQuantity < maxMonsterQuantity && isInCombat)
        {
            isSpawningMonster = true;
        }
    }


    // Check and locate player position and assign it to monster spawn position
    private void CheckPlayerPosition()
    {
        monsterSpawnPosition = CharacterBaseController.Instance.transform;
    }

    // 
    private void AddingMonster()
    {
        if (isSpawningMonster)
        {
            monsterQueue.Enqueue(ZombieObjetPool.Instance.GetObject(monsterSpawnPosition));
        }
    }

    private void Awake()
    {
        // Listen to gameplay time event
        GameplayTimeManager.OnStartCombat += CombatStart;
        GameplayTimeManager.OnEndCombat += CombatEnd;
    }


    private void Start()
    {
        maxMonsterQuantity = 30;
        monsterQueue = new Queue<GameObject>();
        activeMonsterQuantity = monsterQueue.Count;
        CreateMonsterPool();
    }


    private void Update()
    {
        MonsterQuantityManagerment();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawnManager : MonoBehaviour
{
    //
    // FIELDS
    //

    //
    private bool isInCombat;

    // Reference
    private HeroBaseController heroBaseController;

    // 
    [SerializeField]private int monsterMaxQuantity;
    private int monsterQuantity;
    private List<MonsterBaseController> monsterList;

    // Coroutine
    private Coroutine spawnCoroutine;
    
    //
    [SerializeField] private LayerMask groundLayer;


    //
    // FUNCTIONS
    //

    // Control combat duration
    private void StartCombat()
    {
        isInCombat = true;
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnMonsterCoroutine());
        }
    }
    private void EndCombat()
    {
        isInCombat = false;
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    // Control spawn monster logic
    // Spawn logic
    private IEnumerator SpawnMonsterCoroutine()
    {
        while (isInCombat)
        {
            yield return new WaitForSeconds(1f);
            if (monsterQuantity < monsterMaxQuantity)
            {
                SpawnMonster();
            }
        }
    }
    private void SpawnMonster()
    {
        //
        GameObject monster = new GameObject();
        monster.transform.position = GetRandomOffscreenPosition();
        
        //
        int monsterType = Random.Range(0,1);
        
        switch (monsterType)
        {
            case 0:
                    // 
                    GameObject monsterGameObj = ZombieObjectPool.Instance.GetObject(monster.transform);
                    MonsterBaseController monsterBaseController = monsterGameObj.GetComponent<MonsterBaseController>();
                    monsterBaseController.OnMonsterDead += OnMonsterDead;
                    if (monsterBaseController.MonsterBeHaviorState == MonsterBehavior.Dead) monsterBaseController.ResetMonsterState();
                    break;
            case 1:
                    //
                    Debug.Log("Monster 2 is not ready !");
                    break;
        }
        monsterQuantity ++;
        
    }
    // Return monster logic
    //  
    private void OnMonsterDead(object sender, MonsterBaseController.OnMonsterDeadEventArgs monsterDeadEventArgs)
    {
        // Take variable
        MonsterBaseController monsterBaseController = monsterDeadEventArgs.monsterBaseController;
        // Unsub the event
        monsterBaseController.OnMonsterDead -= OnMonsterDead;
        // Return the object to pool
        StartCoroutine(ReturnMonsterCoroutine(monsterBaseController.gameObject));
        // Adjust the quantity
        monsterQuantity --;
    }
    //
    private IEnumerator ReturnMonsterCoroutine(GameObject returnPoolMonster)
    {
        yield return new WaitForSeconds(3.5f);
        ZombieObjectPool.Instance.ReturnObject(returnPoolMonster);
    }

    // Support function
    // Get random position to spawn
    private Vector3 GetRandomOffscreenPosition()
    {
        // Get postion
        Vector3 spawnPos = Vector3.zero;
        Vector3 heroPos = heroBaseController.transform.position;

        // Get random edge
        int edge = Random.Range(0,4);

        float xOffset = Random.Range(-18f, 18f);
        float zOffset = Random.Range(-6f, 15f);

        switch (edge)
        {
            case 0: 
                    // Up
                    spawnPos = heroPos + new Vector3(xOffset, 0f, 15f);
                    break;
            case 1: 
                    // Down
                    spawnPos = heroPos + new Vector3(xOffset, 0f, -6f);
                    break;            
            case 2: 
                    // Left
                    spawnPos = heroPos + new Vector3(-18f, 0f, zOffset);
                    break;            
            case 3: 
                    // Right
                    spawnPos = heroPos + new Vector3(18f, 0f, zOffset);
                    break;
        }
        if (!CheckGround(spawnPos)) GetRandomOffscreenPosition();

        return spawnPos;
    }
    // Check if the spwan position is on the ground
    private bool CheckGround(Vector3 spawnPos)
    {
        // Set a raycast
        Ray ray = new Ray(spawnPos + Vector3.up * 50f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            return true;
        }
        return false;
    }

    private void Start()
    {
        //
        heroBaseController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseController>();

        //
        TimerManager.OnStartCombat += StartCombat;
        TimerManager.OnEndCombat += EndCombat;
    }
}

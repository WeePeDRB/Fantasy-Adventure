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

    // Initialize hero list
    private List<HeroBaseController> heroList;

    // Spawning monster logic
    private HeroBaseController heroBaseController;
    private bool isInCombat;
    private int monsterMaxQuantity;
    private int monsterQuantity;
    private Coroutine spawnCoroutine;
    [SerializeField] private LayerMask groundLayer;


    //
    // FUNCTIONS
    //

    // CONTROL COMBAT DURATION
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

    // CONTROL MONSTER SPAWN
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
        GameObject monsterGameObj = null;

        int monsterType = 2;// Random.Range(0, 2); // 0 - Default, 1 - Elite, 2 - Witch

        switch (monsterType)
        {
            case 0:
                monsterGameObj = DefaultZombieObjectPool.Instance.GetObject(monster.transform);
                break;

            case 1:
                monsterGameObj = EliteZombieObjectPool.Instance.GetObject(monster.transform);
                break;

            case 2:
                monsterGameObj = WitchObjectPool.Instance.GetObject(monster.transform);
                break;
        }

        if (monsterGameObj != null)
        {
            MonsterBaseController monsterBaseController = monsterGameObj.GetComponent<MonsterBaseController>();
            monsterBaseController.GetHeroList(heroList);
            monsterBaseController.OnMonsterDead += OnMonsterDead;
            monsterBaseController.ResetMonsterState();
        }

        monsterQuantity++;
    }

    // RETURN MONSTER
    private void OnMonsterDead(object sender, OnMonsterDeadEventArgs monsterDeadEventArgs)
    {
        // Take variable
        MonsterBaseController monsterBaseController = monsterDeadEventArgs.monsterBaseController;
        // Unsub the event
        monsterBaseController.OnMonsterDead -= OnMonsterDead;
        // Return the object to pool
        StartCoroutine(ReturnMonsterCoroutine(monsterBaseController));
        // Adjust the quantity
        monsterQuantity --;
    }
    //
    private IEnumerator ReturnMonsterCoroutine(MonsterBaseController monsterBaseController)
    {
        yield return new WaitForSeconds(3.5f);
        if (monsterBaseController is ZombieController)
        {
            DefaultZombieObjectPool.Instance.ReturnObject(monsterBaseController.gameObject);
        }
        else if (monsterBaseController is EliteZombieController)
        {
            EliteZombieObjectPool.Instance.ReturnObject(monsterBaseController.gameObject);
        }
        else if (monsterBaseController is WitchController)
        {
            WitchObjectPool.Instance.ReturnObject(monsterBaseController.gameObject);
        }
    }

    // SUPPORT FUNCTIONS
    // Get random position to spawn
    private Vector3 GetRandomOffscreenPosition()
    {
        // Get hero from list
        GetHero();

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

    // Get random hero
    private void GetHero()
    {
        int randomNumber = Random.Range(0,heroList.Count - 1);
        heroBaseController = heroList[randomNumber];
    }

    private void Start()
    {
        heroList = GameUtility.InitializeHeroList();
        //
        TimerManager.OnStartCombat += StartCombat;
        TimerManager.OnEndCombat += EndCombat;

        monsterMaxQuantity = 3;
    }
}

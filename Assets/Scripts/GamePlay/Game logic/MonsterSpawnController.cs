using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawnController : MonoBehaviour
{
    //
    // FIELDS
    //

    //
    [SerializeField] private SceneController sceneController;

    // Initialize hero list
    private List<HeroBaseController> heroList;
    private HeroBaseController heroBaseController;

    // Spawning monster logic
    private int monsterMaxQuantity;
    private int monsterQuantity;
    private Coroutine spawnCoroutine;
    [SerializeField] private LayerMask groundLayer;

    // Round level
    private int roundLevel;

    // Monster kill count
    private int killCount;
    public int KillCount
    {
        get { return killCount; }
    }
    [SerializeField] private TextMeshProUGUI killCountText;

    //
    // FUNCTIONS
    //

    // CONTROL COMBAT DURATION
    private void StartCombat()
    {
        spawnCoroutine = StartCoroutine(SpawnMonsterCoroutine()); 
    }
    private void EndCombat()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    // TEMPO CONTROL
    private void OnLevelUp()
    {
        roundLevel++;
        if (roundLevel % 2 == 0) monsterMaxQuantity += 5;
    }

    private void OnBigWaveStart()
    {
        monsterMaxQuantity *= 2;
    }

    private void OnBigWaveEnd()
    {
        monsterMaxQuantity /= 2;
    }

    // CONTROL MONSTER SPAWN
    // Spawn logic
    private IEnumerator SpawnMonsterCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
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

        int monsterType = Random.Range(0,3); // 0 - Default, 1 - Elite, 2 - Witch

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
            // Reset monster data and state
            MonsterBaseControllerOld monsterBaseController = monsterGameObj.GetComponent<MonsterBaseControllerOld>();
            // Get hero list (in multiplayer mode)
            monsterBaseController.GetHeroList(heroList);
            // Subscribe to monster dead event
            monsterBaseController.OnMonsterDead += OnMonsterDead;
            // Reset monster state 
            monsterBaseController.ResetMonsterState();
            // Check monster level
            if (monsterBaseController.MonsterStats.Level < roundLevel) monsterBaseController.MonsterStats.LevelUp(roundLevel);
        }

        monsterQuantity++;
        Destroy(monster);
    }

    // RETURN MONSTER
    // Get data from dead monster
    private void OnMonsterDead(object sender, OnMonsterDeadEventArgs monsterDeadEventArgs)
    {
        // Take variable
        MonsterBaseControllerOld monsterBaseController = monsterDeadEventArgs.monsterBaseController;
        // Unsub the event
        monsterBaseController.OnMonsterDead -= OnMonsterDead;
        // Return the object to pool
        StartCoroutine(ReturnMonsterCoroutine(monsterBaseController));
        // Adjust the quantity
        monsterQuantity--;

        //
        MonsterKillCount();
    }
    // Return monster to object pool
    private IEnumerator ReturnMonsterCoroutine(MonsterBaseControllerOld monsterBaseController)
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

    // KILL COUNT
    private void MonsterKillCount()
    {
        killCount++;
        killCountText.text = killCount.ToString();
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
        int edge = Random.Range(0, 4);

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
        TimerController.OnEndCombat += EndCombat;
        TimerController.OnLevelUp += OnLevelUp;
        TimerController.OnBigWaveStart += OnBigWaveStart;
        TimerController.OnBigWaveEnd += OnBigWaveEnd;
        monsterMaxQuantity = 5;

        //
        sceneController.OnGameStart += StartCombat;
    }
}

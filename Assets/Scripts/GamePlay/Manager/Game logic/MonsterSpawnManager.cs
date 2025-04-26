using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    //
    // FIELDS
    //

    // Reference
    private HeroBaseController heroBaseController;

    // 
    private int monsterMaxQuantity;
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
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnMonsterCoroutine());
        }
    }
    private void EndCombat()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    // Controle spawn monster logic
    private IEnumerator SpawnMonsterCoroutine()
    {
        yield return null;
    }
    private void SpawnMonster()
    {
        
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

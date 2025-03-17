using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    // 
    public static MonsterSpawnManager Instance { get; private set; }

    
    //
    private bool isSpawningMonster;
    private SO_MonsterList monsterDataList;

    //
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There are more than one player instance !");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}

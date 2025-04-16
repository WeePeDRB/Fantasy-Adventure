using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

public class GameplaySceneManager : MonoBehaviour
{

    //
    // Character instantiate funtions 
    //
    // Set up to instantiate the character gameobject
    private int heroId;
    private SO_Hero heroData;
    
    [SerializeField] private SO_HeroList heroDataList;

    // Get and set value for the character data
    private void GetCharacterData()
    {
        heroId = PlayerPrefs.GetInt("CharacterID");
        heroData = heroDataList.GetCharacterById(heroId);
    }

    // Instantiate character
    private void InstantiateCharacter()
    {
        if (heroData != null)
        {
            // Instantiate game object
            GameObject player = Instantiate(heroData.heroPrefab, Vector3.zero, Quaternion.identity);
            // Get character controller
            HeroBaseController characterBaseController = player.GetComponent<HeroBaseController>();
            // Instantiate character
            characterBaseController.InstantiateDash( 5f, 13f, 5f, 3f );
        }
        else
        {
            Debug.LogError("Can not find character data");
        }
    }


    //
    // Timer instantiate function
    //
    [SerializeField] private GameObject gameplayerTimeManager;
    private void InstantiateGameplayTimeManager()
    {

    }


    //
    // MonsterSpawn  instantiate function
    //
    [SerializeField] private GameObject monsterSpawnManager;
    private void InstantiateMonsterSpawnManager()
    {

    }


    //
    private void Start()
    {
        GetCharacterData();
        InstantiateCharacter();
    }
}

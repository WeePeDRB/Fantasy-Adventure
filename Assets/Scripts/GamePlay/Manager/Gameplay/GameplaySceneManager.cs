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
    private int characterId;
    private SO_Character characterData;
    
    [SerializeField] private SO_CharacterList characterDataList;

    // Get and set value for the character data
    private void GetCharacterData()
    {
        characterId = PlayerPrefs.GetInt("CharacterID");
        characterData = characterDataList.GetCharacterById(characterId);
    }

    // Instantiate character
    private void InstantiateCharacter()
    {
        if (characterData != null)
        {
            // Instantiate game object
            GameObject player = Instantiate(characterData.characterPrefab, Vector3.zero, Quaternion.identity);
            // Get character controller
            CharacterBaseController characterBaseController = player.GetComponent<CharacterBaseController>();
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

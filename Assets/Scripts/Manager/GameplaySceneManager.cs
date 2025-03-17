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
    private SO_CharacterList characterDataList;

    // Get and set value for the character data
    private void GetCharacterData()
    {
        characterId = PlayerPrefs.GetInt("SelectedCharacterID");
        characterData = characterDataList.GetCharacterById(characterId);
    }

    // Instantiate character
    private void InstantiateCharacter()
    {
        if (characterData != null)
        {
            Instantiate(characterData.characterPrefab, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Can not find character data");
        }
    }

    //
    // Enemy spawn instantiate
    //
    
}

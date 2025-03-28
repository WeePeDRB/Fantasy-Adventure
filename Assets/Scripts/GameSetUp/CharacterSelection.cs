using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterSelection : MonoBehaviour
{
    // Event for the select character UI
    // This event will notify and send CharacterData to DisplayUI, 
    // allowing it to update the displayed information.
    public static event EventHandler<CharacterData> OnChangeCharacter;
    public class CharacterData : EventArgs
    {
        public SO_Character characterData;
    }
    [SerializeField] private SO_CharacterList characterDataList;
    private SO_Character characterData;

    //
    private int currentCharacterId;

    //
    private List<GameObject> characterSelectionList;


    // Instantiate character model from the character list scriptable object
    private void InstantiateCharacterList()
    {
        characterSelectionList = new List<GameObject>();
        foreach (SO_Character characterData in characterDataList.characterDataList)
        {
            GameObject characterModel = Instantiate(characterData.characterPrefab);
            characterModel.transform.position = Vector3.zero;
            characterSelectionList.Add(characterModel);
        }
    }

    // This function will update CharacterData whenever the user selects a different 
    // character and send this data to DisplayUI through the OnChangeCharacter event.
    private void OnChangeCharacterHandler()
    {
        characterData = characterDataList.GetCharacterById(currentCharacterId);
        OnChangeCharacter?.Invoke(this, new CharacterData{characterData = characterData});
    }


    // Select character
    public void SelectNextCharacter()
    {
        DisableCharacter();
        currentCharacterId ++;
        if (currentCharacterId > characterSelectionList.Count - 1) currentCharacterId = 0;
        ShowCharacter();
        OnChangeCharacterHandler();
    }
    public void SelectPreviousCharacter()
    {
        DisableCharacter();
        currentCharacterId --;
        if (currentCharacterId < 0) currentCharacterId = characterSelectionList.Count - 1;
        ShowCharacter();
        OnChangeCharacterHandler();
    }


    // Show character
    private void ShowCharacter()
    {
        characterSelectionList[currentCharacterId].SetActive(true);
    }


    // Disable character
    private void DisableAllCharacter()
    {
        foreach (GameObject character in characterSelectionList)
        {
            character.SetActive(false);
        }
    }
    private void DisableCharacter()
    {
        Debug.Log("Disable character");
        characterSelectionList[currentCharacterId].SetActive(false);
    }

    //
    private void Start()
    {
        InstantiateCharacterList();
        OnChangeCharacterHandler();
        DisableAllCharacter();
        currentCharacterId = 0;
        ShowCharacter();
    }

}

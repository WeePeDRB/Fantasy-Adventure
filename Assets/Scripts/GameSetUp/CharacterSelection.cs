using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterSelection : MonoBehaviour
{
    //
    public static event EventHandler<CharacterInfo> OnChangeCharacter;
    public class CharacterInfo : EventArgs
    {
        public SO_Character characterData;
    }

    //
    private int currentCharacterId;

    //
    [SerializeField] private List<GameObject> characterSelectionList;

    // Select character
    public void SelectNextCharacter()
    {
        Debug.Log(characterSelectionList.Count);
        DisableCharacter();
        currentCharacterId ++;
        if (currentCharacterId >= characterSelectionList.Count) currentCharacterId = 0;
        ShowCharacter();
    }
    public void SelectPreviousCharacter()
    {
        DisableCharacter();
        currentCharacterId --;
        if (currentCharacterId <= 0) currentCharacterId = characterSelectionList.Count;
        ShowCharacter();
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
        characterSelectionList[currentCharacterId].SetActive(false);
    }

    private void Start()
    {
        DisableAllCharacter();
        currentCharacterId = 0;
        ShowCharacter();
    }

}

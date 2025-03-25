using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    //
    private SO_CharacterList characterDataList;

    // Event for character data
    public static event EventHandler OnSelectCharacterData;
    public class CharacterData : EventArgs
    {
        public SO_Character characterData;
    }

    //
    private int currentCharacterId;


    // Select character
    private void SelectNextCharacter()
    {
        currentCharacterId ++;
        if (currentCharacterId > characterDataList.characterDataList.Count) currentCharacterId = 1;
    }
    private void SelectPreviousCharacter()
    {
        currentCharacterId --;
        if (currentCharacterId < 1) currentCharacterId = characterDataList.characterDataList.Count;
    }


    // Show character
    private void ShowCharacter()
    {
        
    }

}

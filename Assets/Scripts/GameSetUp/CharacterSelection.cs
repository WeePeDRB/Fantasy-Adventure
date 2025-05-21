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
    public static event EventHandler<HeroData> OnChangeHero;
    public class HeroData : EventArgs
    {
        public SO_Hero heroData;
    }
    //[SerializeField] private SO_HeroList heroDataList;
    private SO_Hero heroData;

    //
    private int currentHeroId;

    //
    private List<GameObject> heroSelectionList;


    // Instantiate character model from the character list scriptable object
    private void InstantiateCharacterList()
    {
        // heroSelectionList = new List<GameObject>();
        // foreach (SO_Hero characterData in heroDataList.heroList)
        // {
        //     GameObject characterModel = Instantiate(characterData.heroPrefab);
        //     characterModel.transform.position = Vector3.zero;
        //     heroSelectionList.Add(characterModel);
        // }
    }

    // This function will update CharacterData whenever the user selects a different 
    // character and send this data to DisplayUI through the OnChangeCharacter event.
    private void OnChangeCharacterHandler()
    {
        //heroData = heroDataList.GetCharacterById(currentHeroId);
        OnChangeHero?.Invoke(this, new HeroData{heroData = heroData});
    }


    // Select character
    public void SelectNextCharacter()
    {
        DisableCharacter();
        currentHeroId ++;
        if (currentHeroId > heroSelectionList.Count - 1) currentHeroId = 0;
        ShowCharacter();
        OnChangeCharacterHandler();
    }
    public void SelectPreviousCharacter()
    {
        DisableCharacter();
        currentHeroId --;
        if (currentHeroId < 0) currentHeroId = heroSelectionList.Count - 1;
        ShowCharacter();
        OnChangeCharacterHandler();
    }


    // Show character
    private void ShowCharacter()
    {
        heroSelectionList[currentHeroId].SetActive(true);
    }


    // Disable character
    private void DisableAllCharacter()
    {
        foreach (GameObject character in heroSelectionList)
        {
            character.SetActive(false);
        }
    }
    private void DisableCharacter()
    {
        heroSelectionList[currentHeroId].SetActive(false);
    }

    //
    private void Start()
    {
        InstantiateCharacterList();
        OnChangeCharacterHandler();
        DisableAllCharacter();
        currentHeroId = 0;
        ShowCharacter();
    }

}

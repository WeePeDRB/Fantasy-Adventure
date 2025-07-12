using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterSelection : MonoBehaviour
{
    //
    // FIELDS
    //
    
    //
    [SerializeField] private List<SO_Hero> heroDataList;
    [SerializeField] private GameObject spawnPosition;
    private int currentHeroInt;
    public event EventHandler<HeroData> OnChangeHero;

    //
    private bool isReadyToChange;
    private float changeCountDown;

    // Instantiate character model from the character list scriptable object
    private void InitializeCharacterModel()
    {
        foreach (SO_Hero heroData in heroDataList)
        {
            GameObject heroPrefab = Instantiate(heroData.heroPrefab, spawnPosition.transform);
            HeroVFX heroVFX = heroPrefab.GetComponentInChildren<HeroVFX>();
            heroVFX.GetData(this, heroData);
        }

        currentHeroInt = 0;
        isReadyToChange = true;
        changeCountDown = 1f;
        OnChangeHero?.Invoke(this, new HeroData { heroData = heroDataList[currentHeroInt] });
    }

    // Select character
    public void SelectNextCharacter()
    {
        if (isReadyToChange)
        {
            //
            currentHeroInt++;
            if (currentHeroInt >= heroDataList.Count)
            {
                currentHeroInt = 0;
            }
            OnChangeHero?.Invoke(this, new HeroData { heroData = heroDataList[currentHeroInt] });

            //
            StartCoroutine(ChangeHeroCoroutine());
        }
    }
    public void SelectPreviousCharacter()
    {
        if (isReadyToChange)
        {
            currentHeroInt--;
            if (currentHeroInt < 0)
            {
                currentHeroInt = heroDataList.Count - 1;
            }
            OnChangeHero?.Invoke(this, new HeroData { heroData = heroDataList[currentHeroInt] });

            //
            StartCoroutine(ChangeHeroCoroutine());
        }
    }

    private IEnumerator ChangeHeroCoroutine()
    {
        yield return new WaitForSeconds(changeCountDown);
        if (!isReadyToChange) isReadyToChange = true;
    }

    //
    private void Start()
    {
        InitializeCharacterModel();
    }

}

public class HeroData : EventArgs
{
    public SO_Hero heroData;
}
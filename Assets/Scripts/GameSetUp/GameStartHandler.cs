using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartHandler : MonoBehaviour
{
    private int characterId;
    private int gameDifficulty;

    private void OnChangeCharacterHandler(object sender, CharacterSelection.HeroData heroData)
    {   
        //characterId = heroData.heroData.id;
    }

    private void OnChaneDifficultyHandler(object sender, DifficultySelection.Difficulty difficulty)
    {
        gameDifficulty = difficulty.gameDifficulty;
    }

    public void OnStartGame()
    {
        PlayerPrefs.SetInt("CharacterID",characterId);
        PlayerPrefs.SetInt("GameDifficulty",gameDifficulty);
        SceneManager.LoadScene("GameplayScene");
    }

    private void Awake()
    {
        CharacterSelection.OnChangeHero += OnChangeCharacterHandler;
        DifficultySelection.OnSelectGameDifficulty += OnChaneDifficultyHandler;
    }
}

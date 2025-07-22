using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class GameStartHandler : MonoBehaviour
{
    //
    [SerializeField] private CharacterSelection characterSelection;
    [SerializeField] private DifficultySelection difficultySelection;
    private int characterId;
    private int gameDifficulty;

    // UI
    [SerializeField] private Image background;

    private void OnChangeCharacterHandler(object sender, HeroData heroData)
    {
        characterId = int.Parse(heroData.heroData.id);
    }

    private void OnChaneDifficultyHandler(object sender, Difficulty difficulty)
    {
        gameDifficulty = difficulty.gameDifficulty;
    }

    public void OnStartGame()
    {
        PlayerPrefs.SetInt("CharacterID", characterId);
        PlayerPrefs.SetInt("GameDifficulty", gameDifficulty);
        background.DOFade(1, 1f);
        StartCoroutine(LoadingCoroutine());   
    }

    private IEnumerator LoadingCoroutine()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameplayScene");
    }

    private void Awake()
    {
        characterSelection.OnChangeHero += OnChangeCharacterHandler;
        difficultySelection.OnSelectGameDifficulty += OnChaneDifficultyHandler;
    }
}

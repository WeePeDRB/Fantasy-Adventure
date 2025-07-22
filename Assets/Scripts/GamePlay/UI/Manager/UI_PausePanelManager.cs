using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PausePanelManager : MonoBehaviour
{
    //
    // FIELDS
    //

    [SerializeField] private GameObject pausePanel;
    private bool pauseGame;
    private HeroBaseController heroBaseController;

    //
    // FUNCTIONS
    //

    private IEnumerator InitialPausePanel()
    {
        yield return new WaitForSeconds(0.5f);
        heroBaseController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseController>();
        pausePanel.SetActive(false);
        pauseGame = false;
    }

    private void OnPauseGame()
    {
        
        if (heroBaseController.HeroHealthState == HeroHealthState.Alive)
        {
            if (pauseGame == false)
            {
                pausePanel.SetActive(true);
                pauseGame = true;
                GameUtility.PauseGame();
            }
            else if (pauseGame == true)
            {
                pausePanel.SetActive(false);
                pauseGame = false;
                GameUtility.UnpauseGame();
            }
        }
        else
        {
            return;
        }
    }

    public void Resume()
    {
        OnPauseGame();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void ChangeHero()
    {
       SceneManager.LoadScene("HeroSelectionScene");
    }

    public void Exit()
    {
       
    }

    private void Start()
    {

        StartCoroutine(InitialPausePanel());
        GameInput.OnPauseGame += OnPauseGame;
    }
}

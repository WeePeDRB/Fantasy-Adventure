using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using Cinemachine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //
    // FIELDS
    //

    // Initial set up
    [SerializeField] private Image background;
    [SerializeField] private List<SO_Hero> heroList;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    public event Action OnGameStart;

    //
    [SerializeField] private UI_DeadPanelManager deadPanel;
    [SerializeField] private UI_PausePanelManager pausePanel;

    private Coroutine cameraCoroutine;

    //
    // FUNCTIONS
    //

    //
    // Enter scene initialize

    // Backgruond start
    private IEnumerator StartGame()
    {
        background.DOFade(0, 1.5f);
        yield return new WaitForSeconds(1.5f);
        OnGameStart?.Invoke();
    }

    // Initialize when game start
    private void InitialHero()
    {
        // Instantiate hero
        float characteId = PlayerPrefs.GetInt("CharacterID");

        if (characteId != 0)
        {
            foreach (SO_Hero heroData in heroList)
            {
                if (int.Parse(heroData.id) == characteId)
                {
                    Instantiate(heroData.heroPrefab, new Vector3(200, 0, 100), new Quaternion(0, 0, 0, 0));
                    return;
                }
            }
        }
        else
        {
            Debug.LogError("Character ID is missing");
            Instantiate(heroList[0].heroPrefab, new Vector3(200, 0, 100), new Quaternion(0, 0, 0, 0));
            Debug.Log("Instantiate Paladin !");
        }
    }
    // Initialize virtual cam with new hero
    private IEnumerator InitalVirtualCam()
    {
        yield return new WaitForSeconds(0.1f);
        virtualCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        //
        InitialHero();
        StartCoroutine(InitalVirtualCam());
        StartCoroutine(StartGame());
    }
}

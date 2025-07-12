using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_DeadPanelManager : MonoBehaviour
{
    // Reference
    [SerializeField] private TimerController timerManager;
    [SerializeField] private MonsterSpawnController monsterSpawnManager;
    private HeroBaseController heroBaseController;

    // UI Component
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI notification;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI timerValue;
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private TextMeshProUGUI killCountValue;
    [SerializeField] private Button restartBtn;
    [SerializeField] private TextMeshProUGUI restartText;

    private void SetUIComponent()
    {
        deadPanel.SetActive(true);

        //
        timerValue.text = string.Format("{0:00}:{1:00}", timerManager.minute, timerManager.second);
        killCountValue.text = monsterSpawnManager.KillCount.ToString();

        //
        UIComponentTween();
    }

    private void UIComponentTween()
    {
        background.DOFade(1f, 4f);
        notification.DOFade(1f, 6f);

        timerText.DOFade(1f, 7f);
        timerValue.DOFade(1f, 7f);

        killCountText.DOFade(1f, 7f);
        killCountValue.DOFade(1f, 7f);

        restartBtn.image.DOFade(1f, 7f);
        restartText.DOFade(1f, 7f);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {
        heroBaseController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseController>();
        heroBaseController.OnHeroDead += SetUIComponent;
    }
}

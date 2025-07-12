using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    //
    // FIELDS
    //

    //
    [SerializeField] private SceneController sceneController;

    // Combat timing event
    public static event Action OnStartCombat;
    public static event Action OnLevelUp;
    public static event Action OnBigWaveStart;
    public static event Action OnBigWaveEnd;
    public static event Action OnEndCombat;

    // Timer value
    public int timer;
    public int second;
    public int minute;

    // UI Component
    [SerializeField] TextMeshProUGUI timerText;

    //
    // FUNCTIONS
    //

    // Timer check 
    private void MonsterLevelUp()
    {
        if (minute > 0)
        {
            if (minute % 2 == 0 && second == 0)
            {
                Debug.Log("Monster level up");
                OnLevelUp?.Invoke();
            }
        }
    }
    private void MonsterWave()
    {
        if (minute > 0)
        {
            if (minute % 4 == 0 && second == 0)
            {
                Debug.Log("Monster big wave");
                OnBigWaveStart?.Invoke();
                StartCoroutine(BigWaveCoroutine());
            }
        }
    }

    // Coroutine 
    private void StartTimerCoroutine()
    {
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // Checking monster level up
            MonsterLevelUp();

            // Checking for monster wave 
            MonsterWave();

            // Set timer
            timer++;
            minute = timer / 60;
            second = timer % 60;
            // Set timer UI
            timerText.text = string.Format("{0:00}:{1:00}", minute, second);
        }
    }

    private IEnumerator BigWaveCoroutine()
    {
        yield return new WaitForSeconds(30f);
        OnBigWaveEnd?.Invoke();
    }

    private void Start()
    {
        sceneController.OnGameStart += StartTimerCoroutine;
    }
}

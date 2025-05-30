using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    //
    // FIELDS
    //

    public static TimerManager Instance;

    // Timer value 
    private float maxCombatTime;
    private float currenCombatTime;
    
    // Level
    private int roundLevel;

    // Combat timing event
    public static event Action OnStartCombat;
    public static event Action OnEndCombat;

    //
    [SerializeField] TextMeshProUGUI timerText;

    //
    // PROPERTIES
    //
    public int RoundLevel
    {
        get { return roundLevel; }
    }

    //
    // FUNCTIONS
    //

    // Initial time manager set up    
    private void InstantiateTimer()
    {
        maxCombatTime = 30f;
        roundLevel = 1;
    }

    // Coroutine countdown
    private IEnumerator CountDownRoutine()
    {
        while (currenCombatTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currenCombatTime --; 
        }
        EndCombat();
    }

    // Invoke event to control timer
    private void StartCombat()
    {
        currenCombatTime = maxCombatTime;
        OnStartCombat?.Invoke();
        StartCoroutine(CountDownRoutine());
    }
    private void EndCombat()
    {
        OnEndCombat?.Invoke();
    }

    //
    private void Awake()
    {
        InstantiateTimer();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCombat();
        }
        timerText.text = currenCombatTime.ToString();
    }
}

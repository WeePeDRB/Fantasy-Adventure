using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayTimeManager : MonoBehaviour
{
    // Combat timing event
    public static event Action OnStartCombat;
    public static event Action OnEndCombat;
    public static event EventHandler<NextLevelEventArgs> OnNextLevel;
    public class NextLevelEventArgs : EventArgs
    {
        public int roundLevel;
    }


    // Timing value 
    private float maxCombatTime;
    private float currenCombatTime;
    

    //
    private int roundLevel;

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

    // Invoke event
    private void StartCombat()
    {
        currenCombatTime = maxCombatTime;
        OnStartCombat?.Invoke();
        StartCoroutine(CountDownRoutine());
    }

    // Invoke evnt and send next round level
    private void EndCombat()
    {
        OnEndCombat?.Invoke();
        OnNextLevel?.Invoke(this, new NextLevelEventArgs{ roundLevel = this.roundLevel ++});
    }

    private void Awake()
    {
        InstantiateTimer();
    }
    
    private void Start()
    {
        StartCombat();
    }
}

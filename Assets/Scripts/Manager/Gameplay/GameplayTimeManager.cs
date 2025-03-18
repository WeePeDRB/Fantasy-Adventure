using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayTimeManager : MonoBehaviour
{
    // Combat timing event
    public static event Action OnStartCombat;
    public static event EventHandler<EndCombatEventArgs> OnEndCombat;
    public class EndCombatEventArgs : EventArgs
    {
        public int nextRoundLevel;
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
        OnEndCombat?.Invoke(this, new EndCombatEventArgs{ nextRoundLevel = this.roundLevel ++});
    }

    private void Awak()
    {
        InstantiateTimer();
    }
    
    private void Start()
    {
        StartCombat();
    }
}

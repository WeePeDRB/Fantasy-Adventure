using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StatsManager : MonoBehaviour
{
    //
    // FIELDS
    //

    // Reference
    private HeroBaseController heroBaseController;
    private HeroStats heroStats;

    // UI COMPONENTS
    [SerializeField] private List<UI_StatComponent> statUIList;

    //
    // FUNCTIONS
    //

    // INITIALIZE
    private void InitializeStatsManager()
    {
        heroBaseController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseController>();
        heroStats = heroBaseController.HeroStats;
    }

    private void InitializeStatsUI()
    {
        foreach (UI_StatComponent statComponent in statUIList)
        {
            statComponent.SetUIComponent();
        }
    }

    private void UpdateStatUI()
    {
        statUIList[0].UpdateComponentValue(heroStats.DamageAmplifier);
        statUIList[1].UpdateComponentValue(heroStats.Resistance);
        statUIList[2].UpdateComponentValue(heroStats.Speed);
    }

    private void Start()
    {
        InitializeStatsManager();
        InitializeStatsUI();
    }

    private void Update()
    {
        UpdateStatUI();
    }
}

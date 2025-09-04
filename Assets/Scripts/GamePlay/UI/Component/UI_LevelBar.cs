using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_LevelBar : MonoBehaviour
{
    //
    // FIELDS
    //

    // Reference
    private HeroController heroController;

    // UI COMPONENTS
    [SerializeField] private Slider slider;

    //
    // FUNCTIONS
    //

    // INSTANTIATE
    private void InstantiateLevelBar()
    {
        // Take hero controller reference
        heroController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroController>();
        
        // Set value to slider
        UpdateExpStatus();
    }

    private void UpdateExpStatus()
    {
        slider.maxValue = heroController.StatsController.ExpRequire;
        slider.value = heroController.StatsController.Exp;
    }

    // Update health
    private void SetExp()
    {
        slider.value = heroController.StatsController.Exp;
    }

    private void Start()
    {
        InstantiateLevelBar();
        heroController.OnLevelUp += UpdateExpStatus;
    }

    private void Update()
    {
        SetExp();
    }
}

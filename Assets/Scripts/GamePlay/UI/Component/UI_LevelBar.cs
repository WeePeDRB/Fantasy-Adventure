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
    private HeroBaseController heroController;

    // UI COMPONENTS
    [SerializeField] private Slider slider;

    //
    // FUNCTIONS
    //

    // INSTANTIATE
    private void InstantiateLevelBar()
    {
        // Take hero controller reference
        heroController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseController>();
        
        // Set value to slider
        UpdateExpStatus();
    }

    private void UpdateExpStatus()
    {
        slider.maxValue = heroController.HeroStats.ExpRequire;
        slider.value = heroController.HeroStats.Exp;
    }

    // Update health
    private void SetExp()
    {
        slider.value = heroController.HeroStats.Exp;
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

using System.Collections;
using System.Collections.Generic;
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
        slider.maxValue = heroController.HeroStats.MaxAmor;
        slider.value = heroController.HeroStats.Amor;
    }

    // Update health
    private void SetExp()
    {
        slider.value = heroController.HeroStats.Exp;
    }

    private void Start()
    {
        InstantiateLevelBar();
    }

    private void Update()
    {
    
    }
}

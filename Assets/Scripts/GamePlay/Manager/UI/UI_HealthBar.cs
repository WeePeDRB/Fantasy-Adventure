using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
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
    private void InstantiateHealthBar()
    {
        // Take hero controller reference
        heroController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseController>();
        
        // Set value to slider
        slider.maxValue = heroController.HeroStats.MaxHealth;
        slider.value = heroController.HeroStats.Health;
    }

    // Update health
    private void SetHealth()
    {
        slider.value = heroController.HeroStats.Health;
    }

    private void Start()
    {
        InstantiateHealthBar();
    }

    private void Update()
    {
        SetHealth();
    }
}

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
    private void InstantiateAmorBar()
    {
        // Take hero controller reference
        heroController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseController>();
        
        // Set value to slider
        slider.maxValue = heroController.heroStats.MaxAmor;
        slider.value = heroController.heroStats.Amor;
    }

    // Update health
    private void SetAmor()
    {
        slider.value = heroController.heroStats.Health;
    }

    private void Start()
    {
        InstantiateAmorBar();
    }

    private void Update()
    {
        SetAmor();
    }
}

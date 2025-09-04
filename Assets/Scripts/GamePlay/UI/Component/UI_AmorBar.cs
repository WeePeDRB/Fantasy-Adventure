using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_AmorBar : MonoBehaviour
{
    //
    // FIELDS
    //

    // Reference
    private HeroController heroController;

    // UI COMPONENTS
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI currentAmorText;
    [SerializeField] private TextMeshProUGUI maxAmorText;
    //
    // FUNCTIONS
    //

    // INSTANTIATE
    private void InstantiateAmorBar()
    {
        // Take hero controller reference
        heroController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroController>();

        // Set value to slider
        slider.maxValue = heroController.StatsController.MaxAmor;
        slider.value = heroController.StatsController.CurrentAmor;

        // Set value to text
        maxAmorText.text = ((int)heroController.StatsController.MaxAmor).ToString();
        currentAmorText.text = ((int)heroController.StatsController.CurrentAmor).ToString();
    }

    // Update amor
    private void SetAmor()
    {
        // Set value to slider
        slider.maxValue = heroController.StatsController.MaxAmor;
        slider.value = heroController.StatsController.CurrentAmor;

        // Set value to text
        maxAmorText.text = ((int)heroController.StatsController.MaxAmor).ToString();
        currentAmorText.text = ((int)heroController.StatsController.CurrentAmor).ToString();
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

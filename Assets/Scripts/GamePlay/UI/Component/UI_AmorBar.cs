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
    private HeroBaseController heroController;

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
        heroController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseController>();

        // Set value to slider
        slider.maxValue = heroController.HeroStats.MaxAmor;
        slider.value = heroController.HeroStats.Amor;

        // Set value to text
        maxAmorText.text = ((int)heroController.HeroStats.MaxAmor).ToString();
        currentAmorText.text = ((int)heroController.HeroStats.Amor).ToString();
    }

    // Update amor
    private void SetAmor()
    {
        // Set value to slider
        slider.maxValue = heroController.HeroStats.MaxAmor;
        slider.value = heroController.HeroStats.Amor;

        // Set value to text
        maxAmorText.text = ((int)heroController.HeroStats.MaxAmor).ToString();
        currentAmorText.text = ((int)heroController.HeroStats.Amor).ToString();
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    //
    // FIELDS
    //

    // Reference
    private HeroController heroController;

    // UI COMPONENTS
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI currentHealthText;
    [SerializeField] private TextMeshProUGUI maxHealthText;

    //
    // FUNCTIONS
    //

    // INSTANTIATE
    private void InstantiateHealthBar()
    {
        // Take hero controller reference
        heroController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroController>();

        // Set value to slider
        slider.maxValue = heroController.StatsController.MaxHealth;
        slider.value = heroController.StatsController.CurrentHealth;

        // Set value to text
        maxHealthText.text = ((int)heroController.StatsController.MaxHealth).ToString();
        currentHealthText.text = ((int)heroController.StatsController.CurrentHealth).ToString();
    }

    // Update health
    private void SetHealth()
    {
        // Set value to slider
        slider.maxValue = heroController.StatsController.MaxHealth;
        slider.value = heroController.StatsController.CurrentHealth;

        // Set value to text
        maxHealthText.text = ((int)heroController.StatsController.MaxHealth).ToString();
        currentHealthText.text = ((int)heroController.StatsController.CurrentHealth).ToString();
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

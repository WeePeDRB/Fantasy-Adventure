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
    private HeroBaseController heroController;

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
        heroController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseController>();

        // Set value to slider
        slider.maxValue = heroController.HeroStats.MaxHealth;
        slider.value = heroController.HeroStats.Health;

        // Set value to text
        maxHealthText.text = ((int)heroController.HeroStats.MaxHealth).ToString();
        currentHealthText.text = ((int)heroController.HeroStats.Health).ToString();
    }

    // Update health
    private void SetHealth()
    {
        // Set value to slider
        slider.maxValue = heroController.HeroStats.MaxHealth;
        slider.value = heroController.HeroStats.Health;

        // Set value to text
        maxHealthText.text = ((int)heroController.HeroStats.MaxHealth).ToString();
        currentHealthText.text = ((int)heroController.HeroStats.Health).ToString();
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

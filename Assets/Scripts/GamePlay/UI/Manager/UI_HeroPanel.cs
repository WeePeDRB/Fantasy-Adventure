using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HeroPanel : MonoBehaviour
{
    //
    // FIELDS
    //

    // Reference
    private HeroBaseController heroController;

    // UI components
    [SerializeField] private Image heroPortrait;
    [SerializeField] private TextMeshProUGUI heroLevel;

    //
    // FUNCTIONS
    //

    private void InstantiateHeroPanel()
    {
        heroPortrait.sprite = heroController.HeroData.heroPortrait;
        heroLevel.text = heroController.HeroStats.Level.ToString();
    }

    private void UpdateLevel()
    {
        heroLevel.text = heroController.HeroStats.Level.ToString();
    }

    private void Start()
    {
        // Instantiate
        heroController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseController>();
        InstantiateHeroPanel();

        // Event subscribe
        heroController.OnLevelUp += UpdateLevel;
    }
}

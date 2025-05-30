using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillManager : MonoBehaviour
{
    //
    // FIELDS
    //

    // Reference
    private HeroBaseController heroController;
    private SO_Hero heroData;

    // UI COMPONENTS
    [SerializeField] private List<UI_SkillComponent> skillUIList;

    //
    // FUNCTIONS
    //

    // INITIALIZE
    // Data
    private void InitializeSkillManagerData()
    {
        // Take hero controller reference
        heroController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseController>();
        heroData = heroController.HeroData;
    }
    // UI 
    private void InitializeSkillUI()
    {
        // Dash
        skillUIList[0].SetUIComponent(heroData.dashSkill);

        // Special
        skillUIList[1].SetUIComponent(heroData.specialSkill);

        // Ultimate
        skillUIList[2].SetUIComponent(heroData.ultimateSkill);
    }


    //
    private void OnHeroDash()
    {
        skillUIList[0].SkillActivate();
    }
    private void OnHeroSpecial()
    {
        skillUIList[1].SkillActivate();   
    }
    private void OnHeroUltimate()
    {
        skillUIList[2].SkillActivate();
    }

    private void Start()
    {
        // Instantiate 
        InitializeSkillManagerData();
        InitializeSkillUI();

        // Event subscribe
        heroController.OnHeroDash += OnHeroDash;
        heroController.OnHeroSpecial += OnHeroSpecial;
        heroController.OnHeroUltimate += OnHeroUltimate;
    }
}


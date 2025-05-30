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
        skillUIList[0].GetHeroSkillData(heroData.dashSkill);
        skillUIList[0].SetUIComponent();

        // Special
        skillUIList[1].GetHeroSkillData(heroData.specialSkill);
        skillUIList[1].SetUIComponent();

        // Ultimate
        skillUIList[2].GetHeroSkillData(heroData.ultimateSkill);
        skillUIList[2].SetUIComponent();
     
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


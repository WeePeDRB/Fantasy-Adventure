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

    // Hero data
    private SO_Hero heroData;

    // UI COMPONENTS
    // Dash
    [SerializeField] private Image dashSkillIcon;
    [SerializeField] private TextMeshProUGUI dashSkillCountDown;

    // Special 
    [SerializeField] private Image specialSkillIcon;
    [SerializeField] private TextMeshProUGUI specialSkillCountDown; 

    // Ultimate
    [SerializeField] private Image ultiamteSkillIcon;
    [SerializeField] private TextMeshProUGUI ultimateSkillCountDown;

    //
    private Color skillReadyColor = new Color(255,255,255);
    private Color skillCoolDownColor = new Color(166,166,166);

    //
    // FUNCTIONS
    //

    // INSTANTIATE
    // Data
    private void InstantiateSkillManagerData()
    {
        // Take hero controller reference
        heroController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseController>();

        // Take hero data
        heroData = heroController.HeroData;
    }
    // UI 
    private void InstantiateSkillUI()
    {
        // Dash
        dashSkillIcon.sprite = heroData.dashSkill.skillSprite;
        dashSkillIcon.color = skillReadyColor;
        dashSkillCountDown.enabled = false;

        // Special
        specialSkillIcon.sprite = heroData.specialSkill.skillSprite;
        specialSkillIcon.color = skillReadyColor;
        specialSkillCountDown.enabled = false;
        
        // Ultimate
        ultiamteSkillIcon.sprite = heroData.ultimateSkill.skillSprite;
        ultiamteSkillIcon.color = skillReadyColor;
        ultimateSkillCountDown.enabled = false;
    }

    // Skill activate
    private void SkillActivate(Image skillIcon, TextMeshProUGUI skillCountDown, float skillCoolDown)
    {
        skillIcon.color = skillCoolDownColor;
        skillCountDown.enabled = true;
        StartCoroutine(SkillCooldown(skillIcon, skillCountDown, skillCoolDown));
    }
    private IEnumerator SkillCooldown(Image skillIcon, TextMeshProUGUI skillCountDown, float skillCooldown)
    {
        while (skillCooldown > 0 )
        {
            skillCountDown.text = skillCooldown.ToString();
            yield return new WaitForSeconds(1f);
            skillCooldown--;
        }
        skillIcon.color = skillReadyColor;
        skillCountDown.enabled = false;
    }
    
    //
    private void OnHeroDash()
    {
        SkillActivate(dashSkillIcon, dashSkillCountDown, heroData.dashSkill.skillCooldown);
    }
    private void OnHeroSpecial()
    {
        SkillActivate(specialSkillIcon, specialSkillCountDown, heroData.specialSkill.skillCooldown);
    }
    private void OnHeroUltimate()
    {
        SkillActivate(ultiamteSkillIcon, ultimateSkillCountDown, heroData.ultimateSkill.skillCooldown);
    }

    private void Start()
    {
        // Instantiate 
        InstantiateSkillManagerData();
        InstantiateSkillUI();

        //
        heroController.OnHeroDash += OnHeroDash;
        heroController.OnHeroSpecial += OnHeroSpecial;
        heroController.OnHeroUltimate += OnHeroUltimate;
    }
}

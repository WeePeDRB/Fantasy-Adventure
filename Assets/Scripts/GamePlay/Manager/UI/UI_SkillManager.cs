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

    }
    // UI 
    private void InstantiateSkillUI()
    {
        // Dash
        dashSkillIcon.sprite = heroController.HeroData.dashSkill.skillSprite;
        dashSkillIcon.color = skillReadyColor;
        dashSkillCountDown.enabled = false;

        // Special
        specialSkillIcon.sprite = heroController.HeroData.specialSkill.skillSprite;
        specialSkillIcon.color = skillReadyColor;
        specialSkillCountDown.enabled = false;
        
        // Ultimate
        ultiamteSkillIcon.sprite = heroController.HeroData.ultimateSkill.skillSprite;
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
        SkillActivate(dashSkillIcon, dashSkillCountDown, heroController.HeroData.dashSkill.skillCooldown);
    }
    private void OnHeroSpecial()
    {
        SkillActivate(specialSkillIcon, specialSkillCountDown, heroController.HeroData.specialSkill.skillCooldown);
    }
    private void OnHeroUltimate()
    {
        SkillActivate(ultiamteSkillIcon, ultimateSkillCountDown, heroController.HeroData.ultimateSkill.skillCooldown);
    }

    private void Start()
    {
        // Instantiate 
        InstantiateSkillManagerData();
        InstantiateSkillUI();

        // Event subscribe
        heroController.OnHeroDash += OnHeroDash;
        heroController.OnHeroSpecial += OnHeroSpecial;
        heroController.OnHeroUltimate += OnHeroUltimate;
    }
}

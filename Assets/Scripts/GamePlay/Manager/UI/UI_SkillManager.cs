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
    [SerializeField] private List<SkillUI> skillUIList;

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
        skillUIList[0].skillIcon.sprite = heroData.dashSkill.skillSprite;
        skillUIList[0].skillIconCover.sprite = heroData.dashSkill.skillSprite;
        skillUIList[0].skillCoolDownCount.enabled = false;

        // Special
        skillUIList[1].skillIcon.sprite = heroData.specialSkill.skillSprite;
        skillUIList[1].skillIconCover.sprite = heroData.specialSkill.skillSprite;
        skillUIList[1].skillCoolDownCount.enabled = false;

        // Special
        skillUIList[2].skillIcon.sprite = heroData.ultimateSkill.skillSprite;
        skillUIList[2].skillIconCover.sprite = heroData.ultimateSkill.skillSprite;
        skillUIList[2].skillCoolDownCount.enabled = false;
    }

    // Skill activate
    private void SkillActivate(int number, float skillCoolDown)
    {
        skillUIList[number].skillCoolDownCount.enabled = true;
        skillUIList[number].skillIconCover.fillAmount = 1;
        StartCoroutine(SkillTextCooldown(skillUIList[number].skillCoolDownCount, skillCoolDown));
        StartCoroutine(SkillIconCooldown(skillUIList[number].skillIconCover, skillCoolDown));
    }
    private IEnumerator SkillTextCooldown(TextMeshProUGUI skillCountDown, float skillCooldown)
    {
        while (skillCooldown > 0)
        {
            skillCountDown.text = skillCooldown.ToString();
            yield return new WaitForSeconds(1f);
            skillCooldown--;
        }
        skillCountDown.enabled = false;
    }
    private IEnumerator SkillIconCooldown(Image skillIconCover, float skillCoolDown)
    {
        float elapsed = 0f;
        skillIconCover.fillAmount = 1;

        while (elapsed < skillCoolDown)
        {
            skillIconCover.fillAmount = 1 - (elapsed / skillCoolDown);
            elapsed += Time.deltaTime;
            yield return null;
        }

        skillIconCover.fillAmount = 0;
        
    }

    //
    private void OnHeroDash()
    {
        SkillActivate(0, heroData.dashSkill.skillCooldown);
    }
    private void OnHeroSpecial()
    {
        SkillActivate(1, heroData.specialSkill.skillCooldown);
    }
    private void OnHeroUltimate()
    {
        SkillActivate(2, heroData.ultimateSkill.skillCooldown);
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

[System.Serializable]
public class SkillUI
{
    public Image skillIcon;
    public Image skillIconCover;
    public TextMeshProUGUI skillCoolDownCount;
}

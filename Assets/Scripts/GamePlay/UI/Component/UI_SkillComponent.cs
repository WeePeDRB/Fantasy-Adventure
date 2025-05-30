using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillComponent : MonoBehaviour
{
    // Tooltip data
    private SO_HeroSkill heroSkillData;
    private string tooltipDataType = new string("Skill: ");

    // UI Data
    [SerializeField] private Image skillIcon;
    [SerializeField] private Image skillIconCover;
    [SerializeField] private TextMeshProUGUI skillCoolDownCount;

    public void SetUIComponent(SO_HeroSkill heroSkillData)
    {
        this.heroSkillData = heroSkillData;

        skillIcon.sprite = heroSkillData.skillSprite;
        skillIconCover.sprite = heroSkillData.skillSprite;
        skillCoolDownCount.enabled = false;
    }

    public void SkillActivate()
    {
        skillCoolDownCount.enabled = true;
        skillIconCover.fillAmount = 1;
        StartCoroutine(SkillTextCoolDown(heroSkillData.skillCooldown));
        StartCoroutine(SkillIconCoolDown(heroSkillData.skillCooldown));
    }

    private IEnumerator SkillTextCoolDown(float skillCooldown)
    {
        while (skillCooldown > 0)
        {
            skillCoolDownCount.text = skillCooldown.ToString();
            yield return new WaitForSeconds(1f);
            skillCooldown--;
        }
        skillCoolDownCount.enabled = false;
    }

    private IEnumerator SkillIconCoolDown(float skillCooldown)
    {
        float elapsed = 0f;
        skillIconCover.fillAmount = 1;

        while (elapsed < skillCooldown)
        {
            skillIconCover.fillAmount = 1 - (elapsed / skillCooldown);
            elapsed += Time.deltaTime;
            yield return null;
        }

        skillIconCover.fillAmount = 0;
    }
}

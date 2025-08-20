using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // UI Data
    private SO_HeroSkillOld heroSkillData;
    [SerializeField] private Image skillIcon;
    [SerializeField] private Image skillIconCover;
    [SerializeField] private TextMeshProUGUI skillCoolDownCount;

    // UI Component logic
    public void GetHeroSkillData(SO_HeroSkillOld heroSkillData)
    {
        if (heroSkillData == null)
        {
            Debug.Log("Hero skill data is missing");
            return;
        }
        this.heroSkillData = heroSkillData;
    }

    public void SetUIComponent()
    {
        if (heroSkillData == null)
        {
            Debug.Log("Hero skill data is missing");
            return;
        }
        skillIcon.sprite = heroSkillData.skillSprite;
        skillIconCover.sprite = heroSkillData.skillSprite;
        skillCoolDownCount.enabled = false;
    }

    public void SkillActivate()
    {
        if (heroSkillData == null)
        {
            Debug.Log("Hero skill data is missing");
            return;
        }
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

    // Tooltip logic
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (heroSkillData == null)
        {
            Debug.Log("This hero skill data is missing.");
            return;
        }
        UI_TooltipManager.Instance.ShowSKillTooltip(heroSkillData.skillSprite, heroSkillData.skillKeyword, heroSkillData.skillName, heroSkillData.skillCooldown, heroSkillData.skillDescription);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (heroSkillData == null)
        {
            Debug.Log("This hero skill data is missing.");
            return;
        }
        UI_TooltipManager.Instance.HideSkillTooltip();
    }


}

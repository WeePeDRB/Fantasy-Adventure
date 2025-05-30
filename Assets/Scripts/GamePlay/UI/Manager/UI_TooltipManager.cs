using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TooltipManager : MonoBehaviour
{
    public static UI_TooltipManager Instance;

    // Skill 
    [Space(10)]
    [Header("Skill Tooltip")]
    [SerializeField] private GameObject skillTooltip;
    [SerializeField] private SkillTooltip skillTooltipComponents;

    public void ShowSKillTooltip(Sprite icon, string keyword, string name, float cooldown, string description)
    {
        // 
        skillTooltip.SetActive(true);
        // 
        string cooldownString = new string(cooldown + "s");

        skillTooltipComponents.skillIcon.sprite = icon;
        skillTooltipComponents.skillKeyword.text = keyword;
        skillTooltipComponents.skillName.text = name;
        skillTooltipComponents.skillCoolDown.text = cooldownString;
        skillTooltipComponents.skillDescription.text = description;
    }
    public void HideSkillTooltip()
    {
        // 
        skillTooltip.SetActive(false);
    }

    // Weapon
    [Space(10)]
    [Header("Weapon Tooltip")]
    [SerializeField] private GameObject weaponTooltip;
    [SerializeField] private WeaponTooltip weaponTooltipComponents;

    public void ShowWeaponTooltip(Sprite icon, string name, int level, float damage, float attackSpeed, string description)
    {
        // 
        weaponTooltip.SetActive(true);
        //
        weaponTooltipComponents.weaponIcon.sprite = icon;
        weaponTooltipComponents.weaponName.text = name;
        weaponTooltipComponents.weaponLevel.text = level.ToString();
        weaponTooltipComponents.weaponDamage.text = damage.ToString();
        weaponTooltipComponents.weaponAttackSpeed.text = attackSpeed.ToString();
        weaponTooltipComponents.weaponDescription.text = description;
    }
    public void HideWeaponTooltip()
    {
        //
        weaponTooltip.SetActive(false);
    }

    // Blessing
    [Space(10)]
    [Header("Blessing Tooltip")]
    [SerializeField] private GameObject blessingTooltip;
    [SerializeField] private BlessingTooltip blessingTooltipComponents;

    public void ShowBlessingTooltip(Sprite icon, string name, int level, float value, string description)
    {
        // 
        blessingTooltip.SetActive(true);
        //
        blessingTooltipComponents.blessingIcon.sprite = icon;
        blessingTooltipComponents.blessingName.text = name;
        blessingTooltipComponents.blessingLevel.text = level.ToString();
        blessingTooltipComponents.blessingValue.text = value.ToString();
        blessingTooltipComponents.blessingDescription.text = description;
    }
    public void HideBlessingTooltip()
    {
        //
        blessingTooltip.SetActive(false);
    }

    // Stat
    [Space(10)]
    [Header("Stat Tooltip")]
    [SerializeField] private GameObject statTooltip;
    [SerializeField] private StatTooltip statTooltipComponents;

    public void ShowStatTooltip(string name, string description, float value)
    {
        //
        statTooltip.SetActive(true);
        //
        statTooltipComponents.statName.text = name;
        statTooltipComponents.statDescription.text = description;
        statTooltipComponents.statValue.text = value.ToString();
    }
    public void HideStatTooltip()
    {
        //
        statTooltip.SetActive(false);
    }

    // SpecialEffect
    [Space(10)]
    [Header("Special Effect Tooltip")]
    [SerializeField] private GameObject specialEffectTooltip;
    [SerializeField] private SpecialEffectTooltip specialEffectTooltipComponents;

    public void ShowSpecialEffectTooltip(string name, string description, Sprite icon)
    {
        //
        specialEffectTooltip.SetActive(true);
        //
        specialEffectTooltipComponents.specialEffectName.text = name;
        specialEffectTooltipComponents.specialEffectDescription.text = description;
        specialEffectTooltipComponents.specialEffectIcon.sprite = icon;
    }
    public void HideSpecialEffectTooltip()
    {
        specialEffectTooltip.SetActive(false);
    }

    // Tooltip
    private void HideAllTooltip()
    {
        skillTooltip.SetActive(false);
        weaponTooltip.SetActive(false);
        blessingTooltip.SetActive(false);
        statTooltip.SetActive(false);
        specialEffectTooltip.SetActive(false);
    }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        HideAllTooltip();
    }
}

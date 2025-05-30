using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SkillTooltip
{
    public Image skillIcon;
    public TextMeshProUGUI skillKeyword;
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI skillCoolDown;
    public TextMeshProUGUI skillDescription;
}

[Serializable]
public class WeaponTooltip
{
    public Image weaponIcon;
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI weaponLevel;
    public TextMeshProUGUI weaponDamage;
    public TextMeshProUGUI weaponAttackSpeed;
    public TextMeshProUGUI weaponDescription;
}

[Serializable]
public class BlessingTooltip
{
    public Image blessingIcon;
    public TextMeshProUGUI blessingName;
    public TextMeshProUGUI blessingLevel;
    public TextMeshProUGUI blessingValue;
    public TextMeshProUGUI blessingDescription;
}

[Serializable]
public class StatTooltip
{
    public TextMeshProUGUI statName;
    public TextMeshProUGUI statDescription;
    public TextMeshProUGUI statValue;
}

[Serializable]
public class SpecialEffectTooltip
{
    public Image specialEffectIcon;
    public TextMeshProUGUI specialEffectName;
    public TextMeshProUGUI specialEffectDescription;
}
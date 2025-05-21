using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradeData
{
    public string id;
    public UpgradeType upgradeType;
    public Sprite upgradeSprite;
    public string upgradeName;
    public string upgradeDescription;
    public SO_Weapon weaponData;
    public SO_Blessing blessingData;
}

public class WeaponData
{
    public string id;
    public GameObject weaponPrefab;
    public Sprite weaponSprite;
    public string weaponName;
    public string weaponDescription;
    public int weaponLevel;
    public float weaponAttackSpeed;
    public float weaponDamage;
}

public class BlessingData
{
    public string id;
    public Sprite blessingSprite;
    public string blessingName;
    public string blessingDescription;
    public int blessingLevel;
    public float blessingValue;
}
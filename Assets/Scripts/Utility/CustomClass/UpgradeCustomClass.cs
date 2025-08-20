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

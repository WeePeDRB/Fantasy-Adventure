using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradeFactory
{
    // Create weapon
    private static readonly Dictionary<string, Func<WeaponBaseOld>> weaponDict = new()
    {
        {"00", () => new ShieldController()},
        {"01", () => new ArrowController()},
        {"02", () => new SwordController()}
    };
    public static WeaponBaseOld CreateWeapon(SO_Weapon weaponData)
    {
        string suffix = weaponData.id.Substring(2, 2);
        return weaponDict.TryGetValue(suffix, out var createFunc) ? createFunc() : null;
    }

    // Create blessing
    private static readonly Dictionary<string, Func<SO_Blessing, BlessingBaseOld>> blessingDict = new()
    {
        {"00", (blessingData) => new DamageAmplifier(blessingData)},
        {"01", (blessingData) => new IncreaseHealth(blessingData)},
        {"02", (blessingData) => new IncreaseResistance(blessingData)}
    };
    public static BlessingBaseOld CreateBlessing(SO_Blessing blessingData)
    {
        string suffix = blessingData.id.Substring(2, 2);
        return blessingDict.TryGetValue(suffix, out var createFunc) ? createFunc(blessingData) : null;
    }

}

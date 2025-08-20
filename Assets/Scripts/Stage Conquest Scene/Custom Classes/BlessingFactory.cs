using System;
using System.Collections.Generic;

public static class BlessingFactory
{
    private static readonly Dictionary<string, Func<SO_Blessing, BlessingBase>> blessingDict = new()
    {
        {"00", (blessingdata) => new BlessingHealth(blessingdata)},
        {"01", (blessingdata) => new BlessingAmor(blessingdata)},
        {"02", (blessingdata) => new BlessingSpeed(blessingdata)},
        {"03", (blessingdata) => new BlessingResistance(blessingdata)},
        {"04", (blessingdata) => new BlessingDamageAmplifier(blessingdata)},
        {"05", (blessingdata) => new BlessingCooldown(blessingdata)},
        {"06", (blessingdata) => new BlessingAttackSpeed(blessingdata)},
        {"07", (blessingdata) => new BlessingCritChance(blessingdata)}
    };

    public static BlessingBase CreateBlessing(SO_Blessing blessingData)
    {
        string suffix = blessingData.id.Substring(2, 2);
        return blessingDict.TryGetValue(suffix, out var func) ? func(blessingData) : null;
    }
}

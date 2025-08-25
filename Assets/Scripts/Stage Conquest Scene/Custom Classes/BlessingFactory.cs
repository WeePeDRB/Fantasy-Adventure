using System;
using System.Collections.Generic;

public static class BlessingFactory
{
    private static readonly Dictionary<string, Func<SO_Blessing, BlessingBase>> blessingDict = new()
    {
        {"00", (blessingdata) => new Bl_Health(blessingdata)},
        {"01", (blessingdata) => new Bl_Amor(blessingdata)},
        {"02", (blessingdata) => new Bl_Speed(blessingdata)},
        {"03", (blessingdata) => new Bl_Resistance(blessingdata)},
        {"04", (blessingdata) => new Bl_DamageAmplifier(blessingdata)},
        {"05", (blessingdata) => new Bl_Cooldown(blessingdata)},
        {"06", (blessingdata) => new Bl_AttackSpeed(blessingdata)},
        {"07", (blessingdata) => new Bl_CritChance(blessingdata)}
    };

    public static BlessingBase CreateBlessing(SO_Blessing blessingData)
    {
        string suffix = blessingData.id.Substring(2, 2);
        return blessingDict.TryGetValue(suffix, out var func) ? func(blessingData) : null;
    }
}


using UnityEngine;

public class IncreaseHealth : BlessingBase
{
    public IncreaseHealth()
    {

    }

    public IncreaseHealth(SO_Blessing blessingData)
    {
        id = blessingData.id;
        blessingName = blessingData.blessingName;
        blessingLevel = blessingData.blessingLevel;
        blessingValue = blessingData.blessingValue;
    }

    //
    // FUNCTIONS
    //

    public override void ApplyBlessingOnHero(HeroBaseController hero)
    {
        hero.HeroStats.MaxHealth += blessingValue;
        hero.HeroStats.Health += blessingValue;
    }
}
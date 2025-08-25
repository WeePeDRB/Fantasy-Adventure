using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bl_Health : BlessingBase
{
    // Initialize data
    public Bl_Health(SO_Blessing blessingData)
    {
        id = blessingData.id;
        blessingName = blessingData.blessingName;
        blessingDescription = blessingData.blessingDescription;
        blessingLevel = blessingData.blessingLevel;
        blessingValue = blessingData.blessingValue;
        blessingSprite = blessingData.blessingSprite;
    }

    // Apply blessing effect on hero stat
    public override void ApplyBlessingOnHero(HeroController heroController)
    {
        // 
        heroController.StatsController.MaxHealth += blessingValue;
        heroController.StatsController.CurrentHealth += blessingValue;
    }

}

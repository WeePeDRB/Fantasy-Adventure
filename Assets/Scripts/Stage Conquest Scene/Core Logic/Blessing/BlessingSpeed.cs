using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessingSpeed : BlessingBase
{
    public BlessingSpeed(SO_Blessing blessingData)
    {
        id = blessingData.id;
        blessingName = blessingData.blessingName;
        blessingDescription = blessingData.blessingDescription;
        blessingLevel = blessingData.blessingLevel;
        blessingValue = blessingData.blessingValue;
        blessingSprite = blessingData.blessingSprite;
    }

    public override void ApplyBlessingOnHero(HeroController heroController)
    {
        throw new System.NotImplementedException();
    }
}

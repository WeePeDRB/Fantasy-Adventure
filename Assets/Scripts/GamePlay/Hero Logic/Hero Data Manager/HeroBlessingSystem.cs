using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HeroBlessingSystem : CharacterBlessingSystem
{
    //
    // FIELDS
    //
    private List<SO_Blessing> blessingList;

    //
    // CONSTRUCTOR
    //
    public HeroBlessingSystem()
    {
        activeBlessings = new Dictionary<string, BlessingBaseOld>();
        blessingList = new List<SO_Blessing>();
    }

    //
    // FUNCTIONS
    //

    // Dictionary logic
    public void ReceiveBlessing(SO_Blessing blessingData, BlessingBaseOld blessing, HeroBaseController hero)
    {
        blessing.ApplyBlessingOnHero(hero);
        activeBlessings.Add(blessingData.id, blessing);
        blessingList.Add(blessingData);
    }

    public void BlessingLevelUp(SO_Blessing blessingData, HeroBaseController hero)
    {
        BlessingBaseOld blessing = GetBlessing(blessingData);
        blessing.BlessingLevelUp(hero);
    }

    // Blessing dictionary check
    public bool IsBlessingExist(SO_Blessing blessingData)
    {
        if (activeBlessings.ContainsKey(blessingData.id))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsBlessingQuantityMax()
    {
        if (blessingList.Count == 2) return true;
        return false;
    }

    // Get data
    public BlessingBaseOld GetBlessing(SO_Blessing blessingData)
    {
        return activeBlessings[blessingData.id];
    }
    public List<SO_Blessing> GetBLessingList()
    {
        return blessingList;
    }
}

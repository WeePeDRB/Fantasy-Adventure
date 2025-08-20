using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBlessingController
{
    // Dictionary containing blessing with the key as the ID and the value as the blessing.
    private Dictionary<string, BlessingBase> activeBlessings;

    // Number of blessings currently owned
    private List<BlessingBase> blessingList;

    // Maximum number of blessings that can be owned
    private int maxBlessing;

    // Receive blessing events
    public event Action<Blessing> OnReceiveBlessing;
    public event Action<Blessing> OnBlessingReachMaxLevel;
    public event Action<BlessingList> OnBlessingListFull;

    // Initialize data
    public HeroBlessingController()
    {
        activeBlessings = new Dictionary<string, BlessingBase>();
        blessingList = new List<BlessingBase>();
        maxBlessing = 5;
    }

    // Receive blessing
    public void ReceiveBlessing(SO_Blessing blessingData, HeroController heroController)
    {
        // Check if blessing exist in the dictionary
        if (IsBlessingExist(blessingData))
        {
            // Get blessing
            BlessingBase blessing = GetBlessing(blessingData);

            // Blessing level up
            blessing.BlessingLevelUp(heroController);

            // Check if blessing reach max level
            if (blessing.IsMaxLevel()) OnBlessingReachMaxLevel?.Invoke(new Blessing { blessing = blessing });

        }
        else
        {

            BlessingBase blessing = BlessingFactory.CreateBlessing(blessingData);
            activeBlessings.Add(blessingData.id, blessing);
            blessingList.Add(blessing);

            // Check if blessing dictionary is full
            if (blessingList.Count >= maxBlessing)
            {
                OnBlessingListFull?.Invoke(new BlessingList { blessingList = blessingList });
            }
        }

        // Invoke the blessing receive event to synchronize the logic
        OnReceiveBlessing?.Invoke(new Blessing { });
    }

    // Check if blessing exist in dictionary
    private bool IsBlessingExist(SO_Blessing blessingData)
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

    // Get blessing
    private BlessingBase GetBlessing(SO_Blessing blessingData)
    {
        return activeBlessings[blessingData.id];
    }
}

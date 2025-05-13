using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    //
    // FIELDS
    //
    public static UpgradeManager Instance;

    // Reference
    private List<HeroBaseController> heroList;
    private HeroBaseController heroBaseController;

    public List<SO_Upgrade> upgradeData;
    private List<SO_Upgrade> randomUpgrade;

    // Event
    public event EventHandler<OnRandomUpgradeEventArgs> OnRandomUpgrade;

    // Custom class
    public class OnRandomUpgradeEventArgs : EventArgs
    {
        public List<SO_Upgrade> randomUpgradeList;
    }

    //
    // FUNCTIONS
    //

    //
    private int[] RandomNumbers()
    {
        int[] randomNumbers = new int[3];
        int count = 0;

        while (count < 3)
        {
            int randomNum = UnityEngine.Random.Range(0, upgradeData.Count);
            bool isDuplicate = false;
            for (int i = 0; i < count; i++)
            {
                if (randomNumbers[i] == randomNum)
                {
                    isDuplicate = true;
                    break;
                }
            }

            if (!isDuplicate)
            {
                randomNumbers[count] = randomNum;
                count++;
            }
        }

        return randomNumbers;
        
    }
    private void GetUpgrade()
    {
        int[] randomNumbers = RandomNumbers();

        for (int i = 0; i < 3; i++)
        {
            randomUpgrade.Add(upgradeData[randomNumbers[i]]);
        }
        OnRandomUpgrade?.Invoke(this, new OnRandomUpgradeEventArgs{ randomUpgradeList = randomUpgrade});
        randomUpgrade = new List<SO_Upgrade>();
    }

    //
    public void GetUpgradeForHero()
    {
        
    }

    //
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        randomUpgrade = new List<SO_Upgrade>();
        heroList = GameUtility.InitializeHeroList();
        for (int i = 0 ; i < heroList.Count; i ++) 
        {
            heroBaseController = heroList[i];
            heroBaseController.OnLevelUp += GetUpgrade;
        }
    }
}

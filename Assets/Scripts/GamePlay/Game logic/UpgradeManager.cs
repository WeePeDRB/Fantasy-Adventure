using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeManager : MonoBehaviour
{
    //
    // FIELDS
    //
    public static UpgradeManager Instance;
    private List<HeroBaseController> heroList; // Get all hero instance that exist in the map
    public List<SO_Upgrade> upgradeDataSO; // Upgrade data 
    private List<UpgradeData> upgradeData;
    public event EventHandler<OnRandomUpgradeEventArgs> OnRandomUpgrade;
    public event EventHandler<WeaponDataEventArgs> OnSelectWeapon;
    public event EventHandler<BlessingDataEventArgs> OnSelectBlessing;
    public event EventHandler<string> OnReceiveUpgrade;

    //
    // FUNCTIONSs
    //

    // Create a copy of the upgrade data
    private void CreateUpgradeData()
    {
        upgradeData = new List<UpgradeData>();

        foreach (SO_Upgrade so_Upgrade in upgradeDataSO)
        {
            UpgradeData upgrade = new UpgradeData
            {
                id = so_Upgrade.id,
                upgradeType = so_Upgrade.upgradeType,
                upgradeSprite = so_Upgrade.upgradeSprite,
                upgradeName = so_Upgrade.upgradeName,
                upgradeDescription = so_Upgrade.upgradeDescription,
                weaponData = so_Upgrade.weaponData,
                blessingData = so_Upgrade.blessingData
            };
            upgradeData.Add(upgrade);
        }
    }

    // Hero level up event 
    private int GetUpgradeQuantity()
    {
        if (upgradeData.Count >= 3) return 3;
        else return upgradeData.Count;
    }

    //
    private List<UpgradeData> GetRamdomUpgrade(int count)
    {
        List<UpgradeData> copy = new List<UpgradeData>(upgradeData);
        List<UpgradeData> result = new List<UpgradeData>();

        while (result.Count < count && copy.Count > 0)
        {
            int index = Random.Range(0, copy.Count);
            result.Add(copy[index]);
            copy.RemoveAt(index);
        }
        return result;
    }

    private void HandleHeroLevelUp()
    {
        List<UpgradeData> randomUpgradeList = GetRamdomUpgrade(GetUpgradeQuantity());
        OnRandomUpgrade?.Invoke(this, new OnRandomUpgradeEventArgs { randomUpgradeList = randomUpgradeList });
        GameUtility.PauseGame();
    }

    public void ReceiveSelectedUpgrade(UpgradeData upgradeData)
    {
        switch (upgradeData.upgradeType)
        {
            case UpgradeType.Weapon:
                OnSelectWeapon?.Invoke(this, new WeaponDataEventArgs { weaponData = upgradeData.weaponData });
                break;
            case UpgradeType.Blessing:
                OnSelectBlessing?.Invoke(this, new BlessingDataEventArgs { blessingData = upgradeData.blessingData });
                break;
        }
        OnReceiveUpgrade?.Invoke(this, new string(upgradeData.id));
    }

    // Weapon related events
    private void HandleWeaponMaxLevel(object sender, WeaponDataEventArgs weaponDataEventArgs)
    {
        //
        SO_Weapon weaponData = weaponDataEventArgs.weaponData;

        for (int i = upgradeData.Count-1; i >= 0; i--)
        {
            if (upgradeData[i].upgradeType != UpgradeType.Weapon) continue;
            else
            {                
                if (upgradeData[i].id == weaponData.id)
                {
                    upgradeData.Remove(upgradeData[i]);
                    break;
                }
            }
        }
    }
    private void HandleWeaponListFull(object sender, WeaponListEventArgs weaponListEventArgs)
    {
        //
        List<SO_Weapon> weaponList = weaponListEventArgs.weaponDataList;
        int maxR = upgradeData.Count;
        for (int i = maxR-1, y = 0; i >= 0; i--)
        {
            if (y < weaponList.Count)
            {
                if (upgradeData[i].upgradeType != UpgradeType.Weapon) continue;
                else
                {
                    upgradeData[i].id = weaponList[y].id;
                    //
                    upgradeData[i].weaponData = weaponList[y];
                    //
                    upgradeData[i].upgradeSprite = weaponList[y].weaponSprite;
                    upgradeData[i].upgradeName = weaponList[y].weaponName;
                    upgradeData[i].upgradeDescription = weaponList[y].weaponDescription;
                    y++;
                }
            }
            else
            {
                if (upgradeData[i].upgradeType != UpgradeType.Weapon) continue;
                else
                {
                    upgradeData.Remove(upgradeData[i]);
                }
            }
        }
    }

    // Blessing related events
    private void HandleBlessingMaxLevel(object sender, BlessingDataEventArgs blessingDataEventArgs)
    {
        //
        SO_Blessing blessingData = blessingDataEventArgs.blessingData;

        for (int i = upgradeData.Count-1; i >= 0; i--)
        {
            if (upgradeData[i].upgradeType != UpgradeType.Blessing) continue;
            else
            {                
                if (upgradeData[i].id == blessingData.id)
                {
                    upgradeData.Remove(upgradeData[i]);
                    break;
                }
            }
        }
    }  
    private void HandleBlessingListFull(object sender, BlessingListEventArgs blessingListEventArgs)
    {
        //
        List<SO_Blessing> blessingList = blessingListEventArgs.blessingDataList;
        int maxR = upgradeData.Count;
        for (int i = maxR-1, y = 0; i >= 0; i--)
        {
            if (y < blessingList.Count)
            {
                if (upgradeData[i].upgradeType != UpgradeType.Blessing) continue;
                else
                {
                    upgradeData[i].id = blessingList[y].id;
                    //
                    upgradeData[i].blessingData = blessingList[y];
                    //
                    upgradeData[i].upgradeSprite = blessingList[y].blessingSprite;
                    upgradeData[i].upgradeName = blessingList[y].blessingName;
                    upgradeData[i].upgradeDescription = blessingList[y].blessingDescription;
                    y++;
                }
            }
            else
            {
                if (upgradeData[i].upgradeType != UpgradeType.Blessing) continue;
                else
                {
                    upgradeData.Remove(upgradeData[i]);
                }
            }
        }   
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        CreateUpgradeData();
        HeroBaseController heroBaseController;
        heroList = GameUtility.InitializeHeroList();
        for (int i = 0; i < heroList.Count; i++)
        {
            heroBaseController = heroList[i];
            heroBaseController.OnLevelUp += HandleHeroLevelUp;
            // Weapon
            heroBaseController.OnWeaponListFull += HandleWeaponListFull;
            heroBaseController.OnWeaponMaxLevel += HandleWeaponMaxLevel;
            // Blessing
            heroBaseController.OnBlessingListFull += HandleBlessingListFull;
            heroBaseController.OnBlessingMaxLevel += HandleBlessingMaxLevel;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpgradePanelManager : MonoBehaviour
{
    //
    // FIELDS
    //

    //
    private List<UpgradeData> upgradeData;

    // Upgrade UI component
    [SerializeField] private GameObject upgradePanel;

    [SerializeField] private List<UpgradeUI> upgradeUIList;

    //
    // FUNCTIONS
    //

    // Get/Set data for upgrade panel
    private void GetUpgradeData(object sender, OnRandomUpgradeEventArgs onRandomUpgradeEventArgs)
    {
        upgradeData = onRandomUpgradeEventArgs.randomUpgradeList;
        SetUpgradeData();
    }
    private void SetUpgradeData()
    {
        upgradePanel.SetActive(true);

        for (int i = 0; i < upgradeData.Count; i++)
        {
            upgradeUIList[i].upgradeGameObj.SetActive(true);
            upgradeUIList[i].upgradeDescription.text = upgradeData[i].upgradeDescription;
            upgradeUIList[i].upgradeName.text = upgradeData[i].upgradeName;
            upgradeUIList[i].upgradeImage.sprite = upgradeData[i].upgradeSprite;
        }
    }

    //
    public void OnButtonClick(int number)
    {
        for (int i = 0; i < 3; i++)
        {
            if (upgradeUIList[i].upgradeGameObj.active)
            {
                upgradeUIList[i].upgradeGameObj.SetActive(false);
            }
        }
        UpgradeManager.Instance.ReceiveSelectedUpgrade(upgradeData[number]);
        upgradePanel.SetActive(false);
    }

    //
    private void Start()
    {
        UpgradeManager.Instance.OnRandomUpgrade += GetUpgradeData;
    }
}

[System.Serializable]
public class UpgradeUI
{
    public GameObject upgradeGameObj;
    public Image upgradeImage;
    public TextMeshProUGUI upgradeName;
    public TextMeshProUGUI upgradeDescription;
}

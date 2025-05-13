using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpgradePanel : MonoBehaviour
{
    //
    // FIELDS
    //

    //
    private List<SO_Upgrade> upgradeData;

    // Upgrade UI component
    [SerializeField] private GameObject upgradePanel;
    // Upgrade 1
    [SerializeField] private TextMeshProUGUI upgradeDescription1;
    [SerializeField] private TextMeshProUGUI upgradeName1;
    [SerializeField] private Image upgradeImage1;
    // Upgrade 2
    [SerializeField] private TextMeshProUGUI upgradeDescription2;
    [SerializeField] private TextMeshProUGUI upgradeName2;
    [SerializeField] private Image upgradeImage2;
    // Upgrade 3
    [SerializeField] private TextMeshProUGUI upgradeDescription3;
    [SerializeField] private TextMeshProUGUI upgradeName3;
    [SerializeField] private Image upgradeImage3;

    //
    // FUNCTIONS
    //

    // Get/Set data for upgrade panel
    private void GetUpgradeData(object sender, UpgradeManager.OnRandomUpgradeEventArgs onRandomUpgradeEventArgs)
    {
        upgradeData = onRandomUpgradeEventArgs.randomUpgradeList;
        SetUpgradeData();
    }
    private void SetUpgradeData()
    {
        upgradePanel.SetActive(true);
        // Upgrade 1
        upgradeDescription1.text = upgradeData[0].upgradeDescription;
        upgradeName1.text = upgradeData[0].upgradeName;
        upgradeImage1.sprite = upgradeData[0].upgradeSprite;
        // Upgrade 2
        upgradeDescription2.text = upgradeData[1].upgradeDescription;
        upgradeName2.text = upgradeData[1].upgradeName;
        upgradeImage2.sprite = upgradeData[1].upgradeSprite;
        // Upgrade 3
        upgradeDescription3.text = upgradeData[2].upgradeDescription;
        upgradeName3.text = upgradeData[2].upgradeName;
        upgradeImage3.sprite = upgradeData[2].upgradeSprite;
    }

    //
    public void OnButtonClick(int number)
    {
        
        upgradePanel.SetActive(false);
    }

    //
    private void Start()
    {
        UpgradeManager.Instance.OnRandomUpgrade += GetUpgradeData;
    }
}

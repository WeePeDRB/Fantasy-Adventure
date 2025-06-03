using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_UpgradePanelManager : MonoBehaviour
{
    //
    // FIELDS
    //

    //
    private List<UpgradeData> upgradeData;

    // Upgrade UI component
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private Image upgradePanelBackGround;

    [SerializeField] private List<UI_UpgradeComponent> upgradeUIList;

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

        // Tweening
        upgradePanelBackGround.DOFade(0.7f, 0.3f).SetUpdate(true);



        for (int i = 0; i < upgradeData.Count; i++)
        {
            upgradeUIList[i].GetUpgradeData(upgradeData[i]);
            upgradeUIList[i].SetUIComponent();
        }
    }

    //
    public void ReceiveUpgrade(object sender, string upgradeID)
    {
        for (int i = 0; i < 3; i++)
        {
            if (upgradeUIList[i].UpgradeData.id != upgradeID)
            {
                upgradeUIList[i].UpgradeBlur();
            }
        }

        StartCoroutine(DisableComponentCoroutine());
    }
    public void DisableUpgradeComponent()
    {
        for (int i = 0; i < 3; i++)
        {
            upgradeUIList[i].DisableUIComponent();
            upgradeUIList[i].UpgradeClarify();
        }
        upgradePanelBackGround.DOFade(0f, 0.3f).SetUpdate(true);
        StartCoroutine(DisablePanelCoroutine());
        StartCoroutine(UnpauseCoroutine());
    }

    // Support functions
    private IEnumerator DisableComponentCoroutine()
    {
        yield return new WaitForSecondsRealtime(1f);
        DisableUpgradeComponent();
    }
    private IEnumerator DisablePanelCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        upgradePanel.SetActive(false);
    }
    private IEnumerator UnpauseCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        GameUtility.UnpauseGame();
    }

    //
    private void Start()
    {
        UpgradeManager.Instance.OnRandomUpgrade += GetUpgradeData;
        UpgradeManager.Instance.OnReceiveUpgrade += ReceiveUpgrade;
    }
}

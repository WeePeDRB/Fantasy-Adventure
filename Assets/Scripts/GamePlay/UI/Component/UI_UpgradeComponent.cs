using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;


public class UI_UpgradeComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // UI Data
    private UpgradeData upgradeData;
    [SerializeField] private GameObject upgradeGameObj;
    [SerializeField] private CanvasGroup upgradeCanvasGrp;
    [SerializeField] private Image upgradeImage;
    [SerializeField] private TextMeshProUGUI upgradeName;
    [SerializeField] private TextMeshProUGUI upgradeDescription;

    public event EventHandler<UpgradeData> OnHoverEnter;
    public event Action OnHoverExit;

    public UpgradeData UpgradeData
    {
        get { return upgradeData; }
    }

    public void GetUpgradeData(UpgradeData upgradeData)
    {
        if (upgradeData == null)
        {
            Debug.LogError("Upgrade data is missing !");
            return;
        }
        this.upgradeData = upgradeData;
    }

    public void SetUIComponent()
    {
        if (upgradeData == null)
        {
            Debug.LogError("Upgrade data is missing !");
            return;
        }
        upgradeGameObj.SetActive(true);
        upgradeImage.sprite = upgradeData.upgradeSprite;
        upgradeName.text = upgradeData.upgradeName;
        upgradeDescription.text = upgradeData.upgradeDescription;
    }

    public void OnBtnClick()
    {
        UpgradeController.Instance.ReceiveSelectedUpgrade(upgradeData);
    }

    public void DisableUIComponent()
    {
        upgradeGameObj.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIComponentScaleUp();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIComponentScaleDown();
    }

    public void UpgradeBlur()
    {
        UIComponentBlur();
    }

    public void UpgradeClarify()
    {
        UIComponetClarify();
    }

    // Support functions
    private void UIComponentScaleUp()
    {
        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f).SetUpdate(true);
    }
    private void UIComponentScaleDown()
    {
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f).SetUpdate(true);
    }

    private void UIComponentBlur()
    {
        upgradeCanvasGrp.DOFade(0.2f, 0.4f).SetUpdate(true);
    }
    private void UIComponetClarify()
    {
        upgradeCanvasGrp.DOFade(1f, 0.1f).SetUpdate(true);
    }
}

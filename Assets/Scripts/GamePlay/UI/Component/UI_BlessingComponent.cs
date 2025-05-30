using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BlessingComponent : MonoBehaviour
{
    // Tooltip data
    private SO_Blessing blessingData;
    private string toolTipDataType = new string("Blessing :");

    // UI Data
    [SerializeField] private string blessingId;
    [SerializeField] private Image blessingIcon;
    [SerializeField] private TextMeshProUGUI blessingLevel;

    public string BlessingID
    {
        get { return blessingId; }
    }

    public void SetUIComponent(SO_Blessing blessingData)
    {
        this.blessingData = blessingData;

        blessingId = blessingData.id;
        blessingIcon.sprite = blessingData.blessingSprite;
        blessingLevel.text = blessingData.blessingLevel.ToString();
    }

    public void UpdataUIComponent(BlessingBase blessing)
    {
        blessingLevel.text = blessing.BlessingLevel.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_BlessingComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // UI Data
    private BlessingBase blessing;
    [SerializeField] private string blessingId;
    [SerializeField] private Image blessingIcon;
    [SerializeField] private TextMeshProUGUI blessingLevel;

    public string BlessingID
    {
        get { return blessingId; }
    }

    // UI Component logic
    public void GetBlessing(BlessingEventArgs blessingEventArgs)
    {
        if (blessingEventArgs.blessing == null)
        {
            Debug.Log("Blessing data is missing.");
            return;
        }
        blessing = blessingEventArgs.blessing;
    }

    public void SetUIComponent()
    {
        if (blessing == null)
        {
            Debug.Log("Blessing data is missing.");
            return;
        }
        blessingId = blessing.ID;
        blessingIcon.sprite = blessing.BlessingSprite;
        blessingLevel.text = blessing.BlessingLevel.ToString();
    }

    public void UpdataUIComponent()
    {
        if (blessing == null)
        {
            Debug.Log("Blessing data is missing.");
            return;
        }
        blessingLevel.text = blessing.BlessingLevel.ToString();
    }

    // Tooltip logic
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (blessing == null)
        {
            Debug.Log("This blessing data is missing.");
            return;
        }
        float bonus = blessing.BlessingLevel * blessing.BlessingValue;
        UI_TooltipManager.Instance.ShowBlessingTooltip(blessing.BlessingSprite, blessing.BlessingName, blessing.BlessingLevel, bonus, blessing.BlessingDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (blessing == null)
        {
            Debug.Log("This blessing data is missing.");
            return;
        }
        UI_TooltipManager.Instance.HideBlessingTooltip();
    }
}

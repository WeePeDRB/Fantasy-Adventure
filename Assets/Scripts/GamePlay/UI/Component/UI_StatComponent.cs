using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_StatComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Tooltip data
    [SerializeField] private string heroStatName;
    [SerializeField] private string heroStatDescription;
    private float heroStatValue;

    // UI data

    [SerializeField] private Sprite statSprite;
    [SerializeField] private Image statIcon;
    [SerializeField] private TextMeshProUGUI statValue;


    public void SetUIComponent()
    {
        statIcon.sprite = statSprite;
    }

    public void UpdateComponentValue(float value)
    {
        heroStatValue = value;
        statValue.text = value.ToString();
    }

    // Tooltip
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (heroStatName == null || heroStatDescription == null)
        {
            Debug.LogError("Stat data is missing.");
            return;
        }
        UI_TooltipManager.Instance.ShowStatTooltip(heroStatName, heroStatDescription, heroStatValue);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI_TooltipManager.Instance.HideStatTooltip();
    }
}


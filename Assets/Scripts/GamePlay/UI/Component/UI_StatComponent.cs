using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatComponent : MonoBehaviour
{
    // Tooltip data
    [SerializeField] private string heroStatName;
    [SerializeField] private string heroStatDescription;


    // UI data
    private HeroStats herostats;
    [SerializeField] private Sprite statSprite;
    [SerializeField] private Image statIcon;
    [SerializeField] private TextMeshProUGUI statValue;

    public void SetUIComponent()
    {
        statIcon.sprite = statSprite;
    }

    public void UpdateComponentValue(float value)
    {
        statValue.text = value.ToString();
    }
}


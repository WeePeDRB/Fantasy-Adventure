using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_WeaponComponent : MonoBehaviour
{
    // Tooltip data
    private SO_Weapon weaponData;
    private string toolTipDataType = new string("Weapon :");

    // UI Data
    [SerializeField] private string weaponId;
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TextMeshProUGUI weaponLevel;

    public string WeaponID
    {
        get { return weaponId; }
    }

    public void SetUIComponent(SO_Weapon weaponData)
    {
        this.weaponData = weaponData;

        weaponId = weaponData.id;
        weaponIcon.sprite = weaponData.weaponSprite;
        weaponLevel.text = weaponData.weaponLevel.ToString();
    }

    public void UpdataUIComponent(WeaponBase weapon)
    {
        weaponLevel.text = weapon.WeaponLevel.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_WeaponComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // UI Data
    private WeaponBaseOld weapon;
    [SerializeField] private string weaponId;
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TextMeshProUGUI weaponLevel;

    public string WeaponID
    {
        get { return weaponId; }
    }

    // UI Component logic
    public void GetWeapon(WeaponEventArgs weaponEventArgs)
    {
        if (weaponEventArgs.weapon == null)
        {
            Debug.Log("Weapon data is missing");
            return;
        }
        weapon = weaponEventArgs.weapon;
    }

    public void SetUIComponent()
    {
        if (weapon == null )
        {
            Debug.Log("Weapon data is missing");
            return;
        }
        weaponId = weapon.ID;
        weaponIcon.gameObject.SetActive(true);
        weaponIcon.sprite = weapon.WeaponSprite;
        weaponLevel.text = weapon.WeaponLevel.ToString();
    }

    public void UpdataUIComponent()
    {
        if (weapon == null)
        {
            Debug.Log("Weapon data is missing");
            return;
        }
        weaponLevel.text = weapon.WeaponLevel.ToString();
    }

    // Tooltip logic
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (weapon == null)
        {
            Debug.Log("This weapon data is missing.");
            return;
        }
        UI_TooltipManager.Instance.ShowWeaponTooltip(weapon.WeaponSprite, weapon.WeaponName, weapon.WeaponLevel, weapon.WeaponAttackDamage, weapon.WeaponAttackSpeed, weapon.WeaponDescription);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (weapon == null)
        {
            Debug.Log("This weapon data is missing.");
            return;
        }
        UI_TooltipManager.Instance.HideWeaponTooltip();
    }
}

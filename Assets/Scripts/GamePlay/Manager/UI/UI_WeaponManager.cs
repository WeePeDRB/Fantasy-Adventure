using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_WeaponManager : MonoBehaviour
{
    //
    // FIELDS
    //

    // Reference 
    private HeroBaseController heroController;

    // UI COMPONENTS
    [SerializeField] private List<WeaponUI> weaponUIList;

    //
    // FUNCTIONS
    //

    // INITIALIZE
    private void InitializeWeaponManager()
    {
        // Take hero controller reference
        heroController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseController>();
    }

    private void SetWeaponUI(object sender, WeaponEventArgs weaponEventArgs)
    {
        foreach (WeaponUI weaponUI in weaponUIList)
        {
            // If weapon UI is empty add data to its
            if (!string.IsNullOrEmpty(weaponUI.weaponId))
            {
                if (weaponUI.weaponId == weaponEventArgs.weaponData.id)
                {
                    weaponUI.weaponLevel.text = weaponEventArgs.weapon.WeaponLevel.ToString();
                    break;
                }
            }
            // If weapon already exist in UI
            else
            {
                weaponUI.weaponId = weaponEventArgs.weaponData.id;
                weaponUI.weaponIcon.sprite = weaponEventArgs.weaponData.weaponSprite;
                weaponUI.weaponLevel.text = weaponEventArgs.weaponData.weaponLevel.ToString();
                break;
            }
        }
    }

    //
    private void Start()
    {
        InitializeWeaponManager();
        heroController.OnReceiveWeapon += SetWeaponUI;
    }
}

[System.Serializable]
public class WeaponUI
{
    public string weaponId;
    public Image weaponIcon;
    public TextMeshProUGUI weaponLevel;
}

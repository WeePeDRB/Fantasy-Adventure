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
    [SerializeField] private List<UI_WeaponComponent> weaponUIList;

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
        foreach (UI_WeaponComponent weaponUI in weaponUIList)
        {
            if (!string.IsNullOrEmpty(weaponUI.WeaponID))
            {
                if (weaponUI.WeaponID == weaponEventArgs.weaponData.id)
                {
                    weaponUI.UpdataUIComponent(weaponEventArgs.weapon);
                    break;
                }
            }
            else
            {
                weaponUI.SetUIComponent(weaponEventArgs.weaponData);
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


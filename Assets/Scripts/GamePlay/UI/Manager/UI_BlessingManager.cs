using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BlessingManager : MonoBehaviour
{
    //
    // FIELDS
    //

    // Reference
    private HeroBaseController heroBaseController;

    // UI Components
    [SerializeField] private List<UI_BlessingComponent> blessingUIList;

    //
    // FUNCTIONS
    //

    private void InitializeBlessingManager()
    {
        heroBaseController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseController>();
    }

    private void SetBlessingUI(object sender, BlessingEventArgs blessingEventArgs)
    {
        foreach (UI_BlessingComponent blessingUI in blessingUIList)
        {
            if (!string.IsNullOrEmpty(blessingUI.BlessingID))
            {
                if (blessingUI.BlessingID == blessingEventArgs.blessingData.id)
                {
                    blessingUI.UpdataUIComponent(blessingEventArgs.blessing);
                    break;
                }
            }
            else
            {
                blessingUI.SetUIComponent(blessingEventArgs.blessingData);
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeBlessingManager();
        heroBaseController.OnReceiveBlessing += SetBlessingUI;
    }
}


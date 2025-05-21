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
    [SerializeField] private List<BlessingUI> blessingUIList;

    //
    // FUNCTIONS
    //

    private void InitializeBlessingManager()
    {
        heroBaseController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseController>();
    }

    private void SetBlessingUI(object sender, BlessingEventArgs blessingEventArgs)
    {
        foreach (BlessingUI blessingUI in blessingUIList)
        {
            // Not empty
            if (!string.IsNullOrEmpty(blessingUI.blessingId))
            {
                if (blessingUI.blessingId == blessingEventArgs.blessingData.id)
                {
                    blessingUI.blessingLevel.text = blessingEventArgs.blessing.BlessingLevel.ToString();
                    break;
                }
            }
            // Weapon has data
            else
            {
                blessingUI.blessingId = blessingEventArgs.blessingData.id;
                blessingUI.blessingIcon.sprite = blessingEventArgs.blessingData.blessingSprite;
                blessingUI.blessingLevel.text = blessingEventArgs.blessingData.blessingLevel.ToString();
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

[System.Serializable]
public class BlessingUI
{
    public string blessingId;
    public Image blessingIcon;
    public TextMeshProUGUI blessingLevel;
}

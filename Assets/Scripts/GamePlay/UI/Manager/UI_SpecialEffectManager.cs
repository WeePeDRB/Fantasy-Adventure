using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SpecialEffectManager : MonoBehaviour
{
    //
    // FIELDS
    //

    private HeroBaseController heroBaseController;

    [SerializeField] private GameObject UIComponentPrefab;
    [SerializeField] private List<UI_SpecialEffectComponent> specialEffectUIList;

    //
    // FUNCTIONS
    //

    // INITIALIZE
    // Data
    private void InitializeSpEffectManagerData()
    {
        // 
        heroBaseController = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseController>();

        if (heroBaseController.HeroSpecialEffectSystem == null) Debug.LogError("Hero special effect is null !");
        heroBaseController.HeroSpecialEffectSystem.OnReceiveSpecialEffect += GetSpEffectData;

        specialEffectUIList = new List<UI_SpecialEffectComponent>();
    }

    //
    private void GetSpEffectData(object sender, OnReceiveSpecialEffectEventArgs spEffect)
    {
        //
        SpecialEffectBaseOld specialEffect = spEffect.specialEffect;
        //
        if (specialEffect != null)
        {
            //
            UI_SpecialEffectComponent specialEffectUI = Instantiate(UIComponentPrefab, transform).GetComponent<UI_SpecialEffectComponent>();

            specialEffectUI.GetSpecialEffect(specialEffect);
            specialEffectUI.SetUIComponent();
            specialEffectUI.OnSpecialEffectEnd += DeleteSpEffect;

            //
            if (specialEffectUIList.Count == 0)
            {
                specialEffectUIList.Add(specialEffectUI);
                specialEffectUI.StartCoolDownCoroutine();
            }
            else
            {
                for (int i = specialEffectUIList.Count - 1; i >= 0; i--)
                {
                    if (specialEffect.ID == specialEffectUIList[i].SpecialEffectID)
                    {
                        specialEffectUIList[i].ResetCoolDown();
                        return;
                    }
                }

                specialEffectUI.StartCoolDownCoroutine();
                specialEffectUIList.Add(specialEffectUI);
            }
        }
        else
        {
            Debug.LogError("Data null !");
        }
    }

    private void DeleteSpEffect(object sender, OnSpecialEffectEndEventArgs spEffectEndEventArg)
    {
        if (spEffectEndEventArg != null)
        {
            UI_SpecialEffectComponent specialEffect = spEffectEndEventArg.specialEffectComponent;
            specialEffect.OnSpecialEffectEnd -= DeleteSpEffect;
            specialEffectUIList.Remove(specialEffect);
            Destroy(specialEffect.gameObject);
        }
        else
        {
            Debug.LogError("Data is null !");
        }
    }

    private void Start()
    {
        InitializeSpEffectManagerData();
    }
}



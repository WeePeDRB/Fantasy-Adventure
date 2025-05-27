using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SpecialEffectComponent : MonoBehaviour
{
    // Tooltip data 
    private SO_SpecialEffect specialEffectData;
    private string toolTipDataType = new string("Special Effect: ");

    // UI data
    [SerializeField] private string specialEffectID;
    [SerializeField] private Image specialEffectIcon;
    [SerializeField] private Image specialEffectIconCover;
    private float specialEffectCoolDown;

    public event EventHandler<OnSpecialEffectEndEventArgs> OnSpecialEffectEnd;

    private Coroutine coolDownCoroutine;

    public string SpecialEffectID
    {
        get { return specialEffectID;  }
    }

    public void SetUIComponent(SO_SpecialEffect specialEffectData)
    {
        //
        this.specialEffectData = specialEffectData;
        //
        specialEffectID = specialEffectData.id;
        specialEffectIcon.sprite = specialEffectData.specialEffectSprite;
        specialEffectIconCover.sprite = specialEffectData.specialEffectSprite;
        specialEffectCoolDown = specialEffectData.specialEffectDuration;
    }

    public void StartCoolDownCoroutine()
    {
        if (coolDownCoroutine != null)
        {
            Debug.LogError("Coroutine already exist");
        }
        else
        {
            coolDownCoroutine = StartCoroutine(CoolDownCoroutine());
        }
    }

    private IEnumerator CoolDownCoroutine()
    {
        float elased = 0f;
        specialEffectIconCover.fillAmount = 1;
        while (elased < specialEffectCoolDown)
        {
            specialEffectIconCover.fillAmount = 1 - (elased / specialEffectCoolDown);
            elased += Time.deltaTime;
            yield return null;
        }
        specialEffectIconCover.fillAmount = 0;
        EndCoolDown();
    }

    public void ResetCoolDown()
    {
        if (coolDownCoroutine != null)
        {
            StopCoroutine(coolDownCoroutine);
            coolDownCoroutine = null;
        }
        StartCoolDownCoroutine();
    }

    private void EndCoolDown()
    {
        OnSpecialEffectEnd?.Invoke(this, new OnSpecialEffectEndEventArgs { specialEffectComponent = this });
    }
}

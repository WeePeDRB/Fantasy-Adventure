using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SpecialEffectComponent : MonoBehaviour
{
    public string specialEffectID;
    public Image specialEffectIcon;
    public Image specialEffectIconCover;

    private float specialEffectCoolDown;

    public event EventHandler<OnSpecialEffectEndEventArgs> OnSpecialEffectEnd;

    private Coroutine coolDownCoroutine;

    public void SetUIComponent(SO_SpecialEffect specialEffectData)
    {
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

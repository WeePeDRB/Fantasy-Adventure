using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SpecialEffectComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    // UI data
    private SpecialEffectBase spEffect;
    [SerializeField] private string specialEffectID;
    [SerializeField] private Image specialEffectIcon;
    [SerializeField] private Image specialEffectIconCover;
    private float specialEffectCoolDown;

    public event EventHandler<OnSpecialEffectEndEventArgs> OnSpecialEffectEnd;

    private Coroutine coolDownCoroutine;

    public string SpecialEffectID
    {
        get { return specialEffectID; }
    }

    public void GetSpecialEffect(SpecialEffectBase specialEffect)
    {
        if (specialEffect == null)
        {
            Debug.LogError("Special effect data is missing.");
            return;
        }
        spEffect = specialEffect;
    }

    public void SetUIComponent()
    {
        specialEffectID = spEffect.ID;
        specialEffectIcon.sprite = spEffect.SpEffectSprite;
        specialEffectIconCover.sprite = spEffect.SpEffectSprite;
        specialEffectCoolDown = spEffect.SpEffectDuration;
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
        UI_TooltipManager.Instance.HideSpecialEffectTooltip();
    }

    // Tooltip logic
    public void OnPointerEnter(PointerEventData eventData)
    {
        UI_TooltipManager.Instance.ShowSpecialEffectTooltip(spEffect.SpEffectName, spEffect.SpEffectDescription, spEffect.SpEffectSprite);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI_TooltipManager.Instance.HideSpecialEffectTooltip();
    }

}

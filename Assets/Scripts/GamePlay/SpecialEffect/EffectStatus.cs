using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectStatus 
{
    private List<SpecialEffectBase> activeEffects = new List<SpecialEffectBase>();
    private CharacterBaseController character;

    public EffectStatus(CharacterBaseController character)
    {
        this.character = character;
    }

    public void ApplyEffect(SpecialEffectBase newEffect)
    {
        // Kiểm tra xem hiệu ứng đã tồn tại chưa
        foreach (var effect in activeEffects)
        {
            if (effect.effectName == newEffect.effectName)
                return;
        }

        // Thêm hiệu ứng vào danh sách
        activeEffects.Add(newEffect);
        newEffect.StartEffect(character, RemoveEffect);
    }

    public void UpdateEffects(float deltaTime)
    {
        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            activeEffects[i].UpdateEffect(deltaTime, character);
        }
    }

    private void RemoveEffect(SpecialEffectBase effect)
    {
        activeEffects.Remove(effect);
    }

}

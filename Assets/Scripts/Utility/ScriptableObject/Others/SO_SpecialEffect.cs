using UnityEngine;

[CreateAssetMenu()]
public class SO_SpecialEffect : ScriptableObject
{
    // Special effect data
    public string id;
    public string spEffectName;
    public string spEffectDescription;
    public float spEffectDuration;
    public float spEffectValue;
    public Sprite spEffectSprite;
    public EffectTarget spEffectTarget;
    public EffectType spEffectType;
}


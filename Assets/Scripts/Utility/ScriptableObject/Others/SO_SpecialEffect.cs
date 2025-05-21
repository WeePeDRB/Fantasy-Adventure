using UnityEngine;

[CreateAssetMenu()]
public class SO_SpecialEffect : ScriptableObject
{
    // Special effect id
    public string id;

    // Essential data
    public string specialEffectName;
    public float specialEffectDuration;
    public float specialEffectValue;
    public EffectTarget specialEffectTarget;
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SO_HeroStats : ScriptableObject
{
    // Basic stats
    public float maxHealth;
    public float maxAmor;
    public float speed;
    public float resistance;
    public float damageAmplifier;
    public float cooldownReduction;
    public float attackSpeed;
    public float criticalChance;
}

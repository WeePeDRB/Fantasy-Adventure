using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu()]
public class SO_CharacterSkill : ScriptableObject
{
    public Sprite skillSprite;
    public string skillName;
    public string skillDescription;
    public float skillCooldown;
}

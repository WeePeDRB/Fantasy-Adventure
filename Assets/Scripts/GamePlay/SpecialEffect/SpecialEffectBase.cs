using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   
public abstract class SpecialEffectBase
{
    public abstract void StartEffect(CharacterBaseController character);
    public abstract void EndEffect();
}
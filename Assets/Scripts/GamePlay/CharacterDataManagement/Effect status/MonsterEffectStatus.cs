using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEffectStatus : CharacterEffectStatus
{
    public override void UpdateEffects(float deltaTime)
    {
        foreach (var effect in activeEffects.Values)
        {
            if (effect.TimeRemaining <= 0)
            {
                RemoveEffect(effect.EffectName);
            }
            else
            {
                effect.UpdateTime(deltaTime);
            }
        }    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    // INITIALIZE STATS FOR WEAPON
    void InitializeWeapon();

    // EQUIP WEAPON
    void EquipWeapon();
    
    // UPGRADE WEAPON (Increase weapon stats)     
    void UpgradeWeapon();
    
    // WEAPON ATTACK LOGIC
    IEnumerator AttackCoroutine();
    
}

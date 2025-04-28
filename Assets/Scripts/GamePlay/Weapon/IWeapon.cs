using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    // EQUIP WEAPON
    void EquipWeapon();
    
    // UPGRADE WEAPON (Increase weapon stats)     
    void UpgradeWeapon();
    
    // WEAPON ATTACK LOGIC
    void Attack();
    IEnumerator AttackCoroutine();
    
}

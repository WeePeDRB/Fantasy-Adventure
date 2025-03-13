using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    //
    // Add the weapon to the player inventory
    //
    void EquipWeapon();
    

    //
    // Upgrade the weapon level (Increase weapon stats) 
    //      
    void UpgradeWeapon();
    

    //
    // Weapon attack
    //
    void Attack();
    

    //
    // Move weapon to the left
    //  
    void MoveToLeft(int parentPos);
    
    
    //
    // Move weapon to the right
    //  
    void MoveToRight(int parentPos);
}

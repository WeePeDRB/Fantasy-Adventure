using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    //
    //  Summary:
    //      Add the weapon to the player inventory
    //
    void EquipWeapon();
    

    //
    //  Summary:
    //      Upgrade the weapon level (Increase weapon stats) 
    //      
    void UpgradeWeapon();
    

    //
    //  Summary:
    //      Weapon attack
    //
    void Attack();
    

    //
    //  Summary:
    //      Move weapon to the left
    //  
    //  Parameters:
    //      parentPos: the parent object position 
    void MoveToLeft(int parentPos);
    
    
    //
    //  Summary:
    //      Move weapon to the right
    //  
    //  Parameters:
    //      parentPos: the parent object position
    void MoveToRight(int parentPos);
}

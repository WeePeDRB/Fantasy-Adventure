using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    //
    // Flags to control the manager behavior
    //
    // This flag is used to check if combat time is not over
    private bool isInCombat; 
    // This flag is used to check if the monster quantity has decreased and more
    // monsters need to be spawned
    private bool isSpawningMonster;


    private int maxMonsterQuantity;
    private int activeMonsterQuantity;

}

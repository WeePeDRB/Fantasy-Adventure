using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SO_MonsterList : ScriptableObject
{   
    // SO_Monster list
    public List<SO_Monster> monsterDataList;

    // Return SO_Monster if match monster id
    public SO_Monster GetMonsterById(int id)
    {
        return monsterDataList.Find(monster => monster.id == id );
    }
}

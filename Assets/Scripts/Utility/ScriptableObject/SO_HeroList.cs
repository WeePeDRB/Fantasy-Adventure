using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SO_HeroList : ScriptableObject
{
    // SO_Character list
    public List<SO_Hero> heroList;

    // Return SO_Character if match character id
    public SO_Hero GetCharacterById(int id)
    {
        return heroList.Find(character => character.id == id );
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SO_CharacterList : ScriptableObject
{
    // SO_Character list
    public List<SO_Character> characterDataList;

    // Return SO_Character if match character id
    public SO_Character GetCharacterById(int id)
    {
        return characterDataList.Find(character => character.id == id );
    }
}

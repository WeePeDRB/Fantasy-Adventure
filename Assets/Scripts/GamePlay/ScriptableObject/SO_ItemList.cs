using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SO_ItemList : ScriptableObject
{
    // SO_Item list
    public List<SO_Item> itemDataList;

    // Return SO_Item if match item id
    public SO_Item GetItemById(int id)
    {
        return itemDataList.Find(item => item.id == id);
    }
}

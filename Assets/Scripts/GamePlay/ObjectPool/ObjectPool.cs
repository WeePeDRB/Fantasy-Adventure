using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{

    //
    // Monster object pool
    //
    // The Queue that will store monster game object
    protected Queue<GameObject> objectPool;
    // Monster data
    protected GameObject objectPrefab;
    // Max monster quantity
    protected int objectPoolSize;



    // Instantiate monster pool values
    protected void InstantiatePoolValue(SO_Monster monsterData, int poolSize)
    {   
        objectPool = new Queue<GameObject>();
        objectPoolSize = poolSize;
        objectPrefab = monsterData.monsterPrefab;
    }

    // Create object pool
    protected void CreatePool()
    {
        for (int i = 0; i < objectPoolSize; i ++)
        {
            GameObject obj = Instantiate(objectPrefab);
            objectPool.Enqueue(obj);
            obj.SetActive(false);
        }
    } 

    // Get object from pool
    protected GameObject GetObject()
    {
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        return null;
    }

    // Return object to pool;
    protected void ReturnObject(GameObject obj)
    {   
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
}

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
    public void InstantiatePoolValue(GameObject objectPrefab, int poolSize)
    {   
        objectPool = new Queue<GameObject>();
        objectPoolSize = poolSize;
        this.objectPrefab = objectPrefab;
    }

    // Create object pool
    public void CreatePool()
    {
        for (int i = 0; i < objectPoolSize; i ++)
        {
            GameObject obj = Instantiate(objectPrefab);
            objectPool.Enqueue(obj);
            obj.SetActive(false);
        }
    } 

    // Get object from pool
    public GameObject GetObject(Transform objectTransform)
    {
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool.Dequeue();
            obj.transform.position = objectTransform.position;
            obj.SetActive(true);
            return obj;
        }
        return null;
    }

    // Return object to pool;
    public void ReturnObject(GameObject obj)
    {   
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
}

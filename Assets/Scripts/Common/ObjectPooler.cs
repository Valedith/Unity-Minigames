using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
    [SerializeField] GameObject objectToPool;
    [SerializeField] int amountToPool;

    public static ObjectPooler Instance;
    List<GameObject> poolList;
    

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        poolList = new List<GameObject>();
        for(int i = 0; i < amountToPool; i++)
        {
            GameObject newObject = Instantiate(objectToPool);
            newObject.SetActive(false);
            poolList.Add(newObject);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach(var gameObj in poolList)
        {
            if(!gameObj.activeInHierarchy)
            {
                return gameObj;
            }
        }
        return null;
    }
}

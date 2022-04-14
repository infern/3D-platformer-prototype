using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> projectilePool;
    public GameObject projectileToPool;
    public int amountOfProjectiles;



    void Awake()
    {
        SharedInstance = this;
    }

     void Start()
    {
        projectilePool = new List<GameObject>();
        GameObject tmp;
        for(int i=0; i< amountOfProjectiles; i++)
        {
            tmp = Instantiate(projectileToPool);
            tmp.SetActive(false);
            projectilePool.Add(tmp);
        }


    }

    public GameObject GetProjectileFromPool()
    {
        for (int i = 0;  i < amountOfProjectiles; i++)
        {
            if (!projectilePool[i].activeInHierarchy) return projectilePool[i];
        }
        return null;
    }



}

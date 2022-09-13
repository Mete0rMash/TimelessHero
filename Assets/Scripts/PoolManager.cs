using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject projectilePrefab2;
    [SerializeField] private GameObject enemyPrefab1;
    [SerializeField] int spawnCount;
    [SerializeField] int spawnCount2;
    [SerializeField] int spawnCount3;
    public List<GameObject> missiles;
    public List<GameObject> fireballs;
    public List<GameObject> enemies;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        missiles = new List<GameObject>();
        fireballs = new List<GameObject>();
        enemies = new List<GameObject>();
        GameObject temp;

        for (int i = 0; i < spawnCount; i++)
        {
            temp = Instantiate(projectilePrefab);
            temp.SetActive(false);
            missiles.Add(temp);
        }

        for (int i = 0; i < spawnCount2; i++)
        {
            temp = Instantiate(projectilePrefab2);
            temp.SetActive(false);
            fireballs.Add(temp);
        }

        for (int i = 0; i < spawnCount3; i++)
        {
            temp = Instantiate(enemyPrefab1);
            temp.SetActive(false);
            enemies.Add(temp);
        }
    }
        
    public GameObject GetPooledObject(string type)
    {
        if (type == "missiles")
        {
            for (int i = 0; i < spawnCount; i++)
            {
                if (!missiles[i].activeInHierarchy)
                {
                    return missiles[i];
                }
            }
        }

        if (type == "fireballs")
        {
            for (int i = 0; i < spawnCount2; i++)
            {
                if (!fireballs[i].activeInHierarchy)
                {
                    return fireballs[i];
                }
            }
        }

        if (type == "enemies")
        {
            for (int i = 0; i < spawnCount3; i++)
            {
                if (!enemies[i].activeInHierarchy)
                {
                    return enemies[i];
                }
            }
        }

        return null;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float x;
    public float z;
    public int enemyCount;

    private void Update() 
    { 
        GameObject enemy = PoolManager.instance.GetPooledObject("enemies");

        if (enemy != null)
        {
            x = Random.Range(10, 25);
            z = Random.Range(70, 90);
            enemy.transform.position = new Vector3(x, transform.position.y, z);
            enemy.transform.rotation = transform.rotation;
            enemy.SetActive(true);
        }
    }

    
}

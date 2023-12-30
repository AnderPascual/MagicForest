using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Windows;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private float timeSpawn = 1.6f;
    [SerializeField] private float speed = 5;
    [SerializeField] private GameObject enemyBulletPrefab;

    [SerializeField] private int amountToPool;
    [SerializeField] private List<GameObject> pooledObjects;

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(enemyBulletPrefab);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }

        StartCoroutine(SpawnBullet());
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
            
        }
        return null;
    }

    IEnumerator SpawnBullet()
    {
        while (!GameManager.Instance.playerIsDead)
        {
            GameObject bullet = GetPooledObject();
            if (bullet != null)
            {
                bullet.SetActive(true);
                bullet.transform.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                
            }
            yield return new WaitForSeconds(timeSpawn);
        }

    }
}

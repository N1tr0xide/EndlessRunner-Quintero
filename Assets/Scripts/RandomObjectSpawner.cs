using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour
{
    private List<GameObject> _pooledObjects;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject[] _objectsToPool;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _pooledObjects = new List<GameObject>();
        
        foreach (var poolObj in _objectsToPool)
        {
            AddObjectToPool(poolObj);
        }
    }

    private void SpawnObject()
    {
        string randomObj = _objectsToPool[Random.Range(0, _objectsToPool.Length)].tag;
        GameObject newObj = randomObj is "Alien" or "Obstacle" or "Powerup"? GetObject(randomObj, false) : GetObject(randomObj);
        
        if(newObj == null) return;

        switch (randomObj)
        {
            case "Alien":
                newObj.transform.position = _spawnPoint.position;
                newObj.SetActive(true);
                break;
            default:
                newObj.transform.position = _spawnPoint.position + new Vector3(0,Random.Range(0,3));
                newObj.SetActive(true);
                break;
        }
    }

    private GameObject GetObject(string objTag, bool shouldExpand = true)
    {
        foreach (var obj in _pooledObjects)
        {
            if (!obj.activeInHierarchy && obj.CompareTag(objTag)) 
            {
                return obj;
            }
        }

        if (!shouldExpand) return null;
        
        foreach (var poolObj in _objectsToPool)
        {
            if (poolObj.CompareTag(objTag))
            {
                return AddObjectToPool(poolObj);
            }
        }

        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
        Debug.LogError("Object with tag not found in Pool. New object could not be added.");
        return null;
    }

    private GameObject AddObjectToPool(GameObject newObject)
    {
        GameObject obj = (GameObject)Instantiate(newObject);
        obj.SetActive(false); 
        _pooledObjects.Add(obj);
        return obj;
    }

    private IEnumerator SpawnRandomly()
    {
        while (!GameManager.Instance.IsGameOver)
        {
            yield return new WaitForSeconds(Random.Range(1, 3));
            SpawnObject();
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnRandomly());
    }
}

using UnityEngine;
using System.Collections;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private Transform pointA;     
    [SerializeField] private Transform pointB;     
    [SerializeField] private GameObject prefab;   
    [SerializeField] private float minDelay = 2f;  
    [SerializeField] private float maxDelay = 5f;  

    private bool _isSpawning = false;
    private Coroutine _spawnCoroutine;

    void Start()
    {
        StartSpawn();
    }
    public void StartSpawn()
    {
        if (_isSpawning) return;
        _isSpawning = true;
        _spawnCoroutine = StartCoroutine(SpawnLoop());
    }

    public void StopSpawn()
    {
        if (!_isSpawning) return;
        _isSpawning = false;
        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);
    }

    private IEnumerator SpawnLoop()
    {
        while (_isSpawning)
        {
            SpawnObject();
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);
        }
    }

    private void SpawnObject()
    {
        float x = Random.Range(pointA.position.x, pointB.position.x);
        float y = Random.Range(pointA.position.y, pointB.position.y);
        Vector2 spawnPos = new Vector2(x, y);

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}

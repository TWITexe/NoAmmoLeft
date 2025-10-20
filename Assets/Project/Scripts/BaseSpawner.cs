using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static NTC.Pool.NightPool;
using Random = UnityEngine.Random;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _pointA;

    [SerializeField]
    private Transform _pointB;

    [SerializeField]
    private GameObject _prefab;

    [SerializeField]
    private float _minDelay = 2f;

    [SerializeField]
    private float _maxDelay = 5f;

    private List<GameObject> _objectList = new();

    private Coroutine _spawnCoroutine;

    private bool _isSpawning = false;

    private void Awake()
    {
        this.ValidateSerializedFields();
    }

    public void StartSpawn()
    {
        if (_isSpawning) return;
        _isSpawning = true;
        _spawnCoroutine = StartCoroutine(SpawnLoop());
    }

    public void StopSpawn()
    {
        if (!_isSpawning)
            return;

        _isSpawning = false;

        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);

        DespawnAll();
    }

    private void DespawnAll()
    {
        foreach (GameObject obj in _objectList)
        {
            Despawn(obj);
        }
    }

    private IEnumerator SpawnLoop()
    {
        while (_isSpawning)
        {
            SpawnObject();
            float delay = Random.Range(_minDelay, _maxDelay);
            yield return new WaitForSeconds(delay);
        }
    }

    private void SpawnObject()
    {
        float x = Random.Range(_pointA.position.x, _pointB.position.x);
        float y = Random.Range(_pointA.position.y, _pointB.position.y);
        Vector2 spawnPos = new(x, y);

        var obj = Spawn(_prefab, spawnPos, Quaternion.identity);

        if (_objectList.Contains(obj))
            return;

        _objectList.Add(obj);
    }
}
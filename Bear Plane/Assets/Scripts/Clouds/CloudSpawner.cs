using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> _cloudPrefabs;
    [SerializeField] float _minZ;
    [SerializeField] float _maxZ;
    [SerializeField] float _minY;
    [SerializeField] float _maxY;
    [SerializeField] float _xSpawnPos;

    [SerializeField] float _distanceBetweenSpawns;
    private float _prevX;
    private float _currentX;

    void Start()
    {
        _currentX = transform.position.x;
        _prevX = _currentX;
    }

    void Update()
    {
        _currentX = transform.position.x;
        if (_currentX - _prevX >= _distanceBetweenSpawns)
        {
            SpawnCloud();
            _prevX = _currentX;
        }
    }

    void SpawnCloud()
    {
        Vector3 spawnPos = new Vector3(_xSpawnPos + _currentX, Random.Range(_minY, _maxY), Random.Range(_minZ, _maxZ));
        GameObject cloud = Instantiate(_cloudPrefabs[Random.Range(0, _cloudPrefabs.Count)], spawnPos, transform.rotation, null);
        cloud.GetComponent<SpriteRenderer>().sortingOrder = -(int)spawnPos.z;
    }
}

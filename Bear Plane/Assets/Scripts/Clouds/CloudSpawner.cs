using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] List<Cloud> _cloudPrefabs;
    [SerializeField] List<Cloud> _startingClouds;
    [SerializeField] int _minZ;
    [SerializeField] int _maxZ;
    [SerializeField] float _minY;
    [SerializeField] float _maxY;
    [SerializeField] float _xSpawnPos;
    [SerializeField] float _cloudSpeed;
    [SerializeField] float _secondsBetweenSpawns;

    void Start()
    {
        StartCoroutine(SpawnCloudCo());

        foreach (Cloud startingCloud in _startingClouds)
        {
            startingCloud.Init(-_xSpawnPos, _cloudSpeed, startingCloud.spriteRenderer.sortingOrder);
        }
    }

    void SpawnCloud()
    {
        int randZ = 0;
        while (randZ == 0 || randZ == 1)
        {
            randZ = Random.Range(_minZ, _maxZ);
        }

        Vector3 spawnPos = new Vector3(_xSpawnPos, Random.Range(_minY, _maxY), randZ);
        Cloud cloud = Instantiate(_cloudPrefabs[Random.Range(0, _cloudPrefabs.Count)], spawnPos, transform.rotation, null) as Cloud;
        cloud.Init(-_xSpawnPos, _cloudSpeed, -(int)spawnPos.z);
    }

    private IEnumerator SpawnCloudCo()
    {
        yield return new WaitForSeconds(_secondsBetweenSpawns);
        SpawnCloud();
        StartCoroutine(SpawnCloudCo());
    }
}

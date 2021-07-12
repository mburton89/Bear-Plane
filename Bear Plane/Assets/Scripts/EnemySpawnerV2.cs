using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerV2 : MonoBehaviour
{
    public static EnemySpawnerV2 Instance;

    [SerializeField] private List<Plane> _enemyPlanePrefabs;
    [SerializeField] private Plane _enemyLeftFlyPlane;

    [SerializeField] private float _secondsBetweenSpawns;
    [SerializeField] private float _x;
    [SerializeField] private float _maxY;
    [SerializeField] private float _minY;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnPlanes());
    }

    void SpawnEnemies(int numberOfPlanes, Plane planeType)
    {

    }

    IEnumerator SpawnPlanes()
    {
        yield return new WaitForSeconds(_secondsBetweenSpawns);

        int rand = Random.Range(0, 2);
        float _y = Random.Range(_minY, _maxY);
        if (rand == 1)
        {
            Vector3 spawnPosition = new Vector3(-_x, _y, 0);
            int planePrefabIndex = Random.Range(0, 3);
            Instantiate(_enemyPlanePrefabs[planePrefabIndex], spawnPosition, this.transform.rotation, this.transform);
        }
        else
        {
            Vector3 spawnPosition = new Vector3(_x, _y, 0);
            Instantiate(_enemyLeftFlyPlane, spawnPosition, this.transform.rotation, this.transform);
        }
        StartCoroutine(SpawnPlanes());
    }

    void SpawnLeftFlyingPlane()
    {

    }
}

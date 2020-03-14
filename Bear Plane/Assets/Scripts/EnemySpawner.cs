using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [SerializeField] Plane _enemyPlanePrefab;
    [SerializeField] List<Vector3> _spawnPositions;
    [SerializeField] List<GameObject> _scenarioPrefabs;
    private GameObject _currentScenario;
    private int _enemyCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _enemyCount = FindObjectsOfType<Plane>().Length;
    }

    void SpawnEnemies(int numberOfPlanes, Plane planeType)
    {
        for (int i = 0; i < numberOfPlanes; i++)
        {
            Instantiate(_enemyPlanePrefab, _spawnPositions[i], this.transform.rotation, this.transform);
        }
    }

    void SpawnEnemies()
    {
        int randomScenario = Random.Range(0, _scenarioPrefabs.Count);
        _currentScenario = Instantiate(_scenarioPrefabs[randomScenario], transform.position, transform.rotation, transform);
        _enemyCount = FindObjectsOfType<Plane>().Length;
    }

    public void CheckEnemyCount()
    {
        int enemyCount = FindObjectsOfType<Plane>().Length;
        if (enemyCount <= 1)
        {
            Destroy(_currentScenario);
            SpawnEnemies();
        }
    }
}

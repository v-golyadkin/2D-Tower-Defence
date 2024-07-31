using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] _enemyPrefabs;

    [Header("Atributes")]
    [SerializeField] private int _baseEnemies = 8;
    [SerializeField] private float _enemiesPerSecond = 0.5f;
    [SerializeField] private float _timeBerweenWaves = 5f;
    [SerializeField] private float _difficultyScallingFactor = 0.75f;
    [SerializeField] private float _enemiesPerSecondCap = 10f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int _currentWave = 1;
    private float _timeSinceLastSpawn;
    private int _enemiesAlive;
    private int _enemiesLeftToSpawn;
    private float _enemiesPerSecondScalling;
    private bool _isSpawning = false;

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Update()
    {
        if (!_isSpawning) return;
        _timeSinceLastSpawn += Time.deltaTime;

        if((_timeSinceLastSpawn >= (1f /  _enemiesPerSecondScalling)) && _enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();

            _enemiesLeftToSpawn--;
            _enemiesAlive++;
            _timeSinceLastSpawn = 0f;
        }

        if( _enemiesLeftToSpawn == 0 && _enemiesAlive == 0 )
        {
            EndWave();
        }
    }

    private void EndWave()
    {
        _isSpawning = false;
        _timeSinceLastSpawn = 0f;
        _currentWave++;
        StartCoroutine(StartWave());
    }

    private void EnemyDestroyed()
    {
        _enemiesAlive--;
    }

    private void SpawnEnemy()
    {
        int index = UnityEngine.Random.Range(0, _enemyPrefabs.Length);
        GameObject prefabToSpawn = _enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.instance.startPoint.position, Quaternion.identity);
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(_timeBerweenWaves);
        _isSpawning = true;
        _enemiesLeftToSpawn = EnemiesPerWave();
        _enemiesPerSecondScalling = EnemiesPerSecond();
    }


    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(_baseEnemies * Mathf.Pow(_currentWave, _difficultyScallingFactor));
    }
    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(_enemiesPerSecond * Mathf.Pow(_currentWave, _difficultyScallingFactor), 0f, _enemiesPerSecondCap);
    }
}

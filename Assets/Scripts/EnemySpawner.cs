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

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int _currentWave = 1;
    private float _timeSinceLastSpawn;
    private int _enemiesAlive;
    private int _enemiesLeftToSpawn;
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

        if((_timeSinceLastSpawn >= (1f /  _enemiesPerSecond)) && _enemiesLeftToSpawn > 0)
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
        GameObject prefabToSpawn = _enemyPrefabs[0];
        Instantiate(prefabToSpawn, LevelManager.instance.startPoint.position, Quaternion.identity);
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(_timeBerweenWaves);
        _isSpawning = true;
        _enemiesLeftToSpawn = EnemiesPerWave();
    }


    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(_baseEnemies * Mathf.Pow(_currentWave, _difficultyScallingFactor));
    }
}

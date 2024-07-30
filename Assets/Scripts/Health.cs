using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int _hitPoint = 2;
    [SerializeField] private int _currencyWorth = 50;

    private bool _isDestroyed = false;

    public void TakeDamage(int damage)
    {
        _hitPoint -= damage;

        if(_hitPoint <= 0 && !_isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.instance.IncreaseCurrency(_currencyWorth);
            _isDestroyed = true;
            Destroy(gameObject);
        }
    }
}

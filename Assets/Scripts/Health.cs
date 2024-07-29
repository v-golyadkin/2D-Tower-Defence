using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int _hitPoint = 2;

    private bool _isDestroyed = false;

    public void TakeDamage(int damage)
    {
        _hitPoint -= damage;

        if(_hitPoint <= 0 && !_isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            _isDestroyed = true;
            Destroy(gameObject);
        }
    }
}

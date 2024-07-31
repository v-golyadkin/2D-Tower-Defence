using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretSlowmo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask _enemyMask;

    [Header("Attribute")]
    [SerializeField] private float _targetingRange = 5f;
    [SerializeField] private float _attackPerSecond = 4f;
    [SerializeField] private float _freezeTime = 1f;

    private float _timeUntilFire;

    private void Update()
    {
        _timeUntilFire += Time.deltaTime;

        if (_timeUntilFire >= 1f / _attackPerSecond)
        {
            FreezeEnemies();
            _timeUntilFire = 0;
        }
    }

    private void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _targetingRange, (Vector2)transform.position, 0f, _enemyMask);

        if (hits.Length > 0)
        {
            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                EnemyMovement enemyMovement =  hit.transform.GetComponent<EnemyMovement>();
                enemyMovement.UpdateSpeed(0.5f);

                StartCoroutine(ResetEnemySpeed(enemyMovement));
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement enemyMovement)
    {
        yield return new WaitForSeconds(_freezeTime);

        enemyMovement.ResetSpeed();
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, _targetingRange);
    }
}

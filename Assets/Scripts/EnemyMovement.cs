using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //[Header("References")]


    [Header("Attributes")]
    [SerializeField] private float _movespeed = 2f;

    private Rigidbody2D _body;

    private Transform _target;
    private int _pathIndex = 0;

    private void Start()
    {
        _target = LevelManager.instance.path[_pathIndex];
    }

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Vector2.Distance(_target.position, transform.position) <= 0.1f)
        {
            _pathIndex++;

            if(_pathIndex >= LevelManager.instance.path.Length)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                _target = LevelManager.instance.path[_pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (_target.position - transform.position).normalized;

        _body.velocity = direction * _movespeed;
    }
}

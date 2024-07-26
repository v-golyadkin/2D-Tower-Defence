using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private int _bulletDamage = 1;

    private Transform _target;
    private Rigidbody2D _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void FixedUpdate()
    {
        if(!_target) return;

        Vector2 direction = (_target.position - transform.position).normalized;

        _body.velocity = direction * _bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Health>().TakeDamage(_bulletDamage);
        Destroy(gameObject);
    }
}

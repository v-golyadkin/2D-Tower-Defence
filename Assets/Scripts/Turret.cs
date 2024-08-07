using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _turretRotationPoint;
    [SerializeField] private Transform _firingPoint;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _upgradeUI;
    [SerializeField] private Button _upgradeButton;

    [Header("Attribute")]
    [SerializeField] private float _targetingRange = 5f;
    [SerializeField] private float _rotationSpeed = 200f;
    [SerializeField] private float _bulletPerSecond = 1f;
    [SerializeField] private int _baseUpgradeCost = 100;

    private Transform _target;
    private float _timeUntilFire;
    
    private float _targetingRangeBase;
    private float _bulletPerSecondBase;

    private int _level = 1;

    private void Start()
    {
        _targetingRangeBase = _targetingRange;
        _bulletPerSecondBase = _bulletPerSecond;

        _upgradeButton.onClick.AddListener(Upgrade);
    }

    private void Update()
    {
        if(_target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardTarget();

        if (!CheckTargetIsInRange())
        {
            _target = null;
        }
        else
        {
            _timeUntilFire += Time.deltaTime;

            if( _timeUntilFire >= 1f / _bulletPerSecond)
            {
                Shoot();
                _timeUntilFire = 0;
            }
        }
    }

    private void Shoot()
    {
        GameObject bulletObject = Instantiate(_bulletPrefab, _firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObject.GetComponent<Bullet>();
        bulletScript.SetTarget(_target);
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(_target.position, transform.position) <= _targetingRange;
    }

    private void RotateTowardTarget()
    {
        float angel = Mathf.Atan2(_target.position.y - transform.position.y, _target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angel));
        _turretRotationPoint.rotation = Quaternion.RotateTowards(_turretRotationPoint.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, _targetingRange, (Vector2)transform.position, 0f, _enemyMask);

        if(hits.Length > 0 ) 
        {
            _target = hits[0].transform;
        }
    }

    public void OpenUpgradeUI()
    {
        _upgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {
        _upgradeUI.SetActive(false);
        UIManager.instance.SetHoveringStage(false);
    }

    public void Upgrade()
    {
        if (CalculateCost() > LevelManager.instance.currency) return;

        LevelManager.instance.SpendCurrency(CalculateCost());

        _level++;

        _bulletPerSecond = CalculateBPS();
        _targetingRange = CalculateRange();


        CloseUpgradeUI();
        Debug.Log("New bps" + _bulletPerSecond);
        Debug.Log("New range" + _targetingRange);
        Debug.Log("New cost" + CalculateCost());
    }

    private int CalculateCost()
    {
        return Mathf.RoundToInt(_baseUpgradeCost * Mathf.Pow(_level, 0.8f));
    }

    private float CalculateBPS()
    {
        return _bulletPerSecondBase * Mathf.Pow(_level, 0.6f);
    }

    private float CalculateRange()
    {
        return _targetingRangeBase * Mathf.Pow(_level, 0.4f);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, _targetingRange);
    }
}

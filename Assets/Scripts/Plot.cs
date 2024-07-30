using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Color _hoverColor;

    private SpriteRenderer _spriteRender;
    private GameObject _tower = null;
    private Color _startColor;


    private void Awake()
    {
         _spriteRender = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _startColor = _spriteRender.color;
    }

    private void OnMouseEnter()
    {
        _spriteRender.color = _hoverColor;
    }

    private void OnMouseExit()
    {
        _spriteRender.color = _startColor;
    }

    private void OnMouseDown()
    {
        if (_tower != null) return;
        
        Tower towerToBuild = BuildManager.instance.GetSelectedTower();

        if(towerToBuild.cost > LevelManager.instance.currency)
        {
            Debug.Log("You can't afford this tower");
            return;
        }

        LevelManager.instance.SpendCurrency(towerToBuild.cost);

        _tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] _towerPrefabs;

    private int _selectedTower = 0;

    public static BuildManager instance;

    private void Awake()
    {
        instance = this; 
    }

    public GameObject GetSelectedTower()
    {
        return _towerPrefabs[_selectedTower];
    }
}

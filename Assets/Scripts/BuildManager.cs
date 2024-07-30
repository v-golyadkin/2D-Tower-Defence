using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Tower[] _towers;

    private int _selectedTower = 0;

    public static BuildManager instance;

    private void Awake()
    {
        instance = this; 
    }

    public Tower GetSelectedTower()
    {
        return _towers[_selectedTower];
    }

    public void SetSelectedTower(int selectedTower)
    {
        _selectedTower = selectedTower;
    }
}

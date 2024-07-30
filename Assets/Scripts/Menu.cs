using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;

    private Animator _animator;
    private bool _isMenuOpen = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ToggleMenu()
    {
        _isMenuOpen = !_isMenuOpen;
        _animator.SetBool("MenuOpen", _isMenuOpen);
    }

    private void OnGUI()
    {
        currencyUI.text = LevelManager.instance.currency.ToString();
    }

    public void SetSelected()
    {

    }
}

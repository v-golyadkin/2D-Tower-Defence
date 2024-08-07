using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private bool _isHoveringUI;

    private void Awake()
    {
        instance = this;
    }

    public void SetHoveringStage(bool state)
    {
        _isHoveringUI = state;
    }

    public bool IsHoveringUI()
    {
        return _isHoveringUI;
    }
}

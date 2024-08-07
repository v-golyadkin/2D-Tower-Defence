using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool _mouseOver = false;

    private void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _mouseOver = true;
        UIManager.instance.SetHoveringStage(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _mouseOver = false;
        UIManager.instance.SetHoveringStage(false);
        gameObject.SetActive(false);
    }
}

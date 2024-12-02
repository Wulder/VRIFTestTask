using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputModeSwitcher : MonoBehaviour
{
    public UnityEvent OnRayModeSelected, OnStickModeSelected = new UnityEvent();
    [SerializeField] private Button _stickModeBtn, _rayModeBtn;
    private void Start()
    {
        _rayModeBtn.onClick?.Invoke();
    }
    public void SetRayMode()
    {
        OnRayModeSelected?.Invoke();
    }

    public void SetStickMode()
    {
     OnStickModeSelected?.Invoke();
    }
}

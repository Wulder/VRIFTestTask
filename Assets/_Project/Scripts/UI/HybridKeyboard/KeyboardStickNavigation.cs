using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class KeyboardStickNavigation : MonoBehaviour
{
    [SerializeField] private EventSystem _eventSystem; //event system handling keyboard
    [SerializeField] private InputActionReference _inputNavigationAction; //action from stick/wasd
    [SerializeField] private InputActionReference _inputPressAction; //action for pressing on keys
    [SerializeField] private Vector2Int _pointer; //pointer with row and column indexes
    [SerializeField] private KeyboardNavigationMap _currentNavigationMap; //navigatin map allows change pointer value for selecting keys


    private Vector2Int _prevPointerValue;
    private Vector2 _lastInput;
    private bool _isReleased;

    private GameObject _prevTarget; //preview selected key's gameObject
    private GameObject _target; //current key's gameObject

    private void OnEnable()
    {
        _inputPressAction.action.performed += OnPress;
    }
    private void OnDisable()
    {
        _inputPressAction.action.performed -= OnPress;
    }
    private void Update()
    {
        UpdatePointer();

    }
    private void UpdatePointer()
    {
        _lastInput = _inputNavigationAction.action.ReadValue<Vector2>().normalized;
        _prevPointerValue = _pointer;
        if (_lastInput == Vector2.zero && _isReleased == false)
        {
            _isReleased = true;
            return;
        }

        var x = Mathf.Abs(_lastInput.x);
        var y = Mathf.Abs(_lastInput.y);

        if (x == 0 && y == 0
            || x == y) { return; }
        else if (_isReleased)
        {
            _isReleased = false;
            if (x > y) //if horizontal value > vertical value
            {
                if (_lastInput.x > 0) _pointer.x++; else _pointer.x--; //change position in the row (select column)

            }
            else
            {
                if (_lastInput.y > 0) _pointer.y++; else _pointer.y--; //change row
            }
        }



        //clamp and moving pointer by keys in row count
        int rowsInMap = _currentNavigationMap.map.Count;
        if (_pointer.y < 0) _pointer.y = rowsInMap - 1;
        if (_pointer.y > rowsInMap - 1) _pointer.y = 0;

        //if new pointer position in new row
        int keysInRow = _currentNavigationMap.map[_pointer.y].keys.Count;
        if (_pointer.y != _prevPointerValue.y)
        {
            if (keysInRow < _currentNavigationMap.map[_prevPointerValue.y].keys.Count )
            {
                float attitude = ((float)keysInRow / (float)_currentNavigationMap.map[_prevPointerValue.y].keys.Count);
                _pointer.x = (int)((float)_pointer.x * attitude);
            }
          
            
        }

        //clamp and moving pointer by rows count
      
        if (_pointer.x < 0) _pointer.x = keysInRow - 1;
        if (_pointer.x > keysInRow - 1) _pointer.x = 0;



        OnChangePointer();
    }

    private void OnPress(InputAction.CallbackContext context)
    {
        ExecuteEvents.Execute(_target, new BaseEventData(_eventSystem), ExecuteEvents.submitHandler);
        Debug.Log("Pressed");
    }

    private void OnChangePointer()
    {
        _prevTarget = _currentNavigationMap.map[_prevPointerValue.y].keys[_prevPointerValue.x].gameObject;
        _target = _currentNavigationMap.map[_pointer.y].keys[_pointer.x].gameObject;

        //execute events on preview target
        ExecuteEvents.Execute(_prevTarget, new PointerEventData(_eventSystem), ExecuteEvents.pointerExitHandler);
        //execute events on current target
        ExecuteEvents.Execute(_target, new PointerEventData(_eventSystem), ExecuteEvents.pointerEnterHandler);
    }
    public void OnChangeKeyboard(GameObject newGm)
    {
        if (newGm.TryGetComponent<KeyboardNavigationMap>(out KeyboardNavigationMap newNavigationMap))
        {
            _currentNavigationMap = newNavigationMap;
            OnChangePointer();
        }
        else
        {
            Debug.Log($"new jeyboard doesn't have a {nameof(KeyboardNavigationMap)} component!");
            _currentNavigationMap = null;
        }
    }


}

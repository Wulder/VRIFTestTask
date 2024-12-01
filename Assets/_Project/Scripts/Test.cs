using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    [SerializeField] private InputActionReference _action;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_action.action.ReadValue<Vector2>());
    }
}

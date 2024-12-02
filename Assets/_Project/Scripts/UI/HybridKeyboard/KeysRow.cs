using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class KeysRow : MonoBehaviour
{
    public List<Button> keys = new List<Button>();

    private void Awake()
    {
        //SetKeysFromChilders();
    }
    public void SetKeysFromChilders()
    {
        keys.Clear();
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            keys.Add(button);
        }
    }

    
}

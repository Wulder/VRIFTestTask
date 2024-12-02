using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardNavigationMap : MonoBehaviour
{
    public List<KeysRow> map = new List<KeysRow>(); //this map describes rows and columns (buttons) in them

    public void BuildMap()
    {
        foreach (var row in map)
        {
            row.SetKeysFromChilders();
        }
    }
}



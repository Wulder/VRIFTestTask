using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class HybridVRKeyboard : VRKeyboard
{
    public List<KeyboardNavigationMap> _navigationMaps = new List<KeyboardNavigationMap>();

    
    public override void Awake()
    {
        base.Awake();
        foreach (var map in _navigationMaps)
        {
            map.BuildMap();
        }
    }
}

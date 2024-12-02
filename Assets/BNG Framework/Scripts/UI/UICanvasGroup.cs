using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace BNG {
    /// <summary>
    /// This component gives you a convenient way to group together GameObjects and toggle them on / off
    /// </summary>
    public class UICanvasGroup : MonoBehaviour {

        public UnityEvent<GameObject> OnChangeCanvas = new UnityEvent<GameObject> ();
        public List<GameObject> CanvasObjects;
        
        public void ActivateCanvas(int CanvasIndex) {
            for(int x = 0; x < CanvasObjects.Count; x++) {
                if(CanvasObjects[x] != null) {
                    CanvasObjects[x].SetActive(x == CanvasIndex);
                    if(x == CanvasIndex)
                    {
                        OnChangeCanvas.Invoke(CanvasObjects[x]);
                    }
                   
                }
            }
        }
    }
}


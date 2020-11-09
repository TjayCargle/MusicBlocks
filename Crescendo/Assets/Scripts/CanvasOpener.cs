using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasOpener : MonoBehaviour
{
    public GameObject panel;
   
    public void Toggle()
    {
        if(panel != null)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
}

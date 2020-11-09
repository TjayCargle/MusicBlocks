using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderVisualizer : MonoBehaviour
{
    public string prefix;
    public Text myText;
    public Slider myslider;
 
    // Update is called once per frame
    void Update()
    {
        if(myslider!= null)
        {
            if(myText!= null)
            {
                myText.text = prefix + " " + myslider.value;
            }
        }
    }
}

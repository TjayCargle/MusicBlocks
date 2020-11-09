using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ValueHelper : MonoBehaviour
{
    public int type;
    public Slider mySlider;
    public Dropdown myDropdown;
    public ValueHolder holder;


    // Update is called once per frame
    void Update()
    {
        if(holder != null)
        {
            switch(type)
            {
                case 0:
                    {
                        if(mySlider != null)
                        {
                            if(holder.wid != mySlider.value)
                            {
                                holder.wid = (int)mySlider.value;
                            }
                        }
                    }
                    break;

                case 1:
                    {
                        if (mySlider != null)
                        {
                            if (holder.hei != mySlider.value)
                            {
                                holder.hei = (int)mySlider.value;
                            }
                        }
                    }
                    break;

                case 2:
                    {
                        if (myDropdown != null)
                        {
                            if (holder.song != myDropdown.value)
                            {
                                holder.song = myDropdown.value;
                            }
                        }
                    }
                    break;
            }
        }
    }
}

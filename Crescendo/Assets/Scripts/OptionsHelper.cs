using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionsHelper : MonoBehaviour
{
    public Slider musicSlider;
    public Toggle colorToggle;
    public Toggle slowToggle;
    private void OnEnable()
    {
        if (musicSlider != null)
            musicSlider.value = OptionsManager.musicVolume;
        if (colorToggle != null)
            colorToggle.isOn = OptionsManager.colorBlindMode;
        if (slowToggle != null)
            slowToggle.isOn = OptionsManager.slowMode;
    }

    private void Update()
    {
        if (musicSlider != null)
        {
            if (OptionsManager.musicVolume != musicSlider.value)
            {
                OptionsManager.musicVolume = musicSlider.value;
            }

        }
        if (colorToggle != null)
        {
            if (OptionsManager.colorBlindMode != colorToggle.isOn)
            {
                OptionsManager.colorBlindMode = colorToggle.isOn ;
                OptionsManager.OnColorBlindChanged();
            }

        }
        if (slowToggle != null)
        {
            if (OptionsManager.slowMode != slowToggle.isOn)
            {
                OptionsManager.slowMode = slowToggle.isOn;
                OptionsManager.OnSlowModeChanged();
            }

        }
    }
}

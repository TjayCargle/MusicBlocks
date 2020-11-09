using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionsManager : MonoBehaviour
{
    public static float musicVolume = 80.0f;
    public static bool slowMode = false;
    public static bool colorBlindMode = false;

    public Slider musicSlider = null;

    private void Start()
    {
        if (musicSlider != null)
            musicSlider.value = musicVolume;
    }
    private void LateUpdate()
    {
        if (musicSlider != null)
        {
            if (musicVolume != musicSlider.value)
                musicVolume = musicSlider.value;
        }
    }
    public static void OnSlowModeChanged()
    {
        MoveBlock[] moves = GameObject.FindObjectsOfType<MoveBlock>();
        for (int i = 0; i < moves.Length; i++)
        {
            if (slowMode == true)
            {

                moves[i].defaultFallTime = 100.0f * Time.fixedDeltaTime;
            }
            else
            {
                moves[i].defaultFallTime = Random.Range(50.0f, 90.0f) * Time.fixedDeltaTime;
            }
        }
        BlockSpawner spawn = GameObject.FindObjectOfType<BlockSpawner>();
        if (spawn != null)
        {
            if (slowMode == true)
                spawn.defaultTimer = 180.0f * Time.fixedDeltaTime;
            else
            {
                if (PlayerPrefs.HasKey("song"))
                {
                    int song = PlayerPrefs.GetInt("song");

                    if (song == 0)
                    {
                        spawn.defaultTimer =  100.0f * Time.fixedDeltaTime;
                    }
                    else if (song == 1)
                    {
                        spawn.defaultTimer =  70.0f * Time.fixedDeltaTime;
                    }
                    else
                    {
                        spawn.defaultTimer = 50.0f * Time.fixedDeltaTime;
                    }

                }
            }

        }
    }
    public static void OnColorBlindChanged()
    {
        DragBlock[] drags = GameObject.FindObjectsOfType<DragBlock>();
        for (int i = 0; i < drags.Length; i++)
        {
            drags[i].SwitchColors();
        }

    }

}

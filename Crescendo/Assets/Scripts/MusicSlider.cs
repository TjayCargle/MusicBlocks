using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicSlider : MonoBehaviour
{
    public Slider mySlider;
    public MusicWhisperer myWhisperer;
    public bool isSetup = false;

    public void Setup()
    {
        if(!isSetup)
        {
            if (mySlider == null)
                mySlider = GetComponent<Slider>();
            if (myWhisperer == null)
                myWhisperer = GameObject.FindObjectOfType<MusicWhisperer>();
            if (mySlider != null && myWhisperer != null)
            {
                myWhisperer.Setup();
                mySlider.maxValue = myWhisperer.musicSource.clip.length;

            }
            if(puased)
            {
                ResumeGame();
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mySlider != null && myWhisperer != null)
        {
            if(myWhisperer.musicSource.isPlaying == false)
            {
                mySlider.value = mySlider.maxValue;
            }
            else
            {

            mySlider.value = myWhisperer.musicSource.time;
            }
        }
     

    }

    private float stoppedTime = 0.0f;
    bool puased = false;
    public void PauseGame()
    {
        if(puased != true)
        {
         
            BlockSpawner.gamePaused = true;
            MoveBlock.gamePaused = true;
            DragBlock.gamePaused = true;
        stoppedTime = myWhisperer.musicSource.time;
        myWhisperer.musicSource.Stop();
        puased = true;
        }
        else
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        if(puased != false)
        {


            BlockSpawner.gamePaused = false;
            MoveBlock.gamePaused = false;
            DragBlock.gamePaused = false;
            myWhisperer.musicSource.Play();
        myWhisperer.musicSource.time = stoppedTime;
        puased = false;
        }
        else
        {
          //  PauseGame();
        }
    }
}

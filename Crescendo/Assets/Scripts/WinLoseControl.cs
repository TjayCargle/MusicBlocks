using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WinLoseControl : MonoBehaviour
{
    public Button tryAgain;
    public Button mainMenu;
    public Button continueButton;
    public Text theText;
    public Image trendi;
    public Image radd;
    public Sprite[] trendiposes;
    public Sprite[] raddposes;

    private enum poses
    {
        basic,
        win,
        lose
    }
    public void UpdatePhoto()
    {
        if (PlayerPrefs.HasKey("photo"))
        {

            int index = PlayerPrefs.GetInt("photo");
            if (PlayerPrefs.HasKey("song"))
            {
                int song = PlayerPrefs.GetInt("song");
                if(song == 0)
                {
                    if(index == 0)
                    {
                        PlayerPrefs.SetInt("photo",1);
                    }
                }
                else if(song == 1)
                {
                    if(index == 1)
                    {
                        PlayerPrefs.SetInt("photo", 2);
                    }
                }
            }
            else
            {

                if (index < 2)
                    PlayerPrefs.SetInt("photo", index + 1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("photo", 1);
        }
    }
    public void ShowGameOver()
    {
        if (theText != null)
        {
            theText.text = "You Lose";
            theText.color = Color.red;
        }

        if (tryAgain != null)
        {
            tryAgain.gameObject.SetActive(true);
        }

        if (mainMenu != null)
        {
            mainMenu.gameObject.SetActive(true);
        }

        if (continueButton != null)
        {
            continueButton.gameObject.SetActive(false);
        }
        if (trendi != null)
        {
            trendi.sprite = trendiposes[(int)poses.lose];
        }

        if (radd != null)
        {
            radd.sprite = raddposes[(int)poses.win];
        }
    }

    public void ShowWinLevel()
    {
        if (theText != null)
        {
            theText.text = "You Win";
            theText.color = Color.blue;
        }
        int modeCheck = 0;
        if (PlayerPrefs.HasKey("mode"))
        {
            modeCheck = PlayerPrefs.GetInt("mode");
        }
        if (tryAgain != null)
        {
            tryAgain.gameObject.SetActive(false);
        }

        if (mainMenu != null)
        {
            if (modeCheck == 0)
                mainMenu.gameObject.SetActive(false);
            else
                mainMenu.gameObject.SetActive(true);

        }

        if (continueButton != null)
        {
            if (modeCheck == 0)
                continueButton.gameObject.SetActive(true);
            else
                continueButton.gameObject.SetActive(false);
        }

        if (trendi != null)
        {
            trendi.sprite = trendiposes[(int)poses.win];
        }

        if (radd != null)
        {
            radd.sprite = raddposes[(int)poses.lose];
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class LevelSeltion : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public int level = 1;
    public ValueHolder myHolder;
    public LevelChange change;
    public Text someText;
    public Image myImage;
    public Button someButton;
    public Animator myAnima;
    private static Color transparent = new Color(0, 0, 0, 0);
    private void Start()
    {
        myAnima = GetComponent<Animator>();
        myImage = GetComponentsInChildren<Image>()[1];
        if (myAnima != null)
        {
            myAnima.speed = 0;
        }
        if (myImage != null)
            myImage.color = transparent;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (myHolder != null)
        {
            if (change != null)
            {
                if (PlayerPrefs.HasKey("photo"))
                {
                    int checkedInt = PlayerPrefs.GetInt("photo");
                    if (level <= checkedInt + 1)
                    {

                        change.SetupDataFromLevel(myHolder);
                        if (someText != null)
                        {
                            someText.text = "Level " + level;
                        }
                        if (someButton != null)
                        {
                            someButton.gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("photo", 0);
                    if (level == 1)
                    {
                        change.SetupDataFromLevel(myHolder);
                        if (someText != null)
                        {
                            someText.text = "Level " + level;
                        }
                        if (someButton != null)
                        {
                            someButton.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
        // throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (PlayerPrefs.HasKey("photo"))
        {
            int checkedInt = PlayerPrefs.GetInt("photo");
            if (level <= checkedInt + 1)
            {
                if (myAnima != null)
                {
                    myAnima.speed = 1;
                }
                if (myImage != null)
                    myImage.color = Color.white;
            }
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (PlayerPrefs.HasKey("photo"))
        {
            int checkedInt = PlayerPrefs.GetInt("photo");
            if (level <= checkedInt + 1)
            {
                if (myAnima != null)
                {
                    myAnima.speed = 0;
                }
                if (myImage != null)
                    myImage.color = transparent;
            }
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhotoUpdater : MonoBehaviour
{
    public Image myImage;
    public Sprite[] possibleSprites;
    // Start is called before the first frame update
    void Start()
    {
        if (possibleSprites.Length >= 3)
        {
            if (myImage != null)
            {

                if (PlayerPrefs.HasKey("photo"))
                {
                    int index = PlayerPrefs.GetInt("photo");
                    if(index < possibleSprites.Length)
                    {
                        myImage.sprite = possibleSprites[index];
                    }
                }
            }
        }

    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextReader : MonoBehaviour
{
    public Image trendi;
    public Image trendishadow;
    public Image radd;
    public Image raddShadow;
    public GameObject trendibubble;
    public GameObject raddbubble;
    public Image speakerbubble;
    public Sprite[] bubblesprites;
    public LevelChange change;
    public CanvasOpener canvasToOpen;
    public CanvasOpener otherCanvas;
    private int currIndex = 0;
    public Text speakerText;
    [SerializeField]
    private TextAsset TheFile = null;
    [SerializeField]
    private List<string> theTexts = new List<string>();
    [SerializeField]
    private List<string> thePages = new List<string>();
    public AudioSource sfx;
    public AudioClip raddclip;
    public AudioClip trendiclip;


    private string currentString;
    public bool nextStage = true;
    public void LoadTextForStage()
    {
        if (PlayerPrefs.HasKey("song"))
        {
            int song = PlayerPrefs.GetInt("song");
            theTexts.Clear();
            string[] lines = null;
            if (song == 0)
            {
                lines = thePages[0].Split('|');
            }
            else if(song == 1)
            {
                lines = thePages[2].Split('|');
            }
            else
            {
                lines = thePages[4].Split('|');
            }

            if (lines != null)
            {
                for (int i = 1; i < lines.Length; i++)
                {
                    string aLine = lines[i].Replace('|', ' ').Trim();
                    theTexts.Add(aLine);
                }
            }
            currIndex = -1;
        }
    }
    public void NextOne()
    {
        if (currIndex + 1 >= theTexts.Count)
        {
            if (canvasToOpen != null)
                canvasToOpen.Toggle();
            if (change != null)
                change.LoadScene("New Scene");
        }
        else
        {
            currIndex += 1;
            if (currIndex == 0)
            {
                if (otherCanvas != null)
                {
                    otherCanvas.Toggle();
                }
            }
            string theCurrentLine = theTexts[currIndex];
            char character = theCurrentLine[0];
            theCurrentLine = theCurrentLine.Remove(0, 1);
            if (character == 't')
            {
                if(sfx != null)
                {
                    if(trendiclip != null)
                    {
                        if(sfx.clip != trendiclip)
                        {

                        sfx.Stop();
                        sfx.clip = trendiclip;
                        sfx.Play();
                        }
                    }
                }
                if (raddShadow != null)
                    raddShadow.enabled = false;

                if (trendishadow != null)
                    trendishadow.enabled = true;

                if (raddbubble != null)
                    radd.gameObject.SetActive(false);

                if (trendibubble != null)
                    trendibubble.gameObject.SetActive(true);

                if (speakerbubble != null)
                {
                    speakerbubble.sprite = bubblesprites[0];

                }
                if (speakerText != null)
                    speakerText.text = theCurrentLine.Trim();
            }
            else if (character == 'r')
            {
                if (sfx != null)
                {
                    if (raddclip != null)
                    {
                        if(sfx.clip != raddclip)
                        {
                        sfx.Stop();
                        sfx.clip = raddclip;
                        sfx.Play();
                        }
                    }
                }

                if (raddShadow != null)
                    raddShadow.enabled = true;

                if (trendishadow != null)
                    trendishadow.enabled = false;

                if (raddbubble != null)
                    radd.gameObject.SetActive(true);

                if (trendibubble != null)
                    trendibubble.gameObject.SetActive(false);

                if (speakerbubble != null)
                {
                    speakerbubble.sprite = bubblesprites[1];

                }
                if (speakerText != null)
                    speakerText.text = theCurrentLine.Trim();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (TheFile != null)
        {
            string fullText = TheFile.text.Replace('\n', ' ').Trim();
            string[] pages = fullText.Split('/');
            for (int i = 0; i < pages.Length; i++)
            {
                string line = pages[i].Trim();
                thePages.Add(line);
            }

        }
    }


}

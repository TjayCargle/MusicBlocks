using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicWhisperer : MonoBehaviour
{
    private const int SAMPLE_SIZE = 1024;

    [SerializeField]
    private float currentRMS;
    [SerializeField]
    private float currentDBData;
    public AudioClip[] clips;
   public AudioSource musicSource;
    private float[] samples;
    private float[] spectrum;
    private float samepleRate;
    public bool isSetup = false;
    public GameObject drums;
    public GameObject guitar;
    public GameObject mic;
    public float RMS
    {
        get { return currentRMS; }
    }

    public float DB
    {
        get { return currentDBData; }
    }
    [SerializeField]
    private float highestDB;

    [SerializeField]
    private float highestRMS;


    public void Setup()
    {
        if(!isSetup)
        {
            musicSource = GetComponent<AudioSource>();
            samples = new float[SAMPLE_SIZE];
            spectrum = new float[SAMPLE_SIZE];
            samepleRate = AudioSettings.outputSampleRate;
            GridManager grid = GameObject.FindObjectOfType<GridManager>();
            if(grid != null)
            {
                grid.Setup();
            }
            if(PlayerPrefs.HasKey("song"))
            {
          
                int songIndex = PlayerPrefs.GetInt("song");

                if(songIndex < clips.Length)
                {
                    musicSource.clip = clips[songIndex];
                }

                if (songIndex == 0)
                {
                    if (drums != null)
                        drums.gameObject.SetActive(true);
                    if (guitar != null)
                        guitar.gameObject.SetActive(false);
                    if (mic != null)
                        mic.gameObject.SetActive(false);
                }
                else if (songIndex == 1)
                {
                    if (drums != null)
                        drums.gameObject.SetActive(false);
                    if (guitar != null)
                        guitar.gameObject.SetActive(true);
                    if (mic != null)
                        mic.gameObject.SetActive(false);
                }
                else
                {
                    if (drums != null)
                        drums.gameObject.SetActive(false);
                    if (guitar != null)
                        guitar.gameObject.SetActive(false);
                    if (mic != null)
                        mic.gameObject.SetActive(true);
                }

            }
            musicSource.Play();
            isSetup = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Setup();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSoundData();
        if (highestDB < DB)
            highestDB = DB;
        if (highestRMS < RMS)
            highestRMS = RMS;
    }

    private void UpdateSoundData()
    {

        musicSource.GetOutputData(samples, 0);

        //Get the RMS

        int i = 0;
        float sum = 0;
        for (; i < SAMPLE_SIZE; i++)
        {
            sum += samples[i] * samples[i];
        }

        currentRMS = Mathf.Sqrt(sum / SAMPLE_SIZE);

        //get db value
        currentDBData = 20 * Mathf.Log10(currentRMS / 0.1f);

        // get sound spectrum

        musicSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
    }

}

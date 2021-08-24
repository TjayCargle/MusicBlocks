using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject[] possibleBlocks = null;
    private List<MoveBlock> availableBlocks = new List<MoveBlock>();
    [SerializeField]
    private float previousTime = 0.0f;
    [SerializeField]
    private float currentTime = 0.0f;
    [SerializeField]
    private float spawnTime = 0.05f;
    public float defaultTimer = 20.0f;
    private MusicWhisperer music = null;

    public bool gameRunning = true;
    public List<int> testData = new List<int>();
    public WinLoseControl winLose = null;
    public static bool gamePaused = false;
    public GameObject SpawnParent = null;

    public MoveBlock GetBlock(int possible = -1)
    {
        MoveBlock someBlock = null;

        for (int i = 0; i < availableBlocks.Count; i++)
        {
            if (availableBlocks[i].inUse == false)
            {
                someBlock = availableBlocks[i];
                someBlock.ResetPositions();
                someBlock.inUse = true;
            
            }
        }
        if (possible == -1)
        {
            possible = Random.Range(0, possibleBlocks.Length);
        }
        someBlock = Instantiate(possibleBlocks[possible], SpawnParent.transform).GetComponent<MoveBlock>();
        someBlock.transform.localScale = new Vector3(17, 17, 1);
        someBlock.inUse = true;
      
        availableBlocks.Add(someBlock);

        return someBlock;
    }

    public void SpawnBlock(int possible = -1)
    {
        MoveBlock spawnedBlock = GetBlock(possible);
        spawnedBlock.Setup();
        spawnedBlock.transform.position = new Vector3(0, Random.Range(4, 10), 0);
        Debug.Log(spawnedBlock.transform.position);
        int roationTimes = Random.Range(0, 4);

        for (int i = 0; i < roationTimes; i++)
        {

            spawnedBlock.Rotate();
        }
        if (spawnedBlock.TryAddToGrid() == false)
        {
         //   EndGame();
            // Debug.Log("Lost level!");
        }
    }

    public void EndGame(bool won = false)
    {
        gameRunning = false;
        if (winLose != null)
        {
            winLose.gameObject.SetActive(true);
            if (won == true)
            {
                winLose.ShowWinLevel();
            }
            else
            {
                winLose.ShowGameOver();
            }
            if (music)
            {
                music.musicSource.Stop();
            }

        }
        // Debug.Log("Lost !!!");
    }
    // Start is called before the first frame update
    void Start()
    {
        //SpawnBlock();
        if (PlayerPrefs.HasKey("song"))
        {
            int song = PlayerPrefs.GetInt("song");

            if (song == 0)
            {
                defaultTimer = 100.0f * Time.fixedDeltaTime;
            }
            else if (song == 1)
            {
                defaultTimer = 80.0f * Time.fixedDeltaTime;
            }
            else if (song > 2)
            {
                defaultTimer = (75.0f + song) * Time.fixedDeltaTime;
            }

            else
            {
                defaultTimer = 60.0f * Time.fixedDeltaTime;
            }

        }
        spawnTime = defaultTimer;// * Time.deltaTime;
        music = GameObject.FindObjectOfType<MusicWhisperer>();
    }

    public int other = 0;
    public int n5 = 0;
    public int n4 = 0;
    public int n3 = 0;
    public int n2 = 0;
    public int n1 = 0;
    public int zero = 0;
    public int z1 = 0;
    public int z2 = 0;
    public int z3 = 0;
    public int z4 = 0;
    public int z5 = 0;
    public int z6 = 0;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {

            OptionsManager.colorBlindMode = !OptionsManager.colorBlindMode;
            OptionsManager.OnColorBlindChanged();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {

            OptionsManager.slowMode = !OptionsManager.slowMode;
            OptionsManager.OnSlowModeChanged();
        }
        if (gamePaused == false)
        {

            if (gameRunning == true)
            {
                if (music)
                {
                    if (music.musicSource.isPlaying == false)
                    {
                        EndGame(true);
                        //  gameRunning = false;
                        //  Debug.Log("Win level!");
                    }
                }
                currentTime = (Time.time - previousTime);
                if (currentTime > spawnTime)
                {
                    if (music)
                    {
                        int testInt = (int)music.DB;
                        switch (testInt)
                        {
                            case -5: { SpawnBlock(2); } break;
                            case -4: { SpawnBlock(3); } break;
                            case -3: { SpawnBlock(6); } break;
                            case -2: { SpawnBlock(3); } break;
                            case -1: { SpawnBlock(6); } break;
                            case 0: { SpawnBlock(5); } break;
                            case 1: { SpawnBlock(1); } break;
                            case 2: { SpawnBlock(1); } break;
                            case 3: { SpawnBlock(4); ; } break;
                            case 4: { SpawnBlock(0); } break;
                            case 5: { SpawnBlock(4); } break;
                            case 6: { SpawnBlock(0); } break;
                            default: { SpawnBlock(); } break;
                        }
                        // if (music.DB >= -2.0f)
                        {
                            //   SpawnBlock();
                        }
                    }
                    else
                    {
                        SpawnBlock();

                    }
                    spawnTime = defaultTimer ;// Random.Range(5.0f, 15.0f);
                  
                    previousTime = Time.time;

                   
                }
            }
        }
    }
}

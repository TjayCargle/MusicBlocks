using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTime = 0.2f;
    public float fallSpeed = 1.0f;
    public float defaultFallTime = 1.0f;
    public bool beingControlled = false;
    public GridManager manager = null;
    public List<Transform> childrenTransforms = new List<Transform>();
    public Color originalColor;
    public Color colorBlindColor;
    public bool isSetup = false;
    //public static float minX = -108.7f;
    //public static float maxX = 259.0f;

    //public static float minY = -75.0f;
    //public static float maxY = 75.0f;
    public bool inUse = false;
    public int height = 19;
    public int width = 40;
    public Vector3[] relativeBlockArray;
    private Vector3 targetPosition;

    public void Setup()
    {
        float maxFall = 8.8f;

        if (PlayerPrefs.HasKey("song"))
        {
            int song = PlayerPrefs.GetInt("song");

            if (song == 0)
            {
                maxFall = 3.2f;
            }
            else if (song == 1)
            {
                maxFall = 5.5f;
            }
            else if (song > 2)
            {
                maxFall = 4.0f;
            }

        }
        fallSpeed = Random.Range(2.2f, maxFall);
        defaultFallTime = Random.Range(1.0f, 20.0f) * Time.fixedDeltaTime;//Random.Range(minDefault, 6.0f) * Time.deltaTime;
        fallTime = defaultFallTime;
        fallSpeed = 1.0f * Time.deltaTime;
        manager = GameObject.FindObjectOfType<GridManager>();
        if (manager != null)
        {
            manager.Setup();
            width = manager.width;
            height = manager.height;
        }
        else
        {
            Debug.Log("no manager");
        }
        if (OptionsManager.slowMode == true)
            defaultFallTime = 50.0f * Time.fixedDeltaTime;
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
        TryAddToGrid();
        foreach (Transform child in transform)
        {
            childrenTransforms.Add(child);
        }
        gamePaused = false;
        isSetup = true;
    }

    // Start is called before the first frame update
    void Start()
    {
  if(!isSetup)
        {
            Setup();
        }
    }
    public void RemoveFromGrid()
    {
        if (manager != null)
        {
            foreach (Transform child in transform)
            {
                int roundedX = Mathf.RoundToInt(child.transform.position.x);
                int roundedY = Mathf.RoundToInt(child.transform.position.y);

                int gridIndex = GridManager.TwoToOneD(roundedY, width, roundedX);
                if (gridIndex >= 0 && gridIndex < width * height)
                {

                    GameObject aGridObj = manager.visibleGrid[gridIndex];
                    VisibleTileScript visibleTile = aGridObj.GetComponent<VisibleTileScript>();
                    DragBlock childBlock = child.GetComponent<DragBlock>();
                    if (childBlock != null)
                    {
                        if (childBlock.TILE != null)
                            childBlock.TILE.BLOCK = null;

                    }
                }

            }
        }
    }

    public bool CanAddToGrid()
    {

        bool returnedBool = true;
        if (manager != null)
        {
            foreach (Transform child in transform)
            {
                int roundedX = Mathf.RoundToInt(child.transform.position.x) + 1;
                int roundedY = Mathf.RoundToInt(child.transform.position.y);
                int gridIndex = GridManager.TwoToOneD(roundedY, width, roundedX);
                if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
                {
                    returnedBool = false;
                    return returnedBool;
                }
                if (gridIndex >= 0 && gridIndex < width * height)
                {

                    GameObject aGridObj = manager.visibleGrid[gridIndex];
                    VisibleTileScript visibleTile = aGridObj.GetComponent<VisibleTileScript>();
                    DragBlock childBlock = child.GetComponent<DragBlock>();
                    if (childBlock != null)
                    {
                        if (visibleTile != null)
                        {
                            if (visibleTile.BLOCK == null)
                            {


                            }
                            else if (childrenTransforms.Contains(visibleTile.BLOCK.transform))
                            {

                            }
                            else
                            {
                                returnedBool = false;
                            }
                        }
                        else
                        {
                            returnedBool = false;
                            Debug.Log("Cant find visible block," + name);
                        }
                    }
                    else
                    {
                        returnedBool = false;
                        Debug.Log("Cant find child block," + name);
                    }
                }
                else
                {
                    returnedBool = false;
                    // Debug.Log("Cant find grid Index," + name);
                }


            }
        }
        return returnedBool;
    }
    public bool TryAddToGrid(int depth = 0)
    {
        if (beingControlled == true)
        {
            return false;
        }
        // RemoveFromGrid();
        bool returnedBool = true;
        if (manager != null)
        {
            foreach (Transform child in transform)
            {
                int roundedX = Mathf.RoundToInt(child.transform.position.x);
                int roundedY = Mathf.RoundToInt(child.transform.position.y);
                int gridIndex = GridManager.TwoToOneD(roundedY, width, roundedX);
                if (roundedX >= width || roundedY < 0 || roundedY >= height)
                {
                    returnedBool = false;
                }
                if (gridIndex >= 0 && gridIndex < width * height)
                {

                    GameObject aGridObj = manager.visibleGrid[gridIndex];
                    VisibleTileScript visibleTile = aGridObj.GetComponent<VisibleTileScript>();
                    DragBlock childBlock = child.GetComponent<DragBlock>();
                    if (childBlock != null)
                    {
                        if (visibleTile != null)
                        {
                            if (visibleTile.BLOCK == null)
                            {
                                if (childBlock.TILE != null)
                                {
                                    childBlock.TILE.BLOCK = null;
                                }
                                childBlock.TILE = visibleTile;
                                visibleTile.BLOCK = childBlock;
                            }
                            else if (childBlock.TILE == visibleTile || childrenTransforms.Contains(visibleTile.BLOCK.transform))
                            {

                                if (childBlock.TILE != null)
                                {
                                    childBlock.TILE.BLOCK = null;
                                }
                                childBlock.TILE = visibleTile;
                                visibleTile.BLOCK = childBlock;

                            }
                            else
                            {
                                returnedBool = false;
                            }
                        }
                        else
                        {
                            returnedBool = false;
                            Debug.Log("Cant find visible block," + name);
                        }
                    }
                    else
                    {
                        returnedBool = false;
                        Debug.Log("Cant find child block," + name);
                    }
                }
                else
                {
                    returnedBool = false;
                    //Debug.Log("Cant find grid Index," + name);
                }


            }
        }
        if (returnedBool == false)
        {
            RemoveFromGrid();
            transform.position -= Vector3.right;
            TrySnapToGrid(depth + 1);
        }
        else
        {
            //foreach (Transform child in transform)
            //{
            //    DragBlock childBlock = child.GetComponent<DragBlock>();
            //    if (childBlock != null)
            //    {
            //        if(childBlock.TILE != null)
            //        {
            //            child.transform.position = childBlock.TILE.transform.position + new Vector3(0,0,-2);
            //        }
            //    }

            //}
        }

        return returnedBool;
    }
    public static bool gamePaused = false;

    // Update is called once per frame
    void Update()
    {

        if (gamePaused == false)
        {

            if (Time.time - previousTime > fallTime)
            {
                // targetPosition = transform.position + (Vector3.right * fallSpeed);
                // if (targetPosition.x > 0 && targetPosition.x < width && targetPosition.y > 0 && targetPosition.y < height)
                if (beingControlled == false)
                {

                    if (ValidMove())
                    {
                        if (CanAddToGrid())
                        {

                            transform.position += new Vector3(1, 0, 0); //targetPosition;
                            TryAddToGrid();                                                       // transform.position -= new Vector3(1, 0, 0);//transform.position - (Vector3.left * fallSpeed);
                        }
                        else
                        {
                            // transform.position += new Vector3(-1, 0, 0);
                            // transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
                            TrySnapToGrid();
                            if (manager)
                            {
                                manager.CheckForLineClear(width, height);
                            }
                        }
                    }
                    else
                    {
                        if (manager)
                        {
                            manager.CheckForLineClear(width, height);
                        }
                    }

                }
                //transform.position += new Vector3(1, 0, 1);
                previousTime = Time.time;
            }
        }
    }

    public bool Rotate()
    {
        bool returnedBool = true;

        transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        if (!ValidMove())
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
        }

        return returnedBool;
    }

    bool ValidMove()
    {

        bool returnedBool = true;
        foreach (Transform child in transform)
        {

            int roundedX = Mathf.RoundToInt(child.transform.position.x);
            int roundedY = Mathf.RoundToInt(child.transform.position.y);

            if (roundedX < -3.5 || roundedX >= width || roundedY <= -0.5f || roundedY > height)
            {
                returnedBool = false;
            }
        }
        //if (returnedBool == true)
        //{
        //    returnedBool = TryAddToGrid();
        //    if (returnedBool == false)
        //        Debug.Log("invalid from add2grid, " + name);
        //}
        //else
        //{
        //    Debug.Log("not valid from valid, " + name);
        //}

        return returnedBool;
    }

    public void TrySnapToGrid(int depth = 0)
    {

        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
        //  if (beingControlled == false)
        {

            Vector3 positionInside = transform.position;
            if (transform.position.x < 0)
            {
                positionInside.x = 0;
                // Debug.Log("setting x to 1");
            }

            if (transform.position.x > width)
            {
                positionInside.x = width - 1;
                // Debug.Log("setting x to width");
            }

            if (transform.position.y < 0)
            {
                positionInside.y = 1;
                // Debug.Log("setting y to 1");
            }

            if (transform.position.y > height)
            {
                positionInside.y = height - 1;

                // Debug.Log("setting height ");
            }

            int moveDownCountx = 0;
            int moveDownCounty = 0;
            foreach (Transform child in transform)
            {
                int roundedX = Mathf.RoundToInt(child.transform.position.x);
                int roundedY = Mathf.RoundToInt(child.transform.position.y);
                if (roundedX >= width)
                {
                    moveDownCountx++;
                }
                if (roundedY >= height)
                {
                    moveDownCounty++;
                }

                if (roundedX < 0)
                {
                    moveDownCountx--;
                }
                if (roundedY < 0)
                {
                    moveDownCounty--;
                }
            }
            positionInside.x -= moveDownCountx;
            positionInside.y -= moveDownCounty;

            transform.position = positionInside;

            if (depth > 100)
            {
                //Debug.Log("Lose due to depth");
                beingControlled = true;
                BlockSpawner spawn = GameObject.FindObjectOfType<BlockSpawner>();
                if (spawn != null)
                {
                    beingControlled = true;
                    gamePaused = true;
                    spawn.EndGame();
                }
                return;
            }
            TryAddToGrid(depth + 1);
        }
    }

    public void ResetPositions()
    {

        if (relativeBlockArray.Length == childrenTransforms.Count)
        {
            for (int i = 0; i < childrenTransforms.Count; i++)
            {
                Transform child = childrenTransforms[i];
                child.transform.localPosition = relativeBlockArray[i];
                child.gameObject.SetActive(true);
            }
        }
    }

}





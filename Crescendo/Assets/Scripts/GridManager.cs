using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // public Transform[,] invisibleGrid = null;
    public List<GameObject> visibleGrid = new List<GameObject>();
    public GameObject block = null;
    private GridManager instance = null;
    private bool created = false;
    private GameObject inactiveParent = null;
    private List<int> blocksInColumn = new List<int>();
    public int height = 15;
    public int width = 30;
    public bool isSetup = false;
    public GameObject visableParent = null;
    public void Setup()
    {
        CreateGrid();
        isSetup = true;
    }
    public GridManager GetInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        return instance;
    }

    public void CreateGrid()
    {
        if (created == false)
        {
            if (PlayerPrefs.HasKey("gridWidth"))
            {
                width = PlayerPrefs.GetInt("gridWidth");
                Debug.Log("w = " + width);
            }

            if (PlayerPrefs.HasKey("gridHeight"))
            {
                height = PlayerPrefs.GetInt("gridHeight");
                Debug.Log("h = " + height);
            }
            //  invisibleGrid = new Transform[width, height];

            int tileCount = width * height;

            if (visableParent != null)
            {
                //visableParent = new GameObject();
            }
            inactiveParent = new GameObject();
            Vector3 startLocation = new Vector3(0.0f, 0.0f, 0.0f);
            float scaleModifer = 17.0f;
            if (block != null)
            {

                for (int i = 0; i < tileCount; i++)
                {
                    GameObject visibleBlock = Instantiate<GameObject>(block, visableParent.transform);
                    visibleBlock.name = "Visible: " + i;
                    visibleBlock.transform.localScale = transform.localScale * scaleModifer;
                    visibleGrid.Add(visibleBlock);
                }
                for (int i = 0; i < width; i++)
                {
                    blocksInColumn.Add(0);
                    for (int j = 0; j < height; j++)
                    {
                        int mapIndex = TwoToOneD(j, width, i);
                        float xLocation = startLocation.x + i + (scaleModifer * 1.5f * i) - 410;


                        float yLocation = startLocation.y + j + (scaleModifer* 1.5f * j) - 260;

                        visibleGrid[mapIndex].transform.localPosition = new Vector3(xLocation, yLocation, 1.0f);
                    }
                }
            }
            created = true;
        }
    }

    public void ShouldCheckLine(int column, bool increase)
    {

    }

    public void CheckForLineClear(int width, int height)
    {

        for (int i = width - 1; i >= 0; i--)
        {
            if (hasLine(i, width, height))
            {
                // Debug.Log("Deleting! " + i);
                deleteLine(i, width, height);
            }

        }


    }
    private bool hasLine(int i, int width, int height)
    {
        int capSize = width * height;
        for (int j = 0; j < height; j++)
        {
            int mapIndex = TwoToOneD(j, width, i);

            if (mapIndex >= 0 && mapIndex < capSize)
            {
                VisibleTileScript visible = visibleGrid[mapIndex].GetComponent<VisibleTileScript>();
                if (visible != null)
                {
                    if (visible.BLOCK == null)
                        return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Debug.Log(mapIndex);
            }
        }

        return true;
    }

    private void deleteLine(int i, int width, int height)
    {
        int capSize = width * height;
        for (int j = 0; j < height; j++)
        {
            int mapIndex = TwoToOneD(j, width, i);

            if (mapIndex >= 0 && mapIndex < capSize)
            {
                VisibleTileScript visible = visibleGrid[mapIndex].GetComponent<VisibleTileScript>();
                if (visible != null)
                {
                    if (visible.BLOCK != null && inactiveParent != null)
                    {
                        visible.BLOCK.parentMove.childrenTransforms.Remove(visible.BLOCK.transform);
                        visible.BLOCK.transform.parent = inactiveParent.transform;
                        visible.BLOCK.gameObject.SetActive(false);
                        visible.BLOCK = null;
                    }

                }

            }

        }

    }

    public static int TwoToOneD(int y, int width, int x)
    {
        return (y * width) + x;
    }

}

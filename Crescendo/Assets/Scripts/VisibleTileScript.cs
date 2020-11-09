using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleTileScript : MonoBehaviour
{
    [SerializeField]
    private DragBlock currentBlock = null;

    public int column;

    public GridManager manager = null;

    public DragBlock BLOCK
    {
        get { return currentBlock; }
        set { currentBlock = value; }
    }
}

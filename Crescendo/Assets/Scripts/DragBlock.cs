using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlock : MonoBehaviour
{
    public MoveBlock parentMove;
    Transform parentTransform;

    int clickedTimes = 0;
    float clicktime = 0.0f;
    float clickdelay = 0.6f;

    private bool triggeredReset = false;
    [SerializeField]
    private VisibleTileScript currentTile = null;
    public static bool gamePaused = false;
    private SpriteRenderer myRender = null;
    public VisibleTileScript TILE
    {
        get { return currentTile; }
        set { currentTile = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent != null)
        {
            parentTransform = transform.parent;
        }

        if (parentTransform != null)
        {
            parentMove = parentTransform.gameObject.GetComponent<MoveBlock>();
        }
        myRender = GetComponent<SpriteRenderer>();
        if (OptionsManager.colorBlindMode == true)
        {
            SwitchColors();

        }
    }

    private void OnMouseDown()
    {
        if (gamePaused == false)
        {

            clickedTimes++;
            if (parentMove != null)
            {
                if(OptionsManager.colorBlindMode == true)
                {
                    if(myRender != null)
                    {
                        myRender.color = Color.white;
                    }
                }
                parentMove.fallTime = parentMove.defaultFallTime;
                parentMove.beingControlled = true;
                parentMove.RemoveFromGrid();

                parentTransform.position = new Vector3(parentTransform.position.x, parentTransform.position.y, 2);

                if (clickedTimes > 1)
                {

                    parentMove.Rotate();
                }
            }
        }

    }
    private void OnMouseDrag()
    {
        if (gamePaused == false)
        {

            if (parentTransform != null)
            {
                if (parentMove != null)
                {
                    if (OptionsManager.colorBlindMode == true)
                    {
                        if (myRender != null)
                        {
                            myRender.color = Color.white;
                        }
                    }
                    parentMove.beingControlled = true;
                    parentMove.fallTime = parentMove.defaultFallTime;
                    triggeredReset = false;
                    clicktime = Time.time;
                    // Debug.Log(Input.mousePosition);
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.x = Mathf.RoundToInt(mousePos.x);
                    mousePos.y = Mathf.RoundToInt(mousePos.y);
                    mousePos.z = 2;
                    // Debug.Log(mousePos);
                    if (mousePos.x >= -1 && mousePos.x < parentMove.width && mousePos.y >= 0f && mousePos.y < parentMove.height)
                    {
                        parentTransform.position = mousePos;
                    }
                }
            }
        }
    }
    private void OnMouseUp()
    {
        if (gamePaused == false)
        {

            if (parentMove != null)
            {
                if (OptionsManager.colorBlindMode == true)
                {
                    if (myRender != null)
                    {
                        myRender.color = parentMove.colorBlindColor;
                    }
                }
                parentTransform.position = new Vector3(Mathf.RoundToInt(parentTransform.position.x), Mathf.RoundToInt(parentTransform.position.y), 0);
                // int loopCount = 0;

                parentMove.TrySnapToGrid();
                triggeredReset = true;
                clicktime = Time.time;
            }
        }
    }
    private void RestartMovement()
    {
        parentMove.fallTime = parentMove.defaultFallTime;
        parentMove.fallSpeed = 1.0f * Time.deltaTime;
        parentMove.beingControlled = false;
        parentMove.TryAddToGrid();
        triggeredReset = false;
    }
    public void SwitchColors()
    {
        if (myRender != null)
        {

            if (OptionsManager.colorBlindMode == true)
            {
                if (myRender.color != parentMove.colorBlindColor)
                    myRender.color = parentMove.colorBlindColor;
            }
            else
            {
                if (myRender.color != parentMove.originalColor)
                    myRender.color = parentMove.originalColor;
            }
        }
    }
    private void Update()
    {
        if (gamePaused == false)
        {


            if (Time.time - clicktime > clickdelay)
            {
                if (TILE != null)
                {
                    if (TILE.BLOCK != this)
                    {
                        if (parentMove)
                        {
                            parentMove.TryAddToGrid();
                        }
                    }
                }

                clickedTimes = 0;
                clicktime = Time.time;
                if (triggeredReset == true)
                {
                    RestartMovement();
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void NewGridCreated(Grid grid);
public class MeshLandSpawner : MonoBehaviour
{
    //Class which handles MonoBehaviour for Grid
    //This is needed only in Editor

    //==============================================//
    //------------------ Scripts -------------------//

    //----------------------------------------------//
    //------------------- Events -------------------//

    //public static event NewGridCreated newGridCreatedEvent;

    //==============================================//
    //------------------- Data ---------------------//

    [SerializeField]
    Grid currentGrid = null;

    //----------------------------------------------//
    //------------------ Variables -----------------//

    [HideInInspector]
    Vector3 centerOfGrid;
    [SerializeField]
    int gridWidth = 20;
    [SerializeField]
    int gridLength = 20;
    [SerializeField , HideInInspector]
    bool animate;
    [SerializeField , HideInInspector]
    float animateSpeed = 1f;

    //----------------------------------------------//

    [SerializeField , HideInInspector]
    private Vector2 noiseAnimVal = Vector2.zero;

    //----------------------------------------------//

    private void OnValidate()
    {
        if (gridLength <= 0) gridLength = 1;
        if (gridWidth <= 0) gridWidth = 1;
        if(animateSpeed > 2f) animateSpeed = 2f;
        else if(animateSpeed <= 0f) animateSpeed = 0.25f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SpawnGrid()
    {
        if (currentGrid != null) currentGrid.DestroyThisGrid();

        StopCoroutine(AnimateGrid());

        if (animate)
        {
            currentGrid = new Grid(gridWidth, gridLength, centerOfGrid, noiseAnimVal);
            noiseAnimVal += new Vector2(1f, 1f);
            Invoke("SpawnGrid", 1f / animateSpeed);
        }
        else
        {
            currentGrid = new Grid(gridWidth, gridLength);
        }

        PlayerPrefs.SetInt("GridWidth", gridWidth);
        PlayerPrefs.SetInt("GridLength", gridLength);

    }

    public void SpawnGrid(int gridLength , int gridWidth)
    {
        this.gridLength = gridLength;
        this.gridWidth = gridWidth;

        SpawnGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Not working
    IEnumerator AnimateGrid()
    {
        while (animate)
        {
            SpawnGrid();

            yield return new WaitForSeconds(1f / animateSpeed);
        }
    }
}




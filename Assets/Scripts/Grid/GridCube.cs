using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Grid Cube Class

//GridCubeFace orientation
public class GridCube
{
    GameObject gridCubeObj;
    //Only to keep things organised
    GridCubeFaceIDToObj cubeFaceToObj;
    //CubeNoise, read more in NoiseSampler class
    GridCubeNoise gridCubeNoise;

    MeshRenderer gridCubeRenderer;
    MeshFilter gridCubeMeshFilter;

    int xPos, yPos;

    //Class Constructor. creates new Obj, and assign components
    public GridCube(int xPos, int yPos)
    {
        cubeFaceToObj = new GridCubeFaceIDToObj();

        this.xPos = xPos;
        this.yPos = yPos;

        gridCubeObj = new GameObject("GridCube/X:" + xPos + "/Y:" + yPos + "/", GridMasterManager.GridSettings.GridCubeInitializingComponents);
        gridCubeRenderer = gridCubeObj.GetComponent<MeshRenderer>();
        gridCubeMeshFilter = gridCubeObj.GetComponent<MeshFilter>();
        gridCubeRenderer.sharedMaterial = GridMasterManager.GridSettings.GridCubeInitializeData.GridFaceIDMaterial[GridCubeFaceID.UP];
        gridCubeObj.isStatic = true;
        //InitializeGridCubeFaces();
    }

    //This method is run when all GridCube class objs are initializes and constructed
    //After that, for each face new obj is created.
    //Parameter is GridCubeNoise which is yOffset for top vertex of cube
    public void InitializeGridCubeFaces(GridCubeNoise gridCubeNoise)
    {
        cubeFaceToObj.Add(GridCubeFaceID.DOWN, GridCubeMeshHandler.GenerateGridCubeFace(GridCubeFaceID.DOWN, gridCubeObj, gridCubeNoise));
        cubeFaceToObj.Add(GridCubeFaceID.LEFT, GridCubeMeshHandler.GenerateGridCubeFace(GridCubeFaceID.LEFT, gridCubeObj, gridCubeNoise));
        cubeFaceToObj.Add(GridCubeFaceID.RIGHT, GridCubeMeshHandler.GenerateGridCubeFace(GridCubeFaceID.RIGHT, gridCubeObj, gridCubeNoise));
        cubeFaceToObj.Add(GridCubeFaceID.FORWARD, GridCubeMeshHandler.GenerateGridCubeFace(GridCubeFaceID.FORWARD, gridCubeObj, gridCubeNoise));
        cubeFaceToObj.Add(GridCubeFaceID.BACKWARD, GridCubeMeshHandler.GenerateGridCubeFace(GridCubeFaceID.BACKWARD, gridCubeObj, gridCubeNoise));
        cubeFaceToObj.Add(GridCubeFaceID.UP, GridCubeMeshHandler.GenerateGridCubeFace(GridCubeFaceID.UP, gridCubeObj, gridCubeNoise));

        CombineGridCubeFaceMeshes();

    }

    private void CombineGridCubeFaceMeshes()
    {
        if (GridMasterManager.GridSettings.GridPreferences.SeperateState == GridSeperateState.PerCubeFace) return;

        GridCubeMeshHandler.CombineGridCubeFaceMeshes(gridCubeObj, cubeFaceToObj);

    }
    

    //Not used anymore
    public void InitializeGridCubeNoise(GridCubeNoise gridCubeNoise)
    {
        cubeFaceToObj.Add(GridCubeFaceID.UP, GridCubeMeshHandler.GenerateGridCubeTopFaceObj(gridCubeNoise , gridCubeObj));
    }

    #region Updating Grid Properties

    private void UpdateGridCubeProperties(GameObject gridCubeObj)
    {
        this.gridCubeObj = gridCubeObj;
    }

    public void SetGridCubeObjPos(Vector3 pos)
    {
        this.gridCubeObj.transform.position = pos;
    }

    public void SetNewParent(Transform parent)
    {
        gridCubeObj.transform.parent = parent;
    }

    #endregion

}

public class GridCubeFaceData
{
    GridCubeFaceID faceID;
    Material material;

    public GridCubeFaceData(GridCubeFaceID faceID, Material material)
    {
        this.faceID = faceID;
        this.material = material;
    }

}

#endregion

using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Grid
{
    //EventHandler for grid
    GridEventHandler gridEventHandler;

    //-----------------------------------//

    GameObject gridParentObject;
    GridCubeNoise[,] gridcubeNoises;

    //-----------------------------------//

    Vector3 center = Vector3.zero;
    int width;
    int length;

    float[,] noiseArray;
    Vector2 noiseAnimVal;

    /// <summary>
    /// 
    /// </summary>
    /// 

    GridCube[,] gridCubes;

    #region Grid Initialization

    public Grid(int width, int length)
    {
        this.width = width;
        this.length = length;

        StartGridGeneration();
        ApplyGridStartingSettings();
    }

    //Center is not in use
    public Grid(int width, int length, Vector3 center)
    {
        this.width = width;
        this.length = length;
        this.center = center;

        StartGridGeneration();
        ApplyGridStartingSettings();
    }

    //Constructor with NoiseOffset
    public Grid(int width, int length, Vector3 center , Vector2 noiseAnimVal)
    {
        this.width = width;
        this.length = length;
        this.center = center;

        StartGridGeneration();
        ApplyGridStartingSettings();
    }

    #endregion

    //New eventHandler is created when new Grid is spawned
    private void ApplyGridStartingSettings()
    {
        gridEventHandler = new GridEventHandler();

        PlayerEventHandler.RegisterToMouseClicked(gridEventHandler.MouseInteractionOnGridStarted);
        PlayerEventHandler.RegisterToMouseUnClicked(gridEventHandler.MouseInteractionOnGridEnded);

        gridEventHandler.NewGridCreatedInvoke(this.center, this.width, this.length);
    }

    //Grid generation
    //All variables, paramaters, face initialization, material assigning etc... is starting from here
    private void StartGridGeneration()
    {
        gridParentObject = new GameObject("Grid");
        gridParentObject.transform.position = center;

        noiseArray = NoiseSampler.ReturnNoiseArray(this.width, this.length);

        gridCubes = GridCubeSpawner.ReturnGridCubes(noiseArray);

        GridCubeNoise[,] gridCubeNoises = NoiseSampler.ReturnGridCubeNoise(this.width, this.length);

        //GridCubes faces initialization starting from here
        for (int i = 0; i < this.width; i++)
        {
            for (int j = 0; j < this.length; j++)
            {
                gridCubes[i, j].InitializeGridCubeFaces(gridCubeNoises[i, j]);
                //gridCubes[i, j].InitializeGridCubeNoise(gridCubeNoises[i, j]);
            }
        }

        GridTransformHandler.ApplyGridCubesTransformSettings(gridCubes, gridParentObject.transform);
        GridTransformHandler.PositionGridCubes(gridCubes, center);

        HandleGridMeshCombining();

        gridParentObject.isStatic = true;
    }

    //Based on User Editor preferences mesh combining is possible from here
    //User has 3 options PerFace,PerCube,PerGrid which changes Batches very much xd
    private void HandleGridMeshCombining()
    {
        if (GridMasterManager.GridSettings.GridPreferences.SeperateState == GridSeperateState.PerGrid)
        {
            MeshFilter[] meshFilters = gridParentObject.GetComponentsInChildren<MeshFilter>();
            List<CombineInstance> combineInstances = new List<CombineInstance>();

            for (int i = 0; i < meshFilters.Length; i++)
            {
                CombineInstance combineInstance = new CombineInstance();
                combineInstance.mesh = meshFilters[i].mesh;
                combineInstance.transform = meshFilters[i].transform.localToWorldMatrix;

                combineInstances.Add(combineInstance);
                meshFilters[i].gameObject.SetActive(false);
            }

            MeshRenderer topMeshRenderer;
            MeshFilter topMeshFilter;

            if (gridParentObject.GetComponent<MeshFilter>())
            {
                topMeshFilter = gridParentObject.GetComponent<MeshFilter>();
            }
            else
            {
                topMeshFilter = gridParentObject.AddComponent<MeshFilter>();
            }

            if (gridParentObject.GetComponent<MeshRenderer>())
            {
                topMeshRenderer = gridParentObject.GetComponent<MeshRenderer>();
            }
            else
            {
                topMeshRenderer = gridParentObject.AddComponent<MeshRenderer>();
            }

            topMeshRenderer.material = GridMasterManager.GridSettings.GridCubeInitializeData.GridFaceIDMaterial[GridCubeFaceID.UP];

            topMeshFilter.mesh = new Mesh();
            topMeshFilter.mesh.CombineMeshes(combineInstances.ToArray());
        }
    }

    public void DestroyThisGrid()
    {
        PlayerEventHandler.UnRegisterToMouseClicked(gridEventHandler.MouseInteractionOnGridStarted);
        PlayerEventHandler.UnRegisterToMouseUnClicked(gridEventHandler.MouseInteractionOnGridEnded);

        GameObject.Destroy(gridParentObject);
    }

    //Below is relocated to gridEventHandler
    /*

    public void MouseInteractionOnGridStarted(MouseButton mouseButton)
    {
        if (mouseButton != MouseButton.Left) return;
        if (!MouseHelper.IsPointerOverUIElement("GridCameraView")) return;

        Timing.RunCoroutine(ApplyGridRotation() , Segment.Update , "GridRotation");

    }

    public void MouseInteractionOnGridEnded(MouseButton mouseButton)
    {
        if (mouseButton != MouseButton.Left) return;

        Timing.KillCoroutines("GridRotation");

    }

    IEnumerator<float> ApplyGridRotation()
    {
        Vector2 lastMousePos = Input.mousePosition;

        while (true)
        {
            Vector2 currMousePos = Input.mousePosition;

            float mouseXDistance = lastMousePos.x - currMousePos.x;
            float yRotationToApply = mouseXDistance * 0.25f;

            gridParentObject.transform.rotation *= Quaternion.Euler(0f, yRotationToApply, 0f);

            lastMousePos = Input.mousePosition;

            yield return Timing.WaitForOneFrame;
        }

    }

    */

}

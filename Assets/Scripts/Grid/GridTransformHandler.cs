using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTransformHandler
{
    //==========================================================================//

    //Static gridTransform, top object for Grid
    static Transform gridTransform;

    //==========================================================================//

    //Positioning gridCubes top objs, after when they are created
    //Center is applied, but not in use currently
    public static void PositionGridCubes(GridCube[,] gridCubes , Vector3 center)
    {
        int gridWidth = gridCubes.GetLength(0);
        int gridLength = gridCubes.GetLength(1);

        float gridCubeXDim = GridMasterManager.GridSettings.GridCubeDimensions.x;
        float gridCubeYDim = GridMasterManager.GridSettings.GridCubeDimensions.y;
        float gridCubeZDim = GridMasterManager.GridSettings.GridCubeDimensions.z;

        //Debug.Log("Grid Cube X Dim: " + gridCubeXDim);
        //Debug.Log("Grid Cube Y Dim: " + gridCubeYDim);
        //Debug.Log("Grid Cube Z Dim: " + gridCubeZDim);

        float gridStartXPos = center.x - ((gridWidth / 2f) * gridCubeXDim ) + gridCubeXDim / 2f;
        float gridStartYPos = 0f;
        float gridStartZPos = center.z - ((gridLength / 2f) * gridCubeZDim ) + gridCubeZDim / 2f;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridLength; y++)
            {
                float gridXPos = gridStartXPos + x * gridCubeXDim;
                float gridYPos = gridStartYPos * gridCubeYDim;
                float gridZPos = gridStartZPos + y * gridCubeZDim;

                gridCubes[x, y].SetGridCubeObjPos(new Vector3(gridXPos, gridYPos, gridZPos));
            }
        }

    }

    //Applying parent to freshly created CubeTransforms
    public static void ApplyGridCubesTransformSettings(GridCube[,] gridCubes , Transform parent)
    {
        for (int x = 0; x < gridCubes.GetLength(0); x++)
        {
            for (int y = 0; y < gridCubes.GetLength(1); y++)
            {
                gridCubes[x, y].SetNewParent(parent);
            }
        }

        gridTransform = parent;
    }

    //Method which handles GridRotation after when mouse is pressed
    public static IEnumerator<float> ApplyGridRotation()
    {
        Vector2 lastMousePos = Input.mousePosition;

        while (true)
        {
            Vector2 currMousePos = Input.mousePosition;

            float mouseXDistance = lastMousePos.x - currMousePos.x;
            float yRotationToApply = mouseXDistance * 0.25f;

            gridTransform.rotation *= Quaternion.Euler(0f, yRotationToApply, 0f);

            lastMousePos = Input.mousePosition;

            yield return Timing.WaitForOneFrame;
        }

    }

}

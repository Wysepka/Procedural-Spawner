using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    Vector3 cameraAppliedPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Registering to new grid creation ( when SpawnGrid button is clicked )
    private void OnEnable()
    {
        GridEventHandler.RegisterToNewGridCreation(ApplyNewCameraSettings);
    }

    //Unregistering from grid creation, when Application is quit
    private void OnDisable()
    {
        GridEventHandler.UnregisterToNewGridCreation(ApplyNewCameraSettings);
    }

    //Applying Camera settings, in this method new position based on grid length and width
    public void ApplyNewCameraSettings(Vector3 gridCentre , float gridWidth , float gridLength)
    {
        //Static GridCube Axis dimensions
        float cubeXDim = GridMasterManager.GridSettings.GridCubeDimensions.x;
        float cubeYDim = GridMasterManager.GridSettings.GridCubeDimensions.y;
        float cubeZDim = GridMasterManager.GridSettings.GridCubeDimensions.z;

        //CubeFace axis multiplier, if x is longer, then x is applied here, otherwise z
        float cubeLongerFaceMultiplier = 1f;

        if (cubeXDim > cubeZDim) cubeLongerFaceMultiplier = cubeXDim;
        else cubeLongerFaceMultiplier = cubeZDim;

        //Grid longer side multiplier
        float higherGridValue = 0f;

        if (gridWidth > gridLength) higherGridValue = gridWidth;
        else higherGridValue = gridLength;

        //Some math in which approporiate offset is added to camera pos
        //This is needed for Camera to see the whole grid
        Vector3 newCameraPos = gridCentre;
        float halfLengthDim = (higherGridValue * cubeLongerFaceMultiplier) / 2f;
        float widthToLengthDiff = Mathf.Abs(gridWidth - gridLength);
        float additionalZOffset = 1 / (widthToLengthDiff + 1);

        newCameraPos.z -= (halfLengthDim + additionalZOffset )* 1.75f;
        newCameraPos.y += halfLengthDim * 1.5f;

        cameraAppliedPos = newCameraPos;

        //Rotation applied to camera, after position is set, to be centred at the middle of the grid
        this.transform.position = newCameraPos;
        Vector3 toGridCentre = gridCentre - this.transform.position;
        this.transform.rotation = Quaternion.LookRotation(toGridCentre);
        //this.transform.rotation = Quaternion.AngleAxis(45f, Vector3.right);
        //this.transform.rotation = Quaternion.AngleAxis(45f, Vector3.right);
    }

}

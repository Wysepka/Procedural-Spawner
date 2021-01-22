using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEventHandler
{
    static System.Action<Vector3 , float , float> NewGridCreated;

    //Such an action is needed when new Grid is created to reset Grid Axis dimensions
    public GridEventHandler()
    {
        GridMasterManager.GridSettings.ChangeGridCubeDimension(CubeDimension.X, 1f);
        GridMasterManager.GridSettings.ChangeGridCubeDimension(CubeDimension.Y, 1f);
        GridMasterManager.GridSettings.ChangeGridCubeDimension(CubeDimension.Z, 1f);
    }

    public static bool RegisterToNewGridCreation(System.Action<Vector3 , float , float> action)
    {
        NewGridCreated += action;

        return true;
    }

    public static bool UnregisterToNewGridCreation(System.Action<Vector3 , float , float> action)
    {
        NewGridCreated -= action;

        return true;
    }

    public void NewGridCreatedInvoke(Vector3 centre , float width , float length)
    {
        if(NewGridCreated != null) NewGridCreated.Invoke(centre, width, length);
    }


    #region Mouse Interaction on Grid

    public void MouseInteractionOnGridStarted(MouseButton mouseButton)
    {
        if (mouseButton != MouseButton.Left) return;
        if (!MouseHelper.IsPointerOverUIElement("GridCameraView")) return;

        Timing.RunCoroutine(GridTransformHandler.ApplyGridRotation(), Segment.Update, "GridRotation");

    }

    public void MouseInteractionOnGridEnded(MouseButton mouseButton)
    {
        if (mouseButton != MouseButton.Left) return;

        Timing.KillCoroutines("GridRotation");

    }

    #endregion

}

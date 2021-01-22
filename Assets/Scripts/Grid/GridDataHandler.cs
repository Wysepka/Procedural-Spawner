using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDataHandler : MonoBehaviour
{
    
}

//Data which handles all Face Material Settings
//For customization purposes such a feature is possible
//If User want to have different Material, Color, etc... for each face, it is possible

[System.Serializable]
public class GridCubeInitializeData
{
    [SerializeField]
    GridFaceIDMaterial gridFaceIDMaterial;

    public GridCubeInitializeData()
    {
        gridFaceIDMaterial = new GridFaceIDMaterial();
    }

    //-------------------------------------------------------------------------------//

    public void ChangeMaterialBaseColor(Color newColor)
    {
        if (gridFaceIDMaterial == null || gridFaceIDMaterial.Count == 0) InitializeDictionary();
        foreach (KeyValuePair<GridCubeFaceID , Material> item in gridFaceIDMaterial)
        {
            item.Value.color = newColor;
            item.Value.SetColor("_BaseColor", newColor);
        }
    }

    public void InitializeDictionary()
    {
        gridFaceIDMaterial = new GridFaceIDMaterial();

        for (int i = 0; i < GridMasterManager.GridSettings.GridCubeFaceIDs.Length; i++)
        {
            gridFaceIDMaterial.Add(GridMasterManager.GridSettings.GridCubeFaceIDs[i], GridMasterManager.GridSettings.GridDefaultMaterial);
        }
    }

    //-------------------------------------------------------------------------------//
    //---------------------------------- Accesors -----------------------------------//

    public GridFaceIDMaterial GridFaceIDMaterial
    {
        get
        {
            if (gridFaceIDMaterial == null || gridFaceIDMaterial.Count == 0) InitializeDictionary();
            return gridFaceIDMaterial;
        }
    }
}

//GridCube seperateness options ( made for optimisation reasons ) 

[System.Serializable]
public class GridPreferences
{
    [SerializeField]
    GridSeperateState seperateState;

    public GridSeperateState SeperateState { get { return seperateState; } }
}




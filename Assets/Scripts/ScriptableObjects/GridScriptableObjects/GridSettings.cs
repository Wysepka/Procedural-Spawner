using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Grid/GridSettings"))]
public class GridSettings : ScriptableObject
{
    #region Editor Changable

    [SerializeField]
    Vector3 gridCubeDimensions = Vector3.one;

    [SerializeField]
    float gridNoiseScaler = 1f;

    [SerializeField]
    GridCubeInitializeData gridCubeInitializeData;

    #endregion

    #region User Changable

    [SerializeField]
    Vector3 gridCubeDimensions_u = Vector3.one;

    [SerializeField]
    float gridNoiseScaler_u = 1f;

    [SerializeField]
    List<Color> gridColors;

    [SerializeField]
    GridCubeInitializeData gridCubeInitializeData_u;

    [SerializeField]
    Material defaultMaterial;

    #endregion

    System.Type[] gridCubeInitializingComponents = new System.Type[2]
    {
        typeof(MeshRenderer),
        typeof(MeshFilter)
    };

    //Not in use, testing purposes
    [SerializeField]
    Dictionary<MeshBodyType, MeshMorhping.MeshMorphData> keyValuePairs;

    [SerializeField]
    GridPreferences gridPreferences;

    #region Colors Predefined

    //Not a single working UI Color Picker in Asset store

    static Color[] gridCubeColorsPredefined = new Color[7]
    {
        Color.black,
        Color.blue,
        Color.green,
        Color.magenta,
        Color.red,
        Color.white,
        Color.yellow,
    };

    static string[] gridCubeColorsNamePredefined = new string[7]
    {
        "Black",
        "Blue",
        "Green",
        "Magenta",
        "Red",
        "White",
        "Yellow"
    };

    #endregion

    Dictionary<string, Color> colorNameToVariable;

    //Array with all GridCubeFaces enum values
    static GridCubeFaceID[] gridCubeFaceIDs = new GridCubeFaceID[]
    {
        GridCubeFaceID.DOWN,
        GridCubeFaceID.BACKWARD,
        GridCubeFaceID.FORWARD,
        GridCubeFaceID.LEFT,
        GridCubeFaceID.RIGHT,
        GridCubeFaceID.UP
    };

    //=============================================================//

    #region Setters

    //Region in which new Data is set from UI

    public void ChangeGridCubeDimension(CubeDimension dimension , float newVal)
    {
        if (gridCubeDimensions_u == Vector3.zero) gridCubeDimensions_u = Vector3.one;
        if(dimension == CubeDimension.X) { gridCubeDimensions_u.x = newVal; }
        else if(dimension == CubeDimension.Y) { gridCubeDimensions_u.y = newVal; }
        else if(dimension == CubeDimension.Z) { gridCubeDimensions_u.z = newVal; }
    }

    public void ChangeGridNoiseScaler(float gridNoiseScaler)
    {
        this.gridNoiseScaler_u = gridNoiseScaler;
    }

    //Changing GridCubeColor, when GridCubeData is null ( when Color was previously not changed new Class is instantiated
    public void ChangeGridCubeColor(Color newColor)
    {
        Debug.Log("Changing Grid Cube Color");

        if(gridCubeInitializeData_u == null || gridCubeInitializeData_u.GridFaceIDMaterial == null
            || gridCubeInitializeData_u.GridFaceIDMaterial.Count == 0)
        {
            gridCubeInitializeData_u = new GridCubeInitializeData();
        }

        gridCubeInitializeData_u.ChangeMaterialBaseColor(newColor);
    }

    #endregion

    //=============================================================//    

    #region Accesors

    public Vector3 GridCubeDimensions
    {
        get
        {
            if (gridCubeDimensions_u == Vector3.zero) return gridCubeDimensions;
            else return gridCubeDimensions_u;
        }
    }

    public GridCubeInitializeData GridCubeInitializeData
    {
        get
        {
            return gridCubeInitializeData_u;

            //return gridCubeInitializeData;
        }
    }

    public System.Type[] GridCubeInitializingComponents { get { return gridCubeInitializingComponents; } }

    public GridPreferences GridPreferences { get { return gridPreferences; } }

    public float GridNoiseScaler { get { return gridNoiseScaler_u; } }

    public List<Color> GridColors { get { return gridColors; } }

    public GridCubeFaceID[] GridCubeFaceIDs { get { return gridCubeFaceIDs; } }

    public Color[] GridCubeColorsPredefined { get { return gridCubeColorsPredefined; } }

    public string[] GridCubeColorsNamePredefined { get { return gridCubeColorsNamePredefined; } }

    public Dictionary<string , Color> ColorNameToVariable
    {
        get
        {
            if(colorNameToVariable == null || colorNameToVariable.Count == 0)
            {
                colorNameToVariable = new Dictionary<string, Color>();

                for (int i = 0; i < gridCubeColorsNamePredefined.Length; i++)
                {
                    colorNameToVariable.Add(gridCubeColorsNamePredefined[i], gridCubeColorsPredefined[i]);
                }
            }

            return colorNameToVariable;
        }
    }

    public Material GridDefaultMaterial { get { return defaultMaterial; } }

    #endregion



}

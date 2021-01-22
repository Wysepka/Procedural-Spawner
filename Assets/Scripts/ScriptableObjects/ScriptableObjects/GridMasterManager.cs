using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MasterManager for singleton, from here there is acces to further Data

[CreateAssetMenu(menuName = ("Managers/GridMasterManager"))]
public class GridMasterManager : ScriptableObjectSingleton<GridMasterManager>
{
    [SerializeField]
    private GridSettings _gridSettings;

    public static GridSettings GridSettings { get { return Instance._gridSettings; } }
}

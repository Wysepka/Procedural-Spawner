using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIVariablesHandler : MonoBehaviour
{

    [SerializeField]
    Slider gridLengthSlider;

    [SerializeField]
    Slider gridWidthSlider;

    [SerializeField]
    Slider gridNoiseScalerSlider;

    [SerializeField]
    Slider gridCubeXDimSlider, gridCubeYDimSlider, gridCubeZDimSlider;

    [SerializeField]
    Dropdown gridColorPickerDropdown;

    // Start is called before the first frame update
    void Start()
    {
        //ConfigureColorDropdown();
        ConfigureColorDropdownPredefined();
    }

    //Not in use
    void ConfigureColorDropdown()
    {
        string optionName = "Option";
        Color[] gridColors = GridMasterManager.GridSettings.GridColors.ToArray();
        gridColorPickerDropdown.ClearOptions();
        List<Dropdown.OptionData> optionDatasList = new List<Dropdown.OptionData>();

        for (int i = 0; i < gridColors.Length; i++)
        {
            Sprite gridColorSprite = ColorToTextureHelper.ColorToSpriteConverter(gridColors[i]);

            Dropdown.OptionData optionData = new Dropdown.OptionData(optionName + " " + i, gridColorSprite);
            optionDatasList.Add(optionData);
        }

        gridColorPickerDropdown.AddOptions(optionDatasList);

    }

    //Calculating Dropdown values, based on GridSettings, predefined Array structures
    void ConfigureColorDropdownPredefined()
    {
        gridColorPickerDropdown.ClearOptions();
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();

        foreach (KeyValuePair<string, Color> item in GridMasterManager.GridSettings.ColorNameToVariable)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData(item.Key);

            optionDatas.Add(optionData);
        }

        gridColorPickerDropdown.AddOptions(optionDatas);
    }

    //===================================================================//

    # region On UI Variables Value Changed

    void OnGridLengthValueChanged()
    {

    }

    void OnGridWidthValueChanged()
    {

    }

    //When any of UI Gameobject value is changed by user such a value is send further to GridSettings and saved

    public void OnGridNoiseScaleValueChanged()
    {
        float newNoiseScalerValue = gridNoiseScalerSlider.value;

        GridMasterManager.GridSettings.ChangeGridNoiseScaler(newNoiseScalerValue);
    }

    public void OnGridCubeXDimensionsValueChanged()
    {
        float newVal = gridCubeXDimSlider.value;
        GridMasterManager.GridSettings.ChangeGridCubeDimension(CubeDimension.X, newVal);
    }

    public void OnGridCubeYDimensionsValueChanged()
    {
        float newVal = gridCubeYDimSlider.value;
        GridMasterManager.GridSettings.ChangeGridCubeDimension(CubeDimension.Y, newVal);
    }

    public void OnGridCubeZDimensionsValueChanged()
    {
        float newVal = gridCubeZDimSlider.value;
        GridMasterManager.GridSettings.ChangeGridCubeDimension(CubeDimension.Z, newVal);
    }

    public void OnCubeColorDropdownValueChanged(Dropdown dropdown)
    {
        Debug.Log("Changing grid color from canvas");

        string colorName = GridMasterManager.GridSettings.GridCubeColorsNamePredefined[dropdown.value];
        Color newColor = GridMasterManager.GridSettings.ColorNameToVariable[colorName];
        GridMasterManager.GridSettings.ChangeGridCubeColor(newColor);
    }


    public void SpawnGridButton()
    {
        float lengthVal = gridLengthSlider.value;
        float widthVal = gridWidthSlider.value;

        FindObjectOfType<MeshLandSpawner>().SpawnGrid((int)lengthVal , (int) widthVal);

    }

    #endregion

    //===================================================================//


}

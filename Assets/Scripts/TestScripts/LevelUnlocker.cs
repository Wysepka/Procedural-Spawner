using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour
{

    //Przyciski z których startuje się poziom
    
    [SerializeField , Tooltip("Ustawione Hierarchicznie - 0 miejsce, odpowiada 1 poziomowi")]
    Button[] levelButtons;

    public static string currentLevelProgressPrefName = "LevelProgress";

    // Start is called before the first frame update
    void Start()
    {
        InitializeLevelProgressPref();
    }

    private static void InitializeLevelProgressPref()
    {
        if (PlayerPrefs.HasKey(currentLevelProgressPrefName) == false)
        {
            PlayerPrefs.SetInt(currentLevelProgressPrefName, 1);
        }
    }

    private void OnEnable()
    {

        InitializeLevelProgressPref();

        int currentLevelProgress = PlayerPrefs.GetInt(currentLevelProgressPrefName);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (currentLevelProgress >= i)
            {
                levelButtons[i].interactable = false;
            }
            else levelButtons[i].interactable = true;
        }

    }





}

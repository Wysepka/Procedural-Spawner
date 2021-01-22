using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skrypt na każdej scenie, jeżeli poziom jest sukcesywny, aktualizuje save


public class NextLevelUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        CurrentLevelSucceded();
    }

    public void CurrentLevelSucceded()
    {
        if (PlayerPrefs.HasKey(LevelUnlocker.currentLevelProgressPrefName))
        {

            int currentLevelProgress = PlayerPrefs.GetInt(LevelUnlocker.currentLevelProgressPrefName);
            PlayerPrefs.SetInt(LevelUnlocker.currentLevelProgressPrefName, currentLevelProgress + 1);

        }
    }

}

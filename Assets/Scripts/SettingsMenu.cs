using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{

    Resolution[] resolutions;

    public TMP_Dropdown resolutionDropdown;

    public TMP_Dropdown qualityDropdown;

    public Toggle isFullscreen;

    void Start()
    {
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        isFullscreen.isOn = Screen.fullScreen;
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int index = 0;
        int savedIndex = 0;

        foreach(Resolution r in resolutions)
        {
            options.Add(r.width + " x " + r.height);
            if (r.width == Screen.width && r.height == Screen.height)
            {
                savedIndex = index;
            }
            index++;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = savedIndex;
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen);
    }
}

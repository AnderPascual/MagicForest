using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown resolutionDropDown;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
        }

        resolutionDropDown.AddOptions(options);
    }

    public void SetVolume (float volume)
  {
    audioMixer.SetFloat("volume", volume);
  }

  public void SetQuality (int qualityIndex)
  {
    QualitySettings.SetQualityLevel(qualityIndex);
  }

  public void SetFullscreen (bool isFullScreen)  
  {
    Screen.fullScreen = isFullScreen;
  }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;
public class MenuController : MonoBehaviour
{
    /* 
     * TODO: Possibly add saving system so settings don't have to be changed every game launch(not sure how it works yet)
     * UNKNOWN: When exiting from the build and rerunning the same build, if set at a higher resolution than the player screen the game automatically
     * lowers to the native. However, if set at a lower resolution than exit/rerun the lower resolution stays. I am leaving this for now, as it 
     * might be fixed when a saving system (like the one from GameDev.tv) is implemented
     */

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Dropdown resolutionDropdown;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject networkMenu;

    Resolution[] resolutions;

    private void Start()
    {
        SetupResolutionDropdown();
        // activate necassary menus
        mainMenu.SetActive(true);
        if(networkMenu != null)
        {
            networkMenu.SetActive(true);
        }       
        optionsMenu.SetActive(false);       
    }

    private void SetupResolutionDropdown()
    {
        // Gets each individual computers resoltions
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();

        resolutionDropdown.ClearOptions();

        // Add resolutions dynamically
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // checks which dropdown is users native screen resolution
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void SetVolume(float volume)
    {
        AkSoundEngine.SetRTPCValue("MasterVolume", volume);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void MainMenu()
    {
        optionsMenu.SetActive(false);
        if (networkMenu != null)
        {
            networkMenu.SetActive(true);
        }
        mainMenu.SetActive(true);       
    }
    public void OptionsMenu()
    {
        mainMenu.SetActive(false);
        if (networkMenu != null)
        {
            networkMenu.SetActive(false);
        }
        optionsMenu.SetActive(true);
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using Mirror;

public class PauseMenuController : MonoBehaviour
{
    /* TODO: Possibly add saving system so settings don't have to be changed every game launch(not sure how it works yet)
     * TODO: Add option to return to main menu
     * NOTE: currently since this is an online game, I have no incorporated any stopping of controls or time or anything
     */

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Dropdown resolutionDropdown;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionsMenu;

    Resolution[] resolutions;

    private void Start()
    {
        SetupResolutionDropdown();
        // deactivate necassary menus
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
        }
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

    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
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
    public void OnResume()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }
    public void PauseMenu()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);       
    }
    public void OptionsMenu()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void ExitToMainMenu()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }
        else
        {
            NetworkManager.singleton.StopClient();
        }
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}

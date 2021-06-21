using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;
using Mirror;
using Steamworks;

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
    [SerializeField] GameObject landingPagePanel;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] private bool useSteam = false;

    Resolution[] resolutions;

    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;

    private void Start()
    {
        // Set up necassary menus
        SetupResolutionDropdown();
        optionsMenu.SetActive(false);
        landingPagePanel.SetActive(true);

        if (!useSteam) { return; }
        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }

    public void HostLobby()
    {
        landingPagePanel.SetActive(false);
        if (useSteam)
        {
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, 4);
            return;
        }
        NetworkManager.singleton.StartHost(); 
    }
    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK)
        {
            landingPagePanel.SetActive(true);
            return;
        }

        NetworkManager.singleton.StartHost();

        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "HostAddress", SteamUser.GetSteamID().ToString());
    }
    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        if (NetworkServer.active) { return; }

        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "HostAddress");

        NetworkManager.singleton.networkAddress = hostAddress;
        NetworkManager.singleton.StartClient();

        landingPagePanel.SetActive(false);
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

        landingPagePanel.SetActive(true);       
    }
    public void OptionsMenu()
    {
        landingPagePanel.SetActive(false);

        optionsMenu.SetActive(true);
    }
}

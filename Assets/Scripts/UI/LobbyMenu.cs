using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mirror;

public class LobbyMenu : MonoBehaviour
{
    [SerializeField] private GameObject lobbyUI = null;
    [SerializeField] private Button startGameButton = null;
    [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[4];
    private void Start()
    {
        MyNetworkManager.ClientOnConnected += HandleClientConnected;
        MyPlayer.AuthorityOnPartyOwnerStateUpdated += AuthorityHandlePartyOwnerStateUpdated;
        MyPlayer.ClientOnInfoUpdated += ClientHandleInfoUpdated;
    }

    private void OnDestroy()
    {
        MyNetworkManager.ClientOnConnected -= HandleClientConnected;
        MyPlayer.AuthorityOnPartyOwnerStateUpdated -= AuthorityHandlePartyOwnerStateUpdated;
        MyPlayer.ClientOnInfoUpdated -= ClientHandleInfoUpdated;
    }
   
    public void ClientHandleInfoUpdated()
    { 
        List<MyPlayer> players = ((MyNetworkManager)NetworkManager.singleton).Players;
        for(int i = 0; i < players.Count; i++)
        {
            Debug.Log("Adding Players");
            playerNameTexts[i].text = players[i].GetDisplayName();
        }

        for(int i = players.Count; i < playerNameTexts.Length; i++)
        {
            Debug.Log("Waiting adding");
            playerNameTexts[i].text = "Waiting For Player...";
        }

        startGameButton.interactable = players.Count >= 2;
    }

    private void AuthorityHandlePartyOwnerStateUpdated(bool state)
    {
        startGameButton.gameObject.SetActive(state);
    }
    private void HandleClientConnected()
    {
        lobbyUI.SetActive(true);
    }
    public void StartGame()
    {
        NetworkClient.connection.identity.GetComponent<MyPlayer>().CmdStartGame();
    }
    public void LeaveLobby()
    {
        if(NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }
        else
        {
            NetworkManager.singleton.StopClient();
            SceneManager.LoadScene(0);
        }
    }
}

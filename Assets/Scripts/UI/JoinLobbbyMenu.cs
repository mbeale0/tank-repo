using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mirror;

public class JoinLobbbyMenu : MonoBehaviour
{
    [SerializeField] private GameObject landingPanel = null;
    [SerializeField] private TMP_InputField addressInput = null;
    [SerializeField] private Button joinButton = null;
    [SerializeField] private GameObject vehicleSelection = null;

    private void OnEnable()
    {
        MyNetworkManager.ClientOnConnected += HandleClientConnected;
        MyNetworkManager.ClientOnDisconnected += HandleClientDisconnected;
    }

    private void OnDisable()
    {
        MyNetworkManager.ClientOnConnected -= HandleClientConnected;
        MyNetworkManager.ClientOnDisconnected -= HandleClientDisconnected;
    }
    public void join()
    {
        string address = addressInput.text;

        NetworkManager.singleton.networkAddress = address;
        NetworkManager.singleton.StartClient();
        joinButton.interactable = false;
    }

    private void HandleClientConnected()
    {
        joinButton.interactable = true;
        gameObject.SetActive(false);
        landingPanel.SetActive(false);
        vehicleSelection.SetActive(true);
    }
    private void HandleClientDisconnected()
    {
        joinButton.interactable = true;
    }
}

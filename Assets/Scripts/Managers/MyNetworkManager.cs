using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    public List<MyPlayer> Players { get; } = new List<MyPlayer>();

    public static event Action ClientOnConnected;
    public static event Action ClientOnDisconnected;

    private bool isGameStarted = false;
    private GameObject playerVehicle = null;
    private NetworkConnectionToClient playerSender = null;
    public void SetStartVehicle(GameObject playerInstance, NetworkConnectionToClient sender)
    {
        playerVehicle = playerInstance;
        playerSender = sender;
    }
    

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (!isGameStarted) { return; }
        conn.Disconnect();
    }
    public override void OnServerDisconnect(NetworkConnection conn)
    {
        MyPlayer player = conn.identity.GetComponent<MyPlayer>();

        Players.Remove(player);

        base.OnServerDisconnect(conn);
    }
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        MyPlayer player = conn.identity.GetComponent<MyPlayer>();
        Players.Add(player);

        player.SetDisplayName($"Player {Players.Count}");
        
        player.SetTeamColor(new Color
        (
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f)
        ));

        player.SetPartyOwner(Players.Count == 1);
    }
  
    public override void OnClientNotReady(NetworkConnection conn)
    {
        base.OnClientNotReady(conn);
        NetworkClient.ready = true;
    }
    public void StartGame()
    {
        if (Players.Count < 2) { return; }
        isGameStarted = true;
        // this is used instead of offline/online scene from network manager
        ServerChangeScene("Scene_Map01");
    }

    public override void OnServerChangeScene(string newSceneName)
    {
        GameObject playerVehicleInstance = (playerVehicle); 
        NetworkServer.Spawn(playerVehicleInstance, playerSender);
    }
    public override void OnStopServer()
    {
        Players.Clear();
        isGameStarted = false;
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        ClientOnConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        ClientOnDisconnected?.Invoke();
    }

    
}

using System.Collections;
using System.Collections.Generic;
using Managers;
using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    public List<MyPlayer> Players { get; } = new List<MyPlayer>();
    
    
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        MyPlayer player = conn.identity.GetComponent<MyPlayer>();
        Players.Add(player);
      
        player.SetTeamColor(new Color
        (
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f)
        ));
    }
}

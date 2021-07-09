using Managers;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSoldiers : NetworkBehaviour
{
    [SerializeField] private GameObject baseSoldier = null;
    [SerializeField] private int numOfSoldiers = 1;

    private bool hasSpawnedEnemies = false;
    private void Update()
    {
        if (!isClientOnly) { return; }
        if (!hasSpawnedEnemies)
        {
            hasSpawnedEnemies = true;
            spawnEnemies();
        }
    }
    private void spawnEnemies()
    {
       
        CmdSpawnEnemies();
    }
    [Command(requiresAuthority =false)]
    public void CmdSpawnEnemies( NetworkConnectionToClient sender = null)
    {
        GameObject enemyInstance = Instantiate(baseSoldier);
        NetworkServer.Spawn(enemyInstance, sender);
    }
}

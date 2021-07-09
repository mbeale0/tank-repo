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
    private void Start()
    {
        StartCoroutine(spawnEnemiesAfterStart());
    }
    IEnumerator spawnEnemiesAfterStart()
    {
        yield return new WaitForSeconds(2);
        CmdSpawnEnemies();
    }
    [Command(requiresAuthority = false)]
    public void CmdSpawnEnemies( NetworkConnectionToClient sender = null)
    {
        hasSpawnedEnemies = true;
        for(int i = 0; i < numOfSoldiers; i++)
        {
            GameObject enemyInstance = Instantiate(baseSoldier);
            NetworkServer.Spawn(enemyInstance, sender);
        }
        
    }
}

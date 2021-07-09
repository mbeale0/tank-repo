using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class SpawnBase : NetworkBehaviour
{
    [SerializeField] GameObject basePrefab;
    [SerializeField] Transform baseSpawn;
    private bool baseExists = false;
    // Start is called before the first frame update
    void Start()
    {
        if (baseExists) return;
        if (SceneManager.GetActiveScene().name.StartsWith("Scene") && hasAuthority)
        {
            CmdBase();
            baseExists = true;
        }
    }

    [Command/*(requiresAuthority = false)*/]
    public void CmdBase(NetworkConnectionToClient sender = null)
    {
       
        GameObject projectile = Instantiate(basePrefab, baseSpawn.position, baseSpawn.rotation);
        NetworkServer.Spawn(projectile, sender);

    }
}

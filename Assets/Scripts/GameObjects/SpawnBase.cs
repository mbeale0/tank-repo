using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnBase : NetworkBehaviour
{
    [SerializeField] GameObject basePrefab;
    [SerializeField] Transform baseSpawn;
    // Start is called before the first frame update
    void Start()
    {
        CmdBase();
    }

    [Command]
    public void CmdBase()
    {
       
        GameObject projectile = Instantiate(basePrefab, baseSpawn.position, baseSpawn.rotation);
        NetworkServer.Spawn(projectile);

    }
}

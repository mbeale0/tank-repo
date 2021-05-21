using UnityEngine;
using UnityEngine.AI;
using Mirror;

namespace Complete
{

    public class TurretController : NetworkBehaviour
    {

        [Header("Firing")]
        public KeyCode shootKey = KeyCode.Space;
        public GameObject projectilePrefab;
        public GameObject turret;
        public Transform projectileMount;


        
        void FixedUpdate()
        {
           // if (!hasAuthority) return;
            CmdRotate();

        }

        private void Update()
        {    
           // if (!hasAuthority) return;
            if (Input.GetKeyDown(shootKey))
            {
                if (!isLocalPlayer) return;
                CmdFire();
            }

        }

        [Command]
        void CmdFire()
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileMount.position, projectileMount.rotation);
            NetworkServer.Spawn(projectile);
        }
        public void CmdRotate()
        {
            if (!isLocalPlayer) return;
            if (Input.GetKey("e"))
            {
                turret.transform.Rotate(new Vector3(0f, 1f, 0f));
            }
            if (Input.GetKey("q"))
            {
                turret.transform.Rotate(new Vector3(0f, -1f, 0f));
            }
        }

    
    }
}


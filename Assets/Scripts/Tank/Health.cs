using System;
using System.Collections;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
 
namespace Tank
{
    public class Health : NetworkBehaviour
    {
        [SyncVar(hook = nameof(SetHealthHook))] public int currentHealth = 100;

        [SerializeField] private int maxHealth = 100;
        [SerializeField] private GameObject remainsPrefab;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private GameObject mainCameraPrefab = null;
        [SerializeField] private GameObject vehicleSelectionPrefab = null;
        [SyncVar] public bool isDead = false;


        NetworkConnection cachedNetworkConnection;
        public static event Action OnHealthUpdated;

        public override void OnStartServer()
        {
            cachedNetworkConnection = connectionToClient;
        }

        public override void OnStartClient()
        {
            healthSlider.value = currentHealth;
        }

        private void Start()
        {
            
            if (!hasAuthority) { return; }
            healthSlider.gameObject.SetActive(true);
        }
        void SetHealthHook(int oldHealth, int newHealth)
        {
            healthSlider.value = currentHealth;
        }
        [ServerCallback]
        void SpawnRemains()
        {
            // if this is a player object, RemovePlayerForConnection to take it off of clients
            // for buildings, UnSpawn it to take it off clients
            /*if (connectionToClient != null)
                NetworkServer.RemovePlayerForConnection(connectionToClient, true);;*/

            // spawn the remains prefab for this object in either case
            NetworkServer.Spawn(Instantiate(remainsPrefab, transform.position, transform.rotation));
        }

        [ServerCallback]
        public void DealDamage(int damageAmount)
        {
            currentHealth = Mathf.Max(currentHealth - damageAmount, 0);
            if (currentHealth==0 && gameObject.tag == "Building")
            {
                StartCoroutine(Respawn());
            }
            else if (currentHealth==0)
            {
                StartCoroutine(Respawn());
                NetworkIdentity thisObject = GetComponent<NetworkIdentity>();
                FindObjectOfType<VehicleViewer>().TargetEnableVehicleViewer(thisObject.connectionToClient, true);
                //OnHealthUpdated?.Invoke();
            }
        }

        [ServerCallback]
        IEnumerator Respawn()
        {
            SpawnRemains();
            if (gameObject.tag == "Building")
            {
                Destroy(gameObject);
            }
                /*CmdVehicleViewer();
                RpcVehicleViewer();*/
                //Instantiate(vehicleSelectionPrefab);

                //NetworkServer.Spawn(vehicleSelectionInstance, connectionToClient);
                GetComponent<PlayerCameraMounting>().DismountCamera();

            NetworkServer.Destroy(gameObject);
            

            yield return new WaitForEndOfFrame();

        }
        /*[Command]
        void CmdVehicleViewer()
        {
            if (hasAuthority)
            {
                vehicleViewer = FindObjectOfType<VehicleViewer>().vehicleViewer;
                vehicleViewer.SetActive(true);
            }
        
        }
        [ClientRpc]
        void RpcVehicleViewer()
        {
            if (hasAuthority)
            {
                vehicleViewer = FindObjectOfType<VehicleViewer>().vehicleViewer;
                vehicleViewer.SetActive(true);
            }
        }*/
    }
}
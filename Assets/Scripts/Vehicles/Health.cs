using System;
using System.Collections;
using Managers;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace Tank
{
    public class Health : NetworkBehaviour
    {
        [SerializeField] private GameObject remainsPrefab;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private GameObject[] healthCanvasObjects = null;
        [SyncVar] public bool isDead = false;

        [SyncVar(hook = nameof(SetHealthHook))] public int currentHealth = 100;

        NetworkConnection cachedNetworkConnection;

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

            int lives = connectionToClient.identity.GetComponent<MyPlayer>().GetLives();
            foreach (GameObject heart in healthCanvasObjects)
            {
                if(lives > 0)
                {
                    heart.SetActive(true);
                }
                lives--;
            }
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
            if (currentHealth == 0 && gameObject.tag == "Building")
            {
                StartCoroutine(Respawn());
            }
            else if (currentHealth == 0)
            {
                StartCoroutine(Respawn());
                NetworkIdentity thisObject = GetComponent<NetworkIdentity>();

                MyPlayer player = connectionToClient.identity.GetComponent<MyPlayer>();
                player.ReduceLives();
                if (player.GetLives() == 0) { return; }
                FindObjectOfType<VehicleViewer>().TargetEnableVehicleViewer(thisObject.connectionToClient, true);
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
            GetComponent<PlayerCameraMounting>().DismountCamera();

            NetworkServer.Destroy(gameObject);


            yield return new WaitForEndOfFrame();
        }
    }

}
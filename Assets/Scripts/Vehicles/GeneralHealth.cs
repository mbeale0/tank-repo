using System;
using System.Collections;
using Managers;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace Vehicles
{
    public class GeneralHealth : NetworkBehaviour
    {
        [SerializeField] private GameObject remainsPrefab;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Image[] healthCanvasImages = null;
        [SerializeField] private GameObject baseSoldier = null;
        [SerializeField] private int numOfSoldiers = 1;

        [SyncVar] public bool isDead = false;
        [SyncVar(hook = nameof(SetHealthHook))] public float currentHealth = 100;

        private float maxHealth;
        private float healthAmount;

        NetworkConnection cachedNetworkConnection;


        public override void OnStartServer()
        {
            cachedNetworkConnection = connectionToClient;
        }

        public override void OnStartClient()
        {
            healthSlider.value = currentHealth;
            // ensures max health is set after health is set in inspector
            maxHealth = currentHealth;
        }

        public void Start()
        {
            if (!hasAuthority) { return; }
            healthSlider.gameObject.SetActive(true);
            
            // Not needed if not vehicle health script
            /*int lives = NetworkClient.localPlayer.GetComponent<MyPlayer>().GetLives();
            foreach (Image heart in healthCanvasImages)
            {

                if (lives > 0)
                {
                    heart.gameObject.SetActive(true);
                }
                lives--;
            }*/
        }
        public void SetHealthHook(float oldHealth, float newHealth)
        {
            float healthPercent = (currentHealth / maxHealth);
            healthSlider.value = healthPercent * 100;
        }

        [ServerCallback]
        public void SpawnRemains()
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
            if (currentHealth <= 0 && gameObject.tag == "Building")
            {
                StartCoroutine(Respawn());
            }
            // not needed if not vehicle health script
            /*else if (currentHealth <= 0)
            {
                NetworkIdentity thisObject = GetComponent<NetworkIdentity>();
                
                MyPlayer player = NetworkClient.localPlayer.GetComponent<MyPlayer>();
                player.ReduceLives();
                if (player.GetLives() == 0) { return; }
                
                FindObjectOfType<VehicleViewer>().TargetEnableVehicleViewer(thisObject.connectionToClient, true);
                StartCoroutine(Respawn());
            }*/
        }
        [ServerCallback]
        public IEnumerator Respawn()
        {
            SpawnRemains();
            if (gameObject.tag == "Building")
            {
                Destroy(gameObject);
            }
            // if ONLY buildins and vehicles hace health this ensures that only players run this, but it is not great option
            if(gameObject.tag != "Building" && gameObject.tag != "Enemy")
            {
                GetComponent<PlayerCameraMounting>().DismountCamera();
            }
            

            NetworkServer.Destroy(gameObject);


            yield return new WaitForEndOfFrame();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Health")
            {
                healthAmount = other.gameObject.GetComponent<HealthPickup>().healthAmount;
                CmdAddHealth();
                Destroy(other.gameObject);
            }
        }

        public void AddHealth()
        {
            currentHealth += healthAmount;
            if (currentHealth > 100)
            {
                currentHealth = 100f;
            }
        }
        [Command]
        public void CmdAddHealth()
        {
            AddHealth();
        }
    }
}
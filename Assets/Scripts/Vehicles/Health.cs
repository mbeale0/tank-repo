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
        [SerializeField] private Image[] healthCanvasImages = null;
        [SyncVar] public bool isDead = false;

        [SyncVar(hook = nameof(SetHealthHook))] public float currentHealth = 100;

        private float maxHealth;

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

        private void Start()
        {
            if (!hasAuthority) { return; }
            healthSlider.gameObject.SetActive(true);
            

            int lives = NetworkClient.localPlayer.GetComponent<MyPlayer>().GetLives();
            foreach (Image heart in healthCanvasImages)
            {

                if (lives > 0)
                {
                    heart.gameObject.SetActive(true);
                }
                lives--;
            }
        }
        void SetHealthHook(float oldHealth, float newHealth)
        {
            float healthPercent = (currentHealth / maxHealth);
            healthSlider.value = healthPercent * 100;
        }
        private void ReduceLocalPlayerLives()
        {
            Debug.Log("start");
            NetworkClient.localPlayer.GetComponent<MyPlayer>().ReduceLives();
            Debug.Log("end");
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


        public void DealDamage(int damageAmount)
        {
            Debug.Log("first");
            currentHealth = Mathf.Max(currentHealth - damageAmount, 0);
            if (currentHealth <= 0 && gameObject.tag == "Building")
            {
                Debug.Log("second");
                StartCoroutine(Respawn());
            }
            else if (currentHealth <= 0)
            {
                Debug.Log("third");
                ReduceLocalPlayerLives();
                //if (player.GetLives() == 0) { return; }
                NetworkIdentity thisObject = GetComponent<NetworkIdentity>();
                FindObjectOfType<VehicleViewer>().TargetEnableVehicleViewer(thisObject.connectionToClient, true);
                StartCoroutine(Respawn());
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
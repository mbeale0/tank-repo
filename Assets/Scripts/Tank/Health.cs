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
        public bool isDead = false;
 
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
            if (connectionToClient != null)
                NetworkServer.RemovePlayerForConnection(connectionToClient, false);;
 
            // spawn the remains prefab for this object in either case
            NetworkServer.Spawn(Instantiate(remainsPrefab, transform.position, transform.rotation));
        }
 
        [ServerCallback]
        public void DealDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
          
            if (currentHealth == 0)
                StartCoroutine(Respawn());
        }
 
        [ServerCallback]
        IEnumerator Respawn()
        {
            // this will nullify connectionToClient so use cachedNetworkConnection after this
            SpawnRemains();
            if (gameObject.tag == "Building")
            {
                Destroy(gameObject);
            }

            // bail out here for non-player objects, e.g. buildings
            if (cachedNetworkConnection == null) 
                yield break;

            GameObject characterSelect = FindObjectOfType<VehicleViewer>().gameObject;

            int childCount = characterSelect.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = characterSelect.transform.GetChild(i);
                child.gameObject.SetActive(true);
            }

            yield return new WaitForEndOfFrame(); 

            //Destroy(gameObject);

            ServerHandlePlayerDie();
            Instantiate(mainCameraPrefab);
            /*ReLocate();
            currentHealth = maxHealth;
            yield return new WaitForSeconds(2f);
 
            NetworkServer.AddPlayerForConnection(cachedNetworkConnection, gameObject);*/
        }
 
        [ServerCallback]
        void ReLocate()
        {
            // leaving this in assuming more complex relocation is planned
            transform.position = Vector3.zero;
        }
        [Server]
        private void ServerHandlePlayerDie()
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}
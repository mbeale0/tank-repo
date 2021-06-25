﻿using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
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
        //private GameObject myPlayerObject;


        NetworkConnection cachedNetworkConnection;

        public override void OnStartServer()
        {
            cachedNetworkConnection = connectionToClient;
            /*if (isLocalPlayer)
            {
                if (hasAuthority)
                {
                    myPlayerObject = GetComponent<MyPlayer>().gameObject;
                }  
            }*/
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
            currentHealth -= damageAmount;

            if (currentHealth == 0)
                StartCoroutine(Respawn());
        }

        [ServerCallback]
        IEnumerator Respawn()
        {
            SpawnRemains();

            GameObject player = connectionToClient.identity.GetComponent<MyPlayer>().gameObject;
            int childCount = player.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = player.transform.GetChild(i);
                child.gameObject.SetActive(true);
            }
            
            // muust be called after so the script can continue to run
            NetworkServer.Destroy(gameObject);

            yield return new WaitForEndOfFrame();

        }

    }
}
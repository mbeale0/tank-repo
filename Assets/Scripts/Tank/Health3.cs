using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
 
namespace Tank
{
    public class Health3 : NetworkBehaviour
    {
        [SerializeField] int currentHealth = 100;
        [SyncVar] [SerializeField] private int maxHealth = 100;
        [SerializeField] private GameObject bustedTankPrefab;
        [SerializeField] private Transform tankTransform;
        [SerializeField] private GameObject tankRenderers;
        [SyncVar] public bool isDead = false;
 
        public int Health
        {
          get
          {
            return currentHealth;
          }
          set
          {
            SetSyncVar(value,ref currentHealth,1UL);
          }
        }
 
        public int MaxHealth
        {
          get
          {
            return maxHealth;
          }
          set
          {
            SetSyncVar(value,ref maxHealth,1UL);
          }
        }
        
        public override void OnStartServer()
        {
            Health = maxHealth;
        }
 
        [Command]
        void CmdSpawnBustedTank()
        {
            if(!isDead) return;
            GameObject tankBusted = Instantiate(bustedTankPrefab, tankTransform.position, tankTransform.rotation);
            NetworkServer.Spawn(tankBusted);
        }
       
        [Server]
        public void DealDamage(int damageAmount)
        {
            Health -= damageAmount;
        }
 
        private void Update()
        {
            if (isLocalPlayer) 
            {
                if (currentHealth <= 0 && !isDead)
                {
                    StartCoroutine(Respawn());
                }
            }
        }
        
        
        IEnumerator Respawn()
        {
            SetSyncVar(true,ref isDead, 1UL);
            CmdSpawnBustedTank();
            yield return new WaitForEndOfFrame();
            Health = maxHealth;
            tankRenderers.SetActive(false);
            CmdRespawn();
            yield return new WaitForSeconds(2f);
            tankRenderers.SetActive(true);
            SetSyncVar(false,ref isDead, 1UL);
        }
         private void CmdRespawn()
                {
                    if (isLocalPlayer)
                    {
                        if(isDead)
                        { 
                          transform.position = Vector3.zero;
                        }
                    }
                }
    }
}
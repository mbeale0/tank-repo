using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace Tank
{
    public class Health : NetworkBehaviour
    {
        [SyncVar(hook = "SetHealthHook")] public int currentHealth = 100;

      
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private GameObject bustedTankPrefab;
        [SerializeField] private Transform tankTransform;
        [SerializeField] private GameObject tankRenderers;
        [SerializeField] private Slider healthSlider;
        public bool isDead = false;
        
        public override void OnStartServer()
        {
            currentHealth = maxHealth;
            healthSlider.value = currentHealth;
        }

        private void SetHealthHook(int oldHealth, int newHealth)
        {
            SetHealth();
        }
        private void SetHealth()
        {
            healthSlider.value = currentHealth;
        }

        [Command]
        void CmdSpawnBustedTank()
        {
            GameObject tankBusted = Instantiate(bustedTankPrefab, tankTransform.position, tankTransform.rotation);
            NetworkServer.Spawn(tankBusted);
        }

        public void DealDamage(int damageAmount)
        {
            currentHealth -= damageAmount;

        }

        private void Update()
        {
            if (hasAuthority)
            {
                SetHealth();
            }

            if (currentHealth == 0)
            {
              StartCoroutine(Respawn());
            }
            else
            {
               return;
            }
        }

        
        IEnumerator Respawn()
        {
            CmdSpawnBustedTank();
            yield return new WaitForEndOfFrame();
            currentHealth = maxHealth;
            tankRenderers.SetActive(false);
            CmdRespawn();
            yield return new WaitForSeconds(2f);
            tankRenderers.SetActive(true);
        }
         private void CmdRespawn()
                {
                    if (isLocalPlayer)
                    {
                        transform.position = Vector3.zero;
                    }
                }

    }

}
using System;
using System.Collections;
using Mirror;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace GameObjects
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int currentHealth = 100;
        [SerializeField] private GameObject baseRuinsPrefab;
        [SerializeField] private Transform baseTransform;
        [SerializeField] private GameObject baseRenderers;


        private void Start()
        {
            currentHealth = maxHealth;
        }

        void SpawnRuins()
        {
            GameObject baseRuins = Instantiate(baseRuinsPrefab, baseTransform.position, baseTransform.rotation);
          //  NetworkServer.Spawn(baseRuins);
        }
       

        public void DealDamage(int damageAmount)
        {
            currentHealth -= damageAmount;

        }

        private void OnTriggerEnter(Collider other)
        {
            if (gameObject.CompareTag("Projectile"))
            {
                var damageAmount = GetComponent<Projectile>().damageAmount;
                DealDamage(damageAmount);
            }
        }

        private void Update()
        {
         if (currentHealth == 0)
         {
             StartCoroutine(BaseDestruction());
         }
         else
         {
             return;
         }
        }
        IEnumerator BaseDestruction()
        {
            SpawnRuins();
            yield return new WaitForEndOfFrame();
            baseRenderers.SetActive(false);
        }
 
    }
}

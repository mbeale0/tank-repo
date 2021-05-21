using System.Collections;
using Mirror;
using Tank;
using UnityEngine;

namespace GameObjects
{
    public class Projectile : NetworkBehaviour
    {
        [SerializeField] private int damageAmount = 25;
        public float destroyAfter = 5;
        public Rigidbody rigidBody;
        public float force = 1000;
        [SerializeField] private ParticleSystem vehicleExplosionSfx;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void OnStartServer()
        {
            Invoke(nameof(DestroySelf), destroyAfter);
        }

        // set velocity for server and client. this way we don't have to sync the
        // position, because both the server and the client simulate it.
        public void Start()
        {
            rigidBody.AddForce(transform.forward * force);
        }

       
        // destroy for everyone on the server
        [Server]
        public void DestroySelf()
        {
            NetworkServer.Destroy(gameObject);
        }

        // ServerCallback because we don't want a warning if OnTriggerEnter is
        // called on the client
        [ServerCallback]
        private void OnTriggerEnter(Collider co)
        {

            if (co.TryGetComponent<NetworkIdentity>(out var networkIdentity))
            {
                if (networkIdentity.connectionToClient == connectionToClient) { return; }
            }

            if (!co.TryGetComponent<Health>(out var health)) return;
            health.DealDamage(damageAmount);
            NetworkServer.Destroy(gameObject);

        }

        IEnumerator VechicleHit()
        {
            yield return new WaitForSeconds(10f);
            NetworkServer.Destroy(gameObject);



        }
    }
}

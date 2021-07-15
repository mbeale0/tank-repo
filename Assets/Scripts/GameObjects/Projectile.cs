using Mirror;
using Vehicles;
using UnityEngine;

namespace GameObjects
{
    public class Projectile : NetworkBehaviour
    {
        public int damageAmount = 25;
        public float destroyAfter = 5;
        public Rigidbody rigidBody;
        public float force = 1000;
       // [SerializeField] private ParticleSystem vehicleExplosionSfx;

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
            // I do not like my method of checking all healths but it should work, feels repetitive
            if (co.TryGetComponent<GeneralHealth>(out var health))
            {
                if (CheckTeamColors(co)) { return; }
                health.DealDamage(damageAmount);
            }
            else if (co.TryGetComponent<TankHealth>(out var tankHealth))
            {
                if (CheckTeamColors(co)) { return; }
                tankHealth.DealDamage(damageAmount);
            }
            else if (co.TryGetComponent<JeepHealth>(out var jeepHealth))
            {
                if (CheckTeamColors(co)) { return; }
                jeepHealth.DealDamage(damageAmount);
            }
            else if (co.TryGetComponent<AATankHealth>(out var aAHealth))
            {
                if (CheckTeamColors(co)) { return; }
                aAHealth.DealDamage(damageAmount);
            }
            else if (co.TryGetComponent<HeliHealth>(out var heliHealth))
            {
                if (CheckTeamColors(co)) { return; }
                heliHealth.DealDamage(damageAmount);
            }
            NetworkServer.Destroy(gameObject);

            /*if (co.TryGetComponent<NetworkIdentity>(out var networkIdentity))
            {
                if (networkIdentity.connectionToClient == connectionToClient) { return; }
            }*/


        }

        /*IEnumerator VechicleHit()
        {
            yield return new WaitForSeconds(10f);
            NetworkServer.Destroy(gameObject);
        }*/
        private bool CheckTeamColors(Collider possibleTarget)
        {
            return GetComponent<TeamColorSetter>().GetTeamColor() == possibleTarget.GetComponent<TeamColorSetter>().GetTeamColor();
        }
    }
}

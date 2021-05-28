using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GatlingGun : NetworkBehaviour
{
    // target the gun will aim at
    Transform go_target;

    // Gameobjects need to control rotation and aiming
    public Transform go_baseRotation;
    public Transform go_GunBody;
    public Transform go_barrel;

    [SerializeField] AudioClip shotFiringClip;

    [SerializeField] private float shotFiringVolume = 1f;
    AudioSource audioSource;
    // Gun barrel rotation
    public float barrelRotationSpeed;
    float currentRotationSpeed;

    // Distance the turret can aim and fire from
    public float firingRange;

    // Particle system for the muzzel flash
    public ParticleSystem muzzelFlash;

    // Used to start and stop the turret firing
    bool canFire = false;
    private bool canFireProjectile = true;
    
    // projectile
    public GameObject bulletPrefab;
    public Transform bulletMount;
    public float bulletsPerSecond = 1f;

    
    void Start()
    {
        // Set the firing range distance
        this.GetComponent<SphereCollider>().radius = firingRange;
    }

    void Update()
    {
        AimAndFire();
    }

    void OnDrawGizmosSelected()
    {
        // Draw a red sphere at the transform's position to show the firing range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, firingRange);
    }

    // Detect an Enemy, aim and fire
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            go_target = other.transform;
            canFire = true;
        }

    }
    // Stop firing
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canFire = false;
        }
    }

    void AimAndFire()
    {
        // Gun barrel rotation
        go_barrel.transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);

        // if can fire turret activates
        if (canFire)
        {
            // start rotation
            currentRotationSpeed = barrelRotationSpeed;

            // aim at enemy
            Vector3 baseTargetPostition = new Vector3(go_target.position.x, this.transform.position.y, go_target.position.z);
            Vector3 gunBodyTargetPostition = new Vector3(go_target.position.x, go_target.position.y, go_target.position.z);

            go_baseRotation.transform.LookAt(baseTargetPostition);
            go_GunBody.transform.LookAt(gunBodyTargetPostition);
            // instantiate projectiles
            
            // start particle system 
            if (!muzzelFlash.isPlaying)
            {
                muzzelFlash.Play();
                audioSource.PlayOneShot(shotFiringClip, shotFiringVolume);
            }
            
            // shoot bullets
            if(canFireProjectile){
            StartCoroutine(BulletsPerSecond());}

        }
        else
        {
            // slow down barrel rotation and stop
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, 0, 10 * Time.deltaTime);

            // stop the particle system
            if (muzzelFlash.isPlaying)
            {
                muzzelFlash.Stop();
            }
        }
      
    }

    IEnumerator BulletsPerSecond()
    {
        FireProjectile();
        yield return new WaitForEndOfFrame();
        canFireProjectile = false;
        yield return new WaitForSeconds(bulletsPerSecond);
        canFireProjectile = true;
    }

    [ServerCallback]
    public void FireProjectile()
    {
       
        GameObject bullet = Instantiate(bulletPrefab, bulletMount.position, bulletMount.rotation);
        NetworkServer.Spawn(bullet);
    }
}
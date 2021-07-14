using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;

public class Firing :NetworkBehaviour
{
    
    [Header("Firing")]
    public KeyCode shootKey = KeyCode.Space;
    public GameObject projectilePrefab;
    public Transform projectileMount;
    public Slider timerSlider;
    [SerializeField] AudioClip shotFiringClip;
    [SerializeField] float shotFiringVolume =1f;
    [SerializeField] private float cannonRechargeTime = 1f;
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private TMP_Text ammoText = null;

    private int ammoAmount;

    
    private int currentAmmo;
    private bool canShoot = true;
     
    private void Start()
    {
        if (!hasAuthority) { return; }
        timerSlider.maxValue = cannonRechargeTime;
        timerSlider.value = cannonRechargeTime;
        timerSlider.gameObject.SetActive(true);
        currentAmmo = maxAmmo;
        ammoText.text = $"Ammo: {currentAmmo}/{maxAmmo}";
        ammoText.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (!hasAuthority) return;
        timerSlider.value += Time.deltaTime;
        if (currentAmmo <= 0) { return; }
        if (Input.GetKey(shootKey)&&canShoot)
        {
            StartCoroutine(ShootDelay());
        }
    }
             private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Ammo")
        {
            ammoAmount=other.gameObject.GetComponent<AmmoPickup>().ammoAmount;
            CmdAddAmmo();
            Destroy(other.gameObject);
        }
    }


    [TargetRpc]
    public void TargetUpdateAmmo(NetworkConnection sender)
    {
        currentAmmo--;
        ammoText.text = $"Ammo: {currentAmmo}/{maxAmmo}";
    }

     [TargetRpc]
    public void TargetAddAmmo(NetworkConnection sender)
    {
        currentAmmo += ammoAmount;
        
        ammoText.text = $"Ammo: {currentAmmo}/{maxAmmo}";
    }

    IEnumerator ShootDelay()
    {
        if (hasAuthority)
        {
            CmdFire();
            yield return new WaitForEndOfFrame();
            timerSlider.value = 0;
            canShoot = false;
            yield return new WaitForSeconds(cannonRechargeTime);
            canShoot = true;
        }
        else
        {
            yield return new WaitForSeconds(1);
        }
    }

    [Command]
    public void CmdFire(NetworkConnectionToClient sender = null)
    {
        
        TargetUpdateAmmo(sender);
        AudioSource.PlayClipAtPoint(shotFiringClip, transform.position, shotFiringVolume);
        GameObject projectile = Instantiate(projectilePrefab, projectileMount.position, projectileMount.rotation);
        NetworkServer.Spawn(projectile, sender);
        
    }
     [Command]
    public void CmdAddAmmo(NetworkConnectionToClient sender = null)
    {
        TargetAddAmmo(sender);
    }
}

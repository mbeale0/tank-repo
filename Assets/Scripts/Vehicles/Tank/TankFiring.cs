using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;


public class TankFiring :NetworkBehaviour
{
    public TankCannonClass cannonClass;
    public TankMachineGunClass machineGunClass;
    
    [Header("Firing")]
    public KeyCode shootKey = KeyCode.Space;
    public Slider timerSlider;
    [SerializeField] private AudioClip shotFiringClip;
    [SerializeField] private TMP_Text ammoText = null;
    [SerializeField] private TMP_Text gunText = null;

    private GameObject tankProjectilePrefab;
    private Transform tankProjectileMount;
    private float tankShotFiringVolume;
    private float tankRechargeTime;
    private int tankMaxAmmo;
    private int tankAmmoAmount;
    private int tankCurrentAmmo;
    private bool canShoot = true;
    private bool hasSwitchedWeapon = false; // note: this is a system for only two weapons 
    private void Start()
    {
        if (!hasAuthority) { return; }
        UpdateForCannon();
        timerSlider.maxValue = tankRechargeTime;
        timerSlider.value = tankRechargeTime;
        timerSlider.gameObject.SetActive(true);
        tankCurrentAmmo = tankMaxAmmo;
        ammoText.text = $"Ammo: {tankCurrentAmmo}/{tankMaxAmmo}";
        ammoText.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (!hasAuthority) return;
        timerSlider.value += Time.deltaTime;
        CheckWeaponSwitching();
        if (tankCurrentAmmo <= 0) { return; }
        if (Input.GetKey(shootKey) && canShoot)
        {
            StartCoroutine(ShootDelay());
        }
    }

    private void CheckWeaponSwitching()
    {
        // TODO cache current ammo to switch back to
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateForCannon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpdateForMachineGun();
        }
    }
    private void UpdateForCannon()
    {
        tankMaxAmmo = cannonClass.cannonMaxAmmo;
        tankRechargeTime = cannonClass.cannonRechargeTime;
        tankShotFiringVolume = cannonClass.cannonShotFiringVolume;
        tankProjectileMount = cannonClass.cannonProjectileMount;
        tankProjectilePrefab = cannonClass.cannonProjectilePrefab;
        gunText.text = "Gun: Cannon";
        TargetUpdateAmmo(connectionToServer);
    }
    private void UpdateForMachineGun()
    {
        tankMaxAmmo = machineGunClass.gunMaxAmmo;
        tankRechargeTime = machineGunClass.gunRechargeTime;
        tankShotFiringVolume = machineGunClass.gunshotFiringVolume;
        tankProjectileMount = machineGunClass.gunProjectileMount;
        tankProjectilePrefab = machineGunClass.gunProjectilePrefab;
        gunText.text = "Gun: MachineGun";
        if (!hasSwitchedWeapon)
        {
            tankCurrentAmmo = tankMaxAmmo;
            hasSwitchedWeapon = true;
        }
        TargetUpdateAmmo(connectionToServer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Ammo")
        {
            tankAmmoAmount = other.gameObject.GetComponent<AmmoPickup>().ammoAmount;
            CmdAddAmmo();
            Destroy(other.gameObject);
        }
    }

    [TargetRpc]
    public void TargetUpdateAmmo(NetworkConnection sender)
    {
        ammoText.text = $"Ammo: {tankCurrentAmmo}/{tankMaxAmmo}";
    }
    [TargetRpc]
    public void TargetReduceAmmo(NetworkConnection sender)
    {
        tankCurrentAmmo--;
        ammoText.text = $"Ammo: {tankCurrentAmmo}/{tankMaxAmmo}";
    }

     [TargetRpc]
    public void TargetAddAmmo(NetworkConnection sender)
    {
        tankCurrentAmmo += tankAmmoAmount;
        
        ammoText.text = $"Ammo: {tankCurrentAmmo}/{tankMaxAmmo}";
    }

    IEnumerator ShootDelay()
    {
        if (hasAuthority)
        {
            CmdFire();
            yield return new WaitForEndOfFrame();
            timerSlider.value = 0;
            canShoot = false;
            yield return new WaitForSeconds(tankRechargeTime);
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
        
        TargetReduceAmmo(sender);
        AudioSource.PlayClipAtPoint(shotFiringClip, transform.position, tankShotFiringVolume);
        GameObject projectile = Instantiate(tankProjectilePrefab, tankProjectileMount.position, tankProjectileMount.rotation);
        NetworkServer.Spawn(projectile, sender);
        
    }
     [Command]
    public void CmdAddAmmo(NetworkConnectionToClient sender = null)
    {
        TargetAddAmmo(sender);
    }
}


[System.Serializable]
public class TankCannonClass
{
    public Transform cannonProjectileMount;
    public float cannonShotFiringVolume = 1f;
    public float cannonRechargeTime = 1f;
    public int cannonMaxAmmo = 10;
    public GameObject cannonProjectilePrefab;
}
[System.Serializable]
public class TankMachineGunClass
{
    public Transform gunProjectileMount;
    public float gunshotFiringVolume = .5f;
    public float gunRechargeTime = .5f;
    public int gunMaxAmmo = 20;
    public GameObject gunProjectilePrefab;
}


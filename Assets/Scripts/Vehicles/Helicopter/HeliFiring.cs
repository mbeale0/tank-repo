using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;


public class HeliFiring :NetworkBehaviour
{
    public HeliCannonClass heliCannonClass;
    public HeliMachineGunClass heliMachineGunClass;
    public HeliMissileClass heliMissileClass;

    [Header("Firing")]
    public KeyCode shootKey = KeyCode.Space;
    public Slider timerSlider;
    [SerializeField] private AudioClip shotFiringClip;
    [SerializeField] private TMP_Text ammoText = null;
    [SerializeField] private TMP_Text gunText = null;

    private GameObject heliProjectilePrefab;
    private Transform heliProjectileMount;
    private float heliShotFiringVolume;
    private float heliRechargeTime;
    private int heliMaxAmmo;
    private int heliAmmoAmount;
    private int heliCurrentAmmo;
    private bool canShoot = true;
    private bool hasSwitchedToMissile = false;
    private bool hasSwitchedToMachinegun = false;

    private enum CurrentWeapon { Cannon, MachineGun, Missile};
    private CurrentWeapon currentWeapon;

    private void Start()
    {
        UpdateForFirstStart();
        if (!hasAuthority) { return; }
        currentWeapon = CurrentWeapon.Cannon;
        timerSlider.maxValue = heliRechargeTime;
        timerSlider.value = heliRechargeTime;
        timerSlider.gameObject.SetActive(true);
        heliCurrentAmmo = heliMaxAmmo;
        ammoText.text = $"Ammo: {heliCurrentAmmo}/{heliMaxAmmo}";
        ammoText.gameObject.SetActive(true);
        gunText.gameObject.SetActive(true);
    }
    private void Update()
    {
        CheckWeaponSwitching();
        if (!hasAuthority) return;
        timerSlider.value += Time.deltaTime;
        if (heliCurrentAmmo <= 0) { return; }
    }

    private void OnFire()
    {
        if (canShoot)
        {
            StartCoroutine(ShootDelay());
        }
    }

    private void CheckWeaponSwitching()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateForCannon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpdateForMachineGun();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpdateForMissile();
        }
    }

    private void UpdateForFirstStart()
    {
        heliMaxAmmo = heliCannonClass.cannonMaxAmmo;
        heliRechargeTime = heliCannonClass.cannonRechargeTime;
        heliShotFiringVolume = heliCannonClass.cannonShotFiringVolume;
        heliProjectileMount = heliCannonClass.cannonProjectileMount;
        heliProjectilePrefab = heliCannonClass.cannonProjectilePrefab;
        gunText.text = "Gun: Cannon";
    }
    private void UpdateForCannon()
    {
        switch (currentWeapon)
        {
            case CurrentWeapon.MachineGun:
                heliMachineGunClass.gunCurrentAmmo = heliCurrentAmmo;
                break;
            case CurrentWeapon.Missile:
                heliMissileClass.missileCurrentAmmo = heliCurrentAmmo;
                break;
        }
        currentWeapon = CurrentWeapon.Cannon;
        heliCurrentAmmo = heliCannonClass.cannonCurrentAmmo;
        heliMaxAmmo = heliCannonClass.cannonMaxAmmo;
        heliRechargeTime = heliCannonClass.cannonRechargeTime;
        heliShotFiringVolume = heliCannonClass.cannonShotFiringVolume;
        heliProjectileMount = heliCannonClass.cannonProjectileMount;
        heliProjectilePrefab = heliCannonClass.cannonProjectilePrefab;
        gunText.text = "Gun: Cannon";
        ammoText.text = $"Ammo: {heliCurrentAmmo}/{heliMaxAmmo}";
    }
    private void UpdateForMachineGun()
    {
        switch (currentWeapon)
        {
            case CurrentWeapon.Cannon:
                heliCannonClass.cannonCurrentAmmo = heliCurrentAmmo;
                break;
            case CurrentWeapon.Missile:
                heliMissileClass.missileCurrentAmmo = heliCurrentAmmo;
                break;
        }
        currentWeapon = CurrentWeapon.MachineGun;
        heliCurrentAmmo = heliMachineGunClass.gunCurrentAmmo;
        heliMaxAmmo = heliMachineGunClass.gunMaxAmmo;
        heliRechargeTime = heliMachineGunClass.gunRechargeTime;
        heliShotFiringVolume = heliMachineGunClass.gunshotFiringVolume;
        heliProjectileMount = heliMachineGunClass.gunProjectileMount;
        heliProjectilePrefab = heliMachineGunClass.gunProjectilePrefab;
        gunText.text = "Gun: MachineGun";
        if (!hasSwitchedToMachinegun)
        {
            heliCurrentAmmo = heliMaxAmmo;
            hasSwitchedToMachinegun = true;
        }
        ammoText.text = $"Ammo: {heliCurrentAmmo}/{heliMaxAmmo}";
    }
    private void UpdateForMissile()
    {
        switch (currentWeapon)
        {
            case CurrentWeapon.Cannon:
                heliCannonClass.cannonCurrentAmmo = heliCurrentAmmo;
                break;
            case CurrentWeapon.MachineGun:
                heliMissileClass.missileCurrentAmmo = heliCurrentAmmo;
                break;
        }
        currentWeapon = CurrentWeapon.Missile;
        heliCurrentAmmo = heliMissileClass.missileCurrentAmmo;
        heliMaxAmmo = heliMissileClass.missileMaxAmmo;
        heliRechargeTime = heliMissileClass.missileRechargeTime;
        heliShotFiringVolume = heliMissileClass.missileFiringVolume;
        heliProjectileMount = heliMissileClass.missileProjectileMount;
        heliProjectilePrefab = heliMissileClass.missileProjectilePrefab;
        gunText.text = "Gun: Missile";
        if (!hasSwitchedToMissile)
        {
            heliCurrentAmmo = heliMaxAmmo;
            hasSwitchedToMissile = true;
        }
        ammoText.text = $"Ammo: {heliCurrentAmmo}/{heliMaxAmmo}";
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Ammo")
        {
            heliAmmoAmount = other.gameObject.GetComponent<AmmoPickup>().ammoAmount;
            CmdAddAmmo();
            Destroy(other.gameObject);
        }
    }
    [TargetRpc]
    public void TargetReduceAmmo(NetworkConnection sender)
    {
        heliCurrentAmmo--;
        ammoText.text = $"Ammo: {heliCurrentAmmo}/{heliMaxAmmo}";
    }

     [TargetRpc]
    public void TargetAddAmmo(NetworkConnection sender)
    {
        heliCurrentAmmo += heliAmmoAmount;
        
        ammoText.text = $"Ammo: {heliCurrentAmmo}/{heliMaxAmmo}";
    }

    IEnumerator ShootDelay()
    {
        if (hasAuthority)
        {
            
            CmdFire();
            yield return new WaitForEndOfFrame();
            timerSlider.value = 0;
            canShoot = false;
            yield return new WaitForSeconds(heliRechargeTime);
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
        AudioSource.PlayClipAtPoint(shotFiringClip, transform.position, heliShotFiringVolume); 
        GameObject projectile = Instantiate(heliProjectilePrefab, heliProjectileMount.position, heliProjectileMount.rotation);
        NetworkServer.Spawn(projectile, sender);
    }
    [Command]
    public void CmdAddAmmo(NetworkConnectionToClient sender = null)
    {
        TargetAddAmmo(sender);
    }
}

[System.Serializable]
public class HeliCannonClass
{
    public int cannonCurrentAmmo;
    public Transform cannonProjectileMount;
    public float cannonShotFiringVolume = 1f;
    public float cannonRechargeTime = 1f;
    public int cannonMaxAmmo = 10;
    public GameObject cannonProjectilePrefab;
}
[System.Serializable]
public class HeliMachineGunClass
{
    public int gunCurrentAmmo;
    public Transform gunProjectileMount;
    public float gunshotFiringVolume = .5f;
    public float gunRechargeTime = .5f;
    public int gunMaxAmmo = 20;
    public GameObject gunProjectilePrefab;
}
[System.Serializable]
public class HeliMissileClass
{
    public int missileCurrentAmmo;
    public Transform missileProjectileMount;
    public float missileFiringVolume = 1f;
    public float missileRechargeTime = 2f;
    public int missileMaxAmmo = 5;
    public GameObject missileProjectilePrefab;
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;


public class TankFiring :NetworkBehaviour
{
    public TankCannonClass tankCannonClass;
    public TankMachineGunClass tankMachineGunClass;
    
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

    private enum CurrentWeapon { Cannon, MachineGun};
    private CurrentWeapon currentWeapon;

    private void Start()
    {
        UpdateForFirstStart();
        if (!hasAuthority) { return; }
        currentWeapon = CurrentWeapon.Cannon;
        timerSlider.maxValue = tankRechargeTime;
        timerSlider.value = tankRechargeTime;
        timerSlider.gameObject.SetActive(true);
        tankCurrentAmmo = tankMaxAmmo;
        ammoText.text = $"Ammo: {tankCurrentAmmo}/{tankMaxAmmo}";
        ammoText.gameObject.SetActive(true);
        gunText.gameObject.SetActive(true);
    }
    private void Update()
    {
        CheckWeaponSwitching();
        if (!hasAuthority) return;
        timerSlider.value += Time.deltaTime;
        if (tankCurrentAmmo <= 0) { return; }
        if (Input.GetKey(shootKey) && canShoot)
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
    }

    private void UpdateForFirstStart()
    {
        tankMaxAmmo = tankCannonClass.cannonMaxAmmo;
        tankRechargeTime = tankCannonClass.cannonRechargeTime;
        tankShotFiringVolume = tankCannonClass.cannonShotFiringVolume;
        tankProjectileMount = tankCannonClass.cannonProjectileMount;
        tankProjectilePrefab = tankCannonClass.cannonProjectilePrefab;
        gunText.text = "Gun: Cannon";
    }
    private void UpdateForCannon()
    {
        switch (currentWeapon)
        {
            case CurrentWeapon.MachineGun:
                tankMachineGunClass.gunCurrentAmmo = tankCurrentAmmo;
                break;
        }
        currentWeapon = CurrentWeapon.Cannon;
        tankCurrentAmmo = tankCannonClass.cannonCurrentAmmo;
        tankMaxAmmo = tankCannonClass.cannonMaxAmmo;
        tankRechargeTime = tankCannonClass.cannonRechargeTime;
        tankShotFiringVolume = tankCannonClass.cannonShotFiringVolume;
        tankProjectileMount = tankCannonClass.cannonProjectileMount;
        tankProjectilePrefab = tankCannonClass.cannonProjectilePrefab;
        gunText.text = "Gun: Cannon";
        ammoText.text = $"Ammo: {tankCurrentAmmo}/{tankMaxAmmo}";
    }
    private void UpdateForMachineGun()
    {
        switch (currentWeapon)
        {
            case CurrentWeapon.Cannon:
                tankCannonClass.cannonCurrentAmmo = tankCurrentAmmo;
                break;
        }
        currentWeapon = CurrentWeapon.MachineGun;
        tankCurrentAmmo = tankMachineGunClass.gunCurrentAmmo;
        tankMaxAmmo = tankMachineGunClass.gunMaxAmmo;
        tankRechargeTime = tankMachineGunClass.gunRechargeTime;
        tankShotFiringVolume = tankMachineGunClass.gunshotFiringVolume;
        tankProjectileMount = tankMachineGunClass.gunProjectileMount;
        tankProjectilePrefab = tankMachineGunClass.gunProjectilePrefab;
        gunText.text = "Gun: MachineGun";
        if (!hasSwitchedWeapon)
        {
            tankCurrentAmmo = tankMaxAmmo;
            hasSwitchedWeapon = true;
        }
        ammoText.text = $"Ammo: {tankCurrentAmmo}/{tankMaxAmmo}";
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
    public int cannonCurrentAmmo;
    public Transform cannonProjectileMount;
    public float cannonShotFiringVolume = 1f;
    public float cannonRechargeTime = 1f;
    public int cannonMaxAmmo = 10;
    public GameObject cannonProjectilePrefab;
}
[System.Serializable]
public class TankMachineGunClass
{
    public int gunCurrentAmmo;
    public Transform gunProjectileMount;
    public float gunshotFiringVolume = .5f;
    public float gunRechargeTime = .5f;
    public int gunMaxAmmo = 20;
    public GameObject gunProjectilePrefab;
}


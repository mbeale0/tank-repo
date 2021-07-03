using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Firing :NetworkBehaviour
{
    
    [Header("Firing")]
    public KeyCode shootKey = KeyCode.Space;
    public GameObject projectilePrefab;
    public Transform projectileMount;
    [SerializeField] AudioClip shotFiringClip;
    [SerializeField] float shotFiringVolume =1f;
    [SerializeField] private float cannonRechargeTime = 1f;
    public Slider timerSlider;
    private bool canShoot=true;

    AudioSource audioSource;
  
    private void Update()
    {
        if (!hasAuthority) return;
        timerSlider.value += Time.deltaTime;
        if (Input.GetKey(shootKey)&&canShoot)
        {
            StartCoroutine(ShootDelay());
        }
    }

    IEnumerator ShootDelay()
    {
        
        CmdFire();
        yield return new WaitForEndOfFrame();
        timerSlider.value = 0;
        canShoot=false;
        yield return new WaitForSeconds(cannonRechargeTime);
        canShoot = true;
    }
    private void Start()
    {
        if (!hasAuthority) { return; }
        audioSource = GetComponent<AudioSource>();
        timerSlider.maxValue = cannonRechargeTime;
        timerSlider.value = cannonRechargeTime;
        timerSlider.gameObject.SetActive(true);
    }
    [Command]
    public void CmdFire()
    {
        AkSoundEngine.PostEvent("Play_Tank_Shoot", gameObject);
        AudioSource.PlayClipAtPoint(shotFiringClip, transform.position, shotFiringVolume);
        GameObject projectile = Instantiate(projectilePrefab, projectileMount.position, projectileMount.rotation);
        NetworkServer.Spawn(projectile);
        
    }
}
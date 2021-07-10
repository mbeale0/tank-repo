using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class AmmoPickup : NetworkBehaviour
{
    [SerializeField] int ammoAmount = 5;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            FindObjectOfType<Firing>().AddAmmo(ammoAmount);
            Destroy(gameObject);
        }
    }
}

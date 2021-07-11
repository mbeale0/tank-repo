using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vehicles;
public class FuelPickup : MonoBehaviour
{
    [SerializeField] float fuel = 50;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<GeneralVehicleMovement>().AddFuel(fuel);
            Destroy(gameObject);
        }
    }
}

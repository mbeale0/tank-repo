using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBuilding : MonoBehaviour
{
    [SerializeField] GameObject FracturedBuildingPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(FracturedBuildingPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

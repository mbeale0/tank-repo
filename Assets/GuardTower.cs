using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardTower : MonoBehaviour
{
 public GameObject fracturedGuardTower;
 
 void OnTriggerEnter(Collider other){
 
 Debug.Log("entered");
     Instantiate(fracturedGuardTower,transform.position,transform.rotation);
     Destroy(gameObject);
      }
     
}

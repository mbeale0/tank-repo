using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleViewer : MonoBehaviour
{
    [SerializeField] string vehicleType = null;
    private void OnMouseOver()
    {
        transform.GetComponent<Renderer>().material.SetFloat("_Metallic", .45f);
        /*/
         * TODO using the above string we can possibly call some function that chooses that prefab
         * Basic controls are implemented for each vehicle, enough to where they can move around and be tested
         * I don't know how to choose player vehicle from code(since it is mirror stuff) Or even where it happens, 
         * but if told I may be able to figure out how to connect to this
         * In order to test vehicles I found they can be switched with the tank prefav under player object in networkmanager script on menu
         /*/

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("User selected: " + vehicleType);
        }
    }
    private void OnMouseExit()
    {
        transform.GetComponent<Renderer>().material.SetFloat("_Metallic", 1f);
    }

}

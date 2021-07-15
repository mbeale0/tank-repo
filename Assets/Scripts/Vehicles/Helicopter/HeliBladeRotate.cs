using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliBladeRotate : NetworkBehaviour
{
    [SerializeField] private GameObject heliBlades = null;
    [SerializeField] private GameObject heliTailRotor = null;
    [SerializeField] private int rotateSpeed = 15;
    private void FixedUpdate()
    {
        heliBlades.transform.Rotate(new Vector3(0f, 1f, 0f), rotateSpeed);
        heliTailRotor.transform.Rotate(new Vector3(1f, 0f, 0f), rotateSpeed);
    }
}

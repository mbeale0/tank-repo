using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookAtObject : MonoBehaviour
{
    [SerializeField] private GameObject target;

    void Update()
    {
        transform.LookAt(Mouse.current.position.ReadValue());
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHiglighter : MonoBehaviour
{
    private void OnMouseEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
    private void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }
}

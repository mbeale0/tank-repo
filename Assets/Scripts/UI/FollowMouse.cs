using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour
{
    private Vector2 mousePos;
    private void Start()
    {
        transform.localPosition = new Vector3(0, 0, 15);
    }
    void Update()
    {
        Vector2 mouse = Mouse.current.position.ReadValue();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        //mouse.x = 0;

        

        float cursorYPos = Mathf.Clamp(((mouse.y * .05f) - 60), 1, 15);
        float cursorXPos = Mathf.Clamp(((mouse.x * .05f) - 60), -45, 45);
        Debug.Log($"{mouse.y} and {transform.localPosition.y} and {cursorYPos}");
        transform.Rotate(1, 0, 0);
        //transform.localPosition = new Vector3(cursorXPos, cursorYPos, 15);
    }
    private void OnAimPos(InputValue input)
    {
        mousePos = input.Get<Vector2>();
    }
}

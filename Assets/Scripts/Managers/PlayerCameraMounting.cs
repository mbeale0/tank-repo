﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class PlayerCameraMounting : NetworkBehaviour
{
    [SerializeField] GameObject CameraMountPoint;

    void Start()
    {
        if (isLocalPlayer)
        {
            Transform cameraTransform = Camera.main.gameObject.transform;  //Find main camera which is part of the scene instead of the prefab
            cameraTransform.parent = CameraMountPoint.transform;  //Make the camera a child of the mount point
            cameraTransform.position = CameraMountPoint.transform.position;  //Set position/rotation same as the mount point
            cameraTransform.rotation = CameraMountPoint.transform.rotation;

        }
    }

    private void OnDisable()
    {
        if (isLocalPlayer)
        {
            Transform cameraTransform = Camera.main.gameObject.transform;
            cameraTransform.parent = null;
        }
    }
}
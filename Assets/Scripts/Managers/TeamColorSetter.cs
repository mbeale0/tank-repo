﻿using Managers;
using Mirror;
using UnityEngine;

public class TeamColorSetter : NetworkBehaviour
{
    [SerializeField] private Renderer[] colorRenderers = new Renderer[0];

    [SyncVar(hook = nameof(HandleTeamColorUpdated))]
    private Color teamColor = new Color();

    #region Server

    public override void OnStartServer()
    {
        MyPlayer player = connectionToClient.identity.GetComponent<MyPlayer>();
        teamColor = player.GetTeamColor();
    }
    #endregion

    #region Client
    private void HandleTeamColorUpdated(Color oldColor, Color newColor)
    {
        foreach(Renderer renderer in colorRenderers)
        {
            renderer.material.SetColor("_Color", newColor);
        }
    }

    #endregion
}

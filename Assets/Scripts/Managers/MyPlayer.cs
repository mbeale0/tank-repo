using Mirror;
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Managers
{
    public class MyPlayer : NetworkBehaviour
    {
        NetworkConnection cachedNetworkConnection;
        private Color _teamColor = new Color();
        [SyncVar(hook = nameof(AuthorityHandlePartyOwnerState))]
        private bool isPartyOwner = false;
        [SyncVar(hook = nameof(ClientHandledisplayNameUpdated))]
        private string displayName;


        public static event Action ClientOnInfoUpdated;
        public static event Action<bool> AuthorityOnPartyOwnerStateUpdated;

        public string GetDisplayName()
        {
            return displayName;
        }
        public bool GetIsPartyOwner()
        {
            return isPartyOwner;
        }
        public Color GetTeamColor()
        {
            return _teamColor;
        }

        [Server]
        public void SetTeamColor(Color newTeamColor)
        {
            _teamColor = newTeamColor;
        }
        [Server]
        public void SetPartyOwner(bool state)
        {
            isPartyOwner = state;
        }
        [Server]
        public void SetDisplayName(string newDisplayname)
        {
            this.displayName = newDisplayname;
        }

        [Command]
        public void CmdStartGame()
        {
            if (!isPartyOwner) { return; }

            ((MyNetworkManager)NetworkManager.singleton).StartGame();
        }
        private void AuthorityHandlePartyOwnerState(bool oldState, bool newState)
        {
            if (!hasAuthority) { return; }
            AuthorityOnPartyOwnerStateUpdated?.Invoke(newState);
        }
        private void ClientHandledisplayNameUpdated(string oldName, string newName)
        {
            ClientOnInfoUpdated?.Invoke();
        }
        public override void OnStartClient()
        {
            ClientOnInfoUpdated?.Invoke();

            MyPlayer player = connectionToClient.identity.GetComponent<MyPlayer>();
            _teamColor = player.GetTeamColor();

            if (NetworkServer.active) { return; }

            ((MyNetworkManager)NetworkManager.singleton).Players.Add(this);
        }
        public override void OnStopClient()
        {
            ClientOnInfoUpdated?.Invoke();
            if (!isClientOnly) { return; }

            ((MyNetworkManager)NetworkManager.singleton).Players.Remove(this);
        }
        
    }
}

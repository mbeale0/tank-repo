using Mirror;
using System;
using UnityEngine;

namespace Managers
{
    public class MyPlayer : NetworkBehaviour
    {
        [SerializeField] private int jeepLives = 3;
        [SerializeField] private int tankLives = 3;
        [SerializeField] private int aaTankLives = 3;
        [SerializeField] private int heliLives = 3;

        [SyncVar(hook = nameof(AuthorityHandlePartyOwnerState))]
        private bool isPartyOwner = false;
        [SyncVar(hook = nameof(ClientHandledisplayNameUpdated))]
        private string displayName;

        private Color _teamColor = new Color();
        private int maxPlayerLives = 0;

        NetworkConnection cachedNetworkConnection;

        public static event Action ClientOnInfoUpdated;
        public static event Action<bool> AuthorityOnPartyOwnerStateUpdated;

        #region Getters
        public int GetLives(string vehicleType)
        {
            return GetVehicleLives(vehicleType);
        }
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
        private int GetVehicleLives(string vehicleType)
        {
            if (vehicleType == "JEEP")
            {
                return jeepLives;
            }
            else if (vehicleType == "TANK")
            {
                return tankLives;
            }
            else if (vehicleType == "AATANK")
            {
                return aaTankLives;
            }
            else if (vehicleType == "HELI")
            {
                return heliLives;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region Setters
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
        private void SetMaxPlayerLives()
        {
            maxPlayerLives = jeepLives + tankLives + aaTankLives + heliLives;
        }
        #endregion

        public void ReducePlayerLives(string vehicleType)
        {
            if (vehicleType == "JEEP")
            {
                jeepLives--;
            }
            else if (vehicleType == "TANK")
            {
                tankLives--;
            }
            else if (vehicleType == "AATANK")
            {
                aaTankLives--;
            }
            else if (vehicleType == "HELI")
            {
                heliLives--;
            }
        }  

        private void Update()
        {
            if (!hasAuthority) { return; }
            SetMaxPlayerLives();
            if (maxPlayerLives == 0)
            {
                Debug.Log("GAME OVER");
            }
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
            //Why are you referencing this own script to call a function you can call naturally?
            //MyPlayer player = connectionToClient.identity.GetComponent<MyPlayer>();
            //_teamColor = player.GetTeamColor();
            _teamColor = GetTeamColor();
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

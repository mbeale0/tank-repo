using Mirror;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Managers
{
    public class MyPlayer : NetworkBehaviour
    {
        NetworkConnection cachedNetworkConnection;
        private Color _teamColor = new Color();


        private void Start()
        {
            cachedNetworkConnection = connectionToClient;
            NetworkServer.RemovePlayerForConnection(connectionToClient, false);
            NetworkServer.AddPlayerForConnection(cachedNetworkConnection, gameObject);
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
    }
}

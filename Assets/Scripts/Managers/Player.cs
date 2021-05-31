using Mirror;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Managers
{
    public class Player : NetworkBehaviour
    {
        private Color _teamColor = new Color();

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

using Complete;
using GameObjects;
using Mirror;
using UnityEngine;

namespace Vehicles
{

    public class HeliController : NetworkBehaviour
    {
        [SerializeField] private GameObject playerMinmap = null;

        public bool hasFlag = false;
        public GameObject flagMountPoint;
        private HeliMovement _heliMovement;
        private TurretRotate _turretRotate;

        private void Start()
        {
            _turretRotate = GetComponent<TurretRotate>(); 
            _heliMovement = GetComponent<HeliMovement>();
            if (!hasAuthority) { return; }

            playerMinmap.SetActive(true);
        }

        private void FixedUpdate()
        {
            if (!hasAuthority) return;

            //_heliMovement.OnMove();
            //_heliMovement.OnTurn();
            _turretRotate.Rotate();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Flag")|| hasFlag) return;
            var otherTransform = other.transform;
            otherTransform.parent = flagMountPoint.transform;
            otherTransform.position = flagMountPoint.transform.position;
            otherTransform.rotation = flagMountPoint.transform.rotation;       
            hasFlag = true;

        }
    }
}


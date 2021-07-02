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
        private HeliMovement _heliMovement1;
        private TurretRotate _turretRotate;

        private void Start()
        {
            //_turretRotate = GetComponent<TurretRotate>(); // temporarily turned off
            _heliMovement1 = GetComponent<HeliMovement>();
            _heliMovement = GetComponent<HeliMovement>();
            if (!hasAuthority) { return; }

            playerMinmap.SetActive(true);
        }

        private void FixedUpdate()
        {
            if (!hasAuthority) return;

            _heliMovement.Move();
            _heliMovement.Turn();
            //_turretRotate.Rotate();
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


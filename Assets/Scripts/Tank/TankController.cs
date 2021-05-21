using Complete;
using Mirror;
using UnityEngine;

namespace Tank {

    public class TankController : NetworkBehaviour
    {
        public GameObject flagMountPoint;
        private TankMovement _tankMovement;
        private TankMovement _tankMovement1;
        private TurretRotate _turretRotate;

        private void Start()
        {
            _turretRotate = GetComponent<TurretRotate>();
            _tankMovement1 = GetComponent<TankMovement>();
            _tankMovement = GetComponent<TankMovement>();
        }

        private void FixedUpdate()
        {
            if (!hasAuthority) return;

            _tankMovement.Move();
            _tankMovement1.Turn();
            _turretRotate.Rotate();
        }

       private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Flag")) return;
            var otherTransform = other.transform;
            otherTransform.parent = flagMountPoint.transform;
            otherTransform.position = flagMountPoint.transform.position;
            otherTransform.rotation = flagMountPoint.transform.rotation;
        }
    }
}


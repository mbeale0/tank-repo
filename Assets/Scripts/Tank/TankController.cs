using Complete;
using GameObjects;
using Mirror;
using UnityEngine;

namespace Tank {

    public class TankController : NetworkBehaviour
    {
        public bool hasFlag = false;
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
            if (!other.CompareTag("Flag")|| hasFlag) return;
            var otherTransform = other.transform;
            otherTransform.parent = flagMountPoint.transform;
            otherTransform.position = flagMountPoint.transform.position;
            otherTransform.rotation = flagMountPoint.transform.rotation;
            hasFlag = true;

            if (!hasFlag || !other.CompareTag("HomeBase")) return;
            otherTransform.parent= other.GetComponent<HomeBase>().flagMountPoint.transform;
            otherTransform.position = other.GetComponent<HomeBase>().flagMountPoint.transform.position;
            otherTransform.rotation = other.GetComponent<HomeBase>().flagMountPoint.transform.rotation;
            hasFlag = false;
        }
    }
}


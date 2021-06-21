using Complete;
using GameObjects;
using Mirror;
using UnityEngine;

namespace Tank {

    public class TankController : NetworkBehaviour
    {
        [SerializeField] private GameObject playerMinmap = null;

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
            if (!hasAuthority) { return; }

            playerMinmap.SetActive(true);
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

        }
    }
}


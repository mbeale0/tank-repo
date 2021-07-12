using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicles
{
    public class HeliMovement : GeneralVehicleMovement
    {
        [SerializeField] private GameObject heliGameObject = null;
        [Tooltip("The rotation in code as of now is only between -1 and 1, so this number is small")]
        [SerializeField][Range(0, 1)] private float rotationLock = .18f;
        
        private float leanSpeed = .3f;

        protected override void Awake()
        {
            base.Awake();
        }
        protected override void OnEnable()
        {
            base.OnEnable();
        }
        protected override void OnDisable()
        {
            base.OnDisable();
        }
        protected override void Start()
        {
            base.Start();
        }
        protected override void Update()
        {
            base.Update();   

            UpdateHeliLean();
        }
        private void UpdateHeliLean()
        {
            if(!hasAuthority) { return; }
            if (Input.GetKey("e"))
            {
                LeanRight();
            }
            else if (Input.GetKey("q"))
            {
                LeanLeft();
            }
            else if (heliGameObject.transform.localRotation.z < 0)
            {
                heliGameObject.transform.Rotate(new Vector3(0, 0, .2f));
            }
            else if (heliGameObject.transform.localRotation.z > 0)
            {
                heliGameObject.transform.Rotate(new Vector3(0, 0, -.2f));
            }
        }
        private void LeanRight()
        {
            heliGameObject.transform.Rotate(new Vector3(0, 0, -.4f) );
            transform.Translate(new Vector3(leanSpeed, 0f, 0f));

            if (heliGameObject.transform.localRotation.z < -rotationLock)
            {
                var rot = heliGameObject.transform.localRotation;
                rot.z = -rotationLock;
                heliGameObject.transform.localRotation = rot;
            }
            else if (heliGameObject.transform.localRotation.z > rotationLock)
            {
                var rot = heliGameObject.transform.localRotation;
                rot.z = rotationLock;
                heliGameObject.transform.localRotation = rot;
            }
        }
        private void LeanLeft()
        {
            heliGameObject.transform.Rotate(new Vector3(0, 0, .4f));
            transform.Translate(new Vector3(-leanSpeed, 0f, 0f));
            if (heliGameObject.transform.localRotation.z < -rotationLock)
            {
                var rot = heliGameObject.transform.localRotation;
                rot.z = -rotationLock;
                heliGameObject.transform.localRotation = rot;
            }
            else if (heliGameObject.transform.localRotation.z > rotationLock)
            {
                var rot = heliGameObject.transform.localRotation;
                rot.z = rotationLock;
                heliGameObject.transform.localRotation = rot;
            }
        }
        protected override void EngineAudio()
        {
            base.EngineAudio();
        }
        public override void Move()
        {
            base.Move();
        }
        public override void Turn()
        {
            base.Turn();
        }
    }
}
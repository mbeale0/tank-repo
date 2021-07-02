using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vehicles
{
    public class HeliMovement : GeneralVehicleMovement
    {
        [SerializeField] private GameObject heliGameObject = null;
        [Tooltip("The rotation in code as of now is only between -1 and 1, so this number is small")]
        [SerializeField] private float rotationLock = .25f;
        

        float speed = 1f;
        float rotation = 0f;
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
            if (Input.GetKey("e"))
            {
                heliGameObject.transform.Rotate(new Vector3(0, 0, -.2f));
                if (heliGameObject.transform.rotation.z < -rotationLock)
                {
                    var rot = heliGameObject.transform.rotation;
                    rot.z = -rotationLock;
                    heliGameObject.transform.rotation = rot;
                }
                else if (heliGameObject.transform.rotation.z > rotationLock)
                {
                    var rot = heliGameObject.transform.rotation;
                    rot.z = rotationLock;

                }
            }
            if (Input.GetKey("q"))
            {
                heliGameObject.transform.Rotate(new Vector3(0, 0, .2f));
                if (heliGameObject.transform.rotation.z < -rotationLock)
                {
                    var rot = heliGameObject.transform.rotation;
                    rot.z = -rotationLock;
                    heliGameObject.transform.rotation = rot;
                }
                else if (heliGameObject.transform.rotation.z > rotationLock)
                {
                    var rot = heliGameObject.transform.rotation;
                    rot.z = rotationLock;
                    heliGameObject.transform.rotation = rot;
                }
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
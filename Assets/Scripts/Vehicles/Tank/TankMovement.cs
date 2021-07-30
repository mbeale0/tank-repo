using UnityEngine;
using UnityEngine.InputSystem;

namespace Vehicles
{
    public class TankMovement : GeneralVehicleMovement
    {

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
        }
        protected override void EngineAudio()
        {
            base.EngineAudio();
        }
        public void OnMove(InputValue input)
        {
            vehicleMovementValue = input.Get<float>();         
        }
        public void OnTurn(InputValue input)
        {
            vehicleTurnValue = input.Get<float>();
        }
    }
}
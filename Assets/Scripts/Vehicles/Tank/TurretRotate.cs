using UnityEngine;
using UnityEngine.InputSystem;

namespace Complete
{
    public class TurretRotate : MonoBehaviour
    {
        [SerializeField] GameObject turret;
        [SerializeField] GameObject barrel;
        [SerializeField] private GameObject crossHair;
        [SerializeField] Quaternion barrelQuaternionRotation;
        [SerializeField] TankFiring firingScript;
        [SerializeField] HeliFiring heliFiringScript;
        AudioSource audioSource;
        private bool rotateUp = false;
        private bool hasFired = true;// not realy true but prevents fire on start
        private bool isDown = true;
        private float HorizontalRotationDirection;
        private Vector2 mousePos;
        private float lastMousePos = 0;
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }
        public void Rotate()
        {
            mousePos = Mouse.current.position.ReadValue();
            //Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            
            HorizontalRotation(HorizontalRotationDirection);
            HorizontalLookAtMouse();
            VerticalBarrelRotate();
        }

        private void VerticalBarrelRotate()
        {
            if (barrel != null)
            {
                var barrelVectorRotation = barrel.transform.localRotation.eulerAngles;
                var smoothTime = 1f;


                if (rotateUp && !CompareTag("Helicopter"))
                {

                    barrelVectorRotation.x = -35;

                    //Actually doing the rotation and making it smooth
                    barrel.transform.localRotation = Quaternion.Slerp(barrel.transform.localRotation, barrelQuaternionRotation, smoothTime * Time.deltaTime);
                }
                if (rotateUp && CompareTag("Helicopter"))
                {

                    barrelVectorRotation.x = 35;

                    //Actually doing the rotation and making it smooth
                    barrel.transform.localRotation = Quaternion.Slerp(barrel.transform.localRotation, barrelQuaternionRotation, smoothTime * Time.deltaTime);
                }
                else if (!rotateUp)
                {
                    barrelVectorRotation.x = 0;
                    //Actually doing the rotation and making it smooth
                    barrel.transform.localRotation = Quaternion.Slerp(barrel.transform.localRotation, barrelQuaternionRotation, smoothTime * Time.deltaTime);
                }

                //Clamping the value so it will be between -35 and 0 
                //barrelVectorRotation.x = Mathf.Clamp(barrelVectorRotation.x, -35, 0);

                //Actually doing the rotation and making it smooth
                barrelQuaternionRotation = Quaternion.Euler(barrelVectorRotation);


                // eulerangle for barrel starts at 360, and the negative 
                // rotation is the same as subtracting the rotation from 360
                if (CompareTag("Helicopter"))
                {
                    CheckPositionForHelicopter(barrelVectorRotation);
                }
                else
                {
                    CheckPositionForTank(barrelVectorRotation);
                }
            }
        }

        private void OnVerticalRotation()
        {
            if (!isDown)
            {
                rotateUp = false;
                hasFired = false;
            }
            else if (isDown)
            {
                rotateUp = true;
                hasFired = false;
            }
        }

        private void CheckPositionForTank(Vector3 barrelVectorRotation)
        {
            if (CheckTankBarrelPosition(barrelVectorRotation) && !hasFired && rotateUp)
            {
                FireAtTankUpPosition();
            }
            else if (CheckTankBarrelPosition(barrelVectorRotation) && !hasFired && !rotateUp)
            {
                FireAtTankDownPosition();
            }
        }

        private void CheckPositionForHelicopter(Vector3 barrelVectorRotation)
        {
            if (CheckHeliBarrelPosition(barrelVectorRotation) && !hasFired && rotateUp)
            {
                FireAtHeliUpPosition();
            }
            else if (CheckHeliBarrelPosition(barrelVectorRotation) && !hasFired && !rotateUp)
            {
                FireAtHeliDownPosition();
            }
        }
        private void FireAtTankUpPosition()
        {
            hasFired = true;
            if (firingScript != null)
            {
                firingScript.CmdFire();
            }
            else if (heliFiringScript != null)
            {
                heliFiringScript.CmdFire();
            }
            isDown = false;
        }
        private void FireAtTankDownPosition()
        {
            hasFired = true;
            if (firingScript != null)
            {
                firingScript.CmdFire();
            }
            else if (heliFiringScript != null)
            {
                heliFiringScript.CmdFire();
            }
            isDown = true;
        }
        private void FireAtHeliUpPosition()
        {
            hasFired = true;
            if (firingScript != null)
            {
                firingScript.CmdFire();
            }
            else if (heliFiringScript != null)
            {
                heliFiringScript.CmdFire();
            }
            isDown = false;
        }
        private void FireAtHeliDownPosition()
        {
            hasFired = true;
            if (firingScript != null)
            {
                firingScript.CmdFire();
            }
            else if (heliFiringScript != null)
            {
                heliFiringScript.CmdFire();
            }
            isDown = true;
        }
        private void HorizontalLookAtMouse()
        {
            Debug.Log($"{mousePos.x}");
            
            if (mousePos.x == lastMousePos)
            {
                turret.transform.Rotate(new Vector3(0f, 0f, 0f));
            }
            else if (mousePos.x <= lastMousePos)
            {
                Vector3 lookHere = new Vector3(0, -mousePos.x  * .001f, 0);
                turret.transform.Rotate(lookHere);
            }
            else if (mousePos.x >= lastMousePos)
            {
                Vector3 lookHere = new Vector3(0, mousePos.x * .001f, 0);
                turret.transform.Rotate(lookHere);
            }
            lastMousePos = mousePos.x; 
            
        }
        private void OnHorizontalChange(InputValue input)
        {
            HorizontalRotationDirection = input.Get<float>();
        }

        private void HorizontalRotation(float HorizontalRotationDirection)
        {
            if (turret != null)
            {
                RotateTurret(HorizontalRotationDirection);
            }
        }

        private void RotateTurret(float HorizontalRotationDirection)
        {
            if (HorizontalRotationDirection == 0)
            {
                turret.transform.Rotate(new Vector3(0f, 0f, 0f));
                //AkSoundEngine.PostEvent("Stop_Tank_Rotation", gameObject);
            }
            else if (HorizontalRotationDirection == 1)
            {
                turret.transform.Rotate(new Vector3(0f, 1f, 0f));
                //AkSoundEngine.PostEvent("Play_Tank_Rotation", gameObject);
            }
            else if (HorizontalRotationDirection == -1)
            {
                turret.transform.Rotate(new Vector3(0f, -1f, 0f));
                //AkSoundEngine.PostEvent("Play_Tank_Rotation", gameObject);
            }
        }

        private bool CheckTankBarrelPosition(Vector3 barrelVectorRotation)
        {
            return Mathf.Round(barrel.transform.eulerAngles.x) == (360 + barrelVectorRotation.x);
        }
        private bool CheckHeliBarrelPosition(Vector3 barrelVectorRotation)
        {

            bool isAtAngle = Mathf.Round(barrel.transform.eulerAngles.x) == (barrelVectorRotation.x);
            return isAtAngle;
        }
    }
}
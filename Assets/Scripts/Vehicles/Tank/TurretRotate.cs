using UnityEngine;


namespace Complete
{
    public class TurretRotate : MonoBehaviour
    {
        [SerializeField] GameObject turret;
        [SerializeField] GameObject barrel;
        [SerializeField] Quaternion barrelQuaternionRotation;
        [SerializeField] TankFiring firingScript;
        [SerializeField] HeliFiring heliFiringScript;
        AudioSource audioSource;
        private bool rotateUp = false;
        private bool hasFired = true;// not realy true but prevents fire on start
        private bool isDown = true;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }
        public void Rotate()
        {
            HorizontalTurretRotate();
            VerticalBarrelRotate();
        }

        private void VerticalBarrelRotate()
        {
            if (barrel != null)
            {
                var barrelVectorRotation = barrel.transform.localRotation.eulerAngles;
                var smoothTime = 1f;
                if (Input.GetKey("z") && !isDown)
                {
                    rotateUp = false;
                    hasFired = false;
                }
                if (Input.GetKey("x") && isDown)
                {
                    rotateUp = true;
                    hasFired = false;
                }

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

        private void CheckPositionForTank(Vector3 barrelVectorRotation)
        {
            if (CheckTankBarrelPosition(barrelVectorRotation) && !hasFired && rotateUp)
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
            else if (CheckTankBarrelPosition(barrelVectorRotation) && !hasFired && !rotateUp)
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
        }

        private void CheckPositionForHelicopter(Vector3 barrelVectorRotation)
        {
            if (CheckHeliBarrelPosition(barrelVectorRotation) && !hasFired && rotateUp)
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
            else if (CheckHeliBarrelPosition(barrelVectorRotation) && !hasFired && !rotateUp)
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
        }

        private void HorizontalTurretRotate()
        {
            if (turret != null)
            {
                if (Input.GetKey("e"))
                {
                    turret.transform.Rotate(new Vector3(0f, 1f, 0f));
                    //AkSoundEngine.PostEvent("Play_Tank_Rotation", gameObject);
                }
                if (Input.GetKey("q"))
                {
                    turret.transform.Rotate(new Vector3(0f, -1f, 0f));
                    //AkSoundEngine.PostEvent("Play_Tank_Rotation", gameObject);
                }
                if (Input.GetKeyUp("q"))
                {
                    //AkSoundEngine.PostEvent("Stop_Tank_Rotation", gameObject);
                }
                if (Input.GetKeyUp("e"))
                {
                    //AkSoundEngine.PostEvent("Stop_Tank_Rotation", gameObject);
                }
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
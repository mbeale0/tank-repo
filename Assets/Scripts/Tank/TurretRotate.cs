using UnityEngine;


namespace Complete
{
    public class TurretRotate : MonoBehaviour
    {
        [SerializeField] GameObject turret;
        
        public void Update()
        {
            
        }

        public void Rotate()
        {
            if (Input.GetKey("e"))
                {
                AkSoundEngine.PostEvent("Play_Tank_Rotation", gameObject);
                turret.transform.Rotate(new Vector3(0f, 1f, 0f)); }

            if (Input.GetKey("q"))
            {
                AkSoundEngine.PostEvent("Play_Tank_Rotation", gameObject);
                turret.transform.Rotate(new Vector3(0f, -1f, 0f));
            }
            else
            {
                AkSoundEngine.PostEvent("Stop_Tank_Rotation", gameObject);
            }
        }
    }
}
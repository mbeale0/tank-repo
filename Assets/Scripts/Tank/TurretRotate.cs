using UnityEngine;


namespace Complete
{
    public class TurretRotate : MonoBehaviour
    {
        [SerializeField] GameObject turret;
        

        public void Rotate()
        {
            if (Input.GetKey("e"))
                {
               
                turret.transform.Rotate(new Vector3(0f, 1f, 0f)); }
            
            if (Input.GetKey("q"))
            {
                
                turret.transform.Rotate(new Vector3(0f, -1f, 0f));
                
            }
            if (Input.GetKeyDown("q"))
            {
                AkSoundEngine.PostEvent("Play_Tank_Rotation", gameObject);
            }
            if (Input.GetKeyDown("e"))
            {
                AkSoundEngine.PostEvent("Play_Tank_Rotation", gameObject);
            }
            if (Input.GetKeyUp("q"))
            {
                AkSoundEngine.PostEvent("Stop_Tank_Rotation", gameObject);
            }
            if (Input.GetKeyUp("e"))
            {
                AkSoundEngine.PostEvent("Stop_Tank_Rotation", gameObject);
            }
            
        }
    }
}
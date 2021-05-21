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
        }
    }
}
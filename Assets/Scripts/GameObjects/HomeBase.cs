using UnityEngine;
using Vehicles;

namespace GameObjects
{
    public class HomeBase : MonoBehaviour
    {
        public Transform flagMountTransform;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<TankController>().hasFlag) return;
            var flag = GameObject.FindGameObjectWithTag("Flag");
            flag.transform.SetParent(flagMountTransform);
            flag.transform.position = flagMountTransform.position;
            flag.transform.rotation = flagMountTransform.rotation;
            other.GetComponent<TankController>().hasFlag = false;
        }
    }
}
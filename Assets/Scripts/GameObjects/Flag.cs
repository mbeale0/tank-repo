using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Tank;

namespace Complete {

    public class Flag : NetworkBehaviour
    {


        void Update()
        {

        }

      
        private void OnTriggerEnter(Collider other)
        {
          
            
                var flagMountPoint = FindObjectOfType<TankController>().flagMountPoint;
                gameObject.transform.parent = flagMountPoint.transform;
                gameObject.transform.position = flagMountPoint.transform.position;
                gameObject.transform.rotation = flagMountPoint.transform.rotation;
            


        }
     
}
}
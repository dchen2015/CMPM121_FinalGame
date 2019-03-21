using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Controller : MonoBehaviour
{

    private void Update()
    {
        if (this.transform.position.x > 50 || this. transform.position.z > 50)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject); 
    }
}

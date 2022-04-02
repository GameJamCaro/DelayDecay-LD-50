using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Billboard : MonoBehaviour
{

    public Camera cam;
    public bool useStaticBillboard;

    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        if (useStaticBillboard)
        {
            transform.rotation = cam.transform.rotation;
        }
        else 
        {
            transform.LookAt(cam.transform);
        }

        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}

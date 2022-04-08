using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCam : MonoBehaviour
{
    public Camera cam;

    private void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            cam.gameObject.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.O))
        {
            cam.gameObject.SetActive(false);
        }
    }
}

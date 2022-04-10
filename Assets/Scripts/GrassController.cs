using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassController : MonoBehaviour
{
    SpriteRenderer ren;


    private void Start()
    {
        ren = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Feet"))
        {
            ren.material.SetFloat("speed", 10);
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Feet"))
        {
            ren.material.SetFloat("speed", .5f);
        }
    }
}

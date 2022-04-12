using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassController : MonoBehaviour
{
    SpriteRenderer ren;


    private void Start()
    {
        ren = GetComponent<SpriteRenderer>();
        //StartCoroutine(GrassStart());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Feet"))
        {
            StartCoroutine(WalkingThroughGrass());
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Feet"))
        {
           
            StartCoroutine(LeavingGrass());
        }
    }

   

    IEnumerator GrassStart()
    {
        ren.material.SetFloat("speed", 3);
        yield return new WaitForSeconds(1);
        ren.material.SetFloat("speed", .5f);
    }

    WaitForSeconds wait = new WaitForSeconds(.3f);

    IEnumerator WalkingThroughGrass()
    {
        
        ren.material.SetFloat("speed", 1);
        yield return wait;
        ren.material.SetFloat("speed", .2f);
       

    }

    IEnumerator LeavingGrass()
    {
        yield return wait;
        ren.material.SetFloat("speed", 1);
        yield return wait;
        ren.material.SetFloat("speed", 3);
    }

    
}

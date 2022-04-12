using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 tempPos;
    public float speed = 1;
    float horizontalInput;
    float verticalInput;
  
    SpriteRenderer spriteRen;
    Animator anim;
    float blinkSpeed;
    float runSpeed = 2;

   
    void Start()
    {
        tempPos = transform.position;
        spriteRen = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(BlinkRandomizer());
    }

    
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0).normalized;
        
        if(Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("interact");
            anim.SetBool("running", false);
            anim.speed = runSpeed;
        }

       


        if (horizontalInput == 0  && verticalInput == 0)
        { 
            
            anim.SetBool("running", false);
          //  anim.SetBool("up", false);
            anim.speed = blinkSpeed;

        }
        
        else if(horizontalInput < 0)
        {
           
            anim.SetBool("running", true);
          
            spriteRen.flipX = false;
            anim.speed = runSpeed;
        }
        else if (horizontalInput > 0)
        {
            
            anim.SetBool("running", true);
           
            spriteRen.flipX = true;
            anim.speed = runSpeed;
        }
       
        else if(verticalInput != 0)
        {
           
            anim.SetBool("running", true);
            anim.speed = runSpeed;
        }
       
        else
            anim.speed = runSpeed;

        transform.Translate(direction * speed * Time.deltaTime);
    }



    IEnumerator BlinkRandomizer()
    {
        blinkSpeed = Random.Range(.5f, 3);
        yield return new WaitForSeconds(5);
        StartCoroutine(BlinkRandomizer());
    }
}

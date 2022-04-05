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
    public Sprite[] playerSprites;
    SpriteRenderer spriteRen;
    Animator anim;
    float blinkSpeed;
    float runSpeed = 2;

    // Start is called before the first frame update
    void Start()
    {
        tempPos = transform.position;
        spriteRen = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(BlinkRandomizer());
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0).normalized;
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("interact");
            anim.speed = runSpeed;
        }


        if(horizontalInput == 0  && verticalInput == 0)
        { 
            
            anim.SetBool("running", false);
            anim.SetBool("up", false);
            anim.speed = blinkSpeed;

        }
        else if(horizontalInput < 0)
        {
            anim.SetBool("running", true);
            anim.SetBool("up", false);
            spriteRen.flipX = false;
            anim.speed = runSpeed;
        }
        else if (horizontalInput > 0)
        {
            anim.SetBool("running", true);
            anim.SetBool("up", false);
            spriteRen.flipX = true;
            anim.speed = runSpeed;
        }
        else if(verticalInput != 0)
        {
            anim.SetBool("up", true);
            anim.SetBool("running", false);
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

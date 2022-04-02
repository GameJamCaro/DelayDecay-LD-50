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

    // Start is called before the first frame update
    void Start()
    {
        tempPos = transform.position;
        spriteRen = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0).normalized;
        
        if(horizontalInput == 0)
        {
            spriteRen.sprite = playerSprites[0];
            
        }
        else if(horizontalInput < 0.01f || verticalInput != 0)
        {
            spriteRen.sprite = playerSprites[1];
            spriteRen.flipX = false;
        }
        else if (horizontalInput > 0.01f || verticalInput != 0)
        {
            spriteRen.sprite = playerSprites[1];
            spriteRen.flipX = true;
        }

       
        

        transform.Translate(direction * speed * Time.deltaTime);


       
    }
}

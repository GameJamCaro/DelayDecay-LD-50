using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{
    Vector2 startPos;
    Vector2 tempPos;
    SpriteRenderer ren;

    public float speedLimit = 3;
    float mainSpeed = 1;
    float speed;
    float speed1;
    GameObject player;
    bool closeToPlayer;
    float stepSpeed = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ren = GetComponentInChildren<SpriteRenderer>();
        tempPos = startPos = transform.position;
        StartCoroutine(Drifting());
    }

    // Update is called once per frame
    void Update()
    {
        if (!closeToPlayer)
        {
            tempPos.x += (2 * Time.deltaTime * speed * mainSpeed);
            tempPos.y += (2 * Time.deltaTime * speed1 * mainSpeed);
            transform.position = tempPos;

            if (speed > 0)
                 ren.flipX = true;
            else
                ren.flipX = false;
        }
        else
        {
            float step = stepSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,step);
        }

        if (Vector2.Distance(transform.position, player.transform.position) < 10)
        {
            closeToPlayer = true;
            if (transform.position.x > player.transform.position.x)
            {
                ren.flipX = false;
            }
            else
                ren.flipX = true;
        }
        else
        {
            tempPos = transform.position;
            closeToPlayer = false;
        }
    }
    


    IEnumerator Drifting()
    {
        speed = Random.Range(-speedLimit,speedLimit);
        speed1 = Random.Range(-speedLimit, speedLimit); 
        yield return new WaitForSeconds(Random.Range(3,6));
        speed1 = Random.Range(-speedLimit, speedLimit);
        speed = Random.Range(-speedLimit, speedLimit);
        yield return new WaitForSeconds(Random.Range(5,8));
        StartCoroutine(Drifting());

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed *= -1;
        speed1 *= -1;
    }
}

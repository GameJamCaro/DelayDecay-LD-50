using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{
    Vector2 startPos;
    Vector2 tempPos;

    public float speedLimit = 3;
    float mainSpeed = 1;
    float speed;
    float speed1;


    // Start is called before the first frame update
    void Start()
    {
        tempPos = startPos = transform.position;
        StartCoroutine(Drifting());
    }

    // Update is called once per frame
    void Update()
    {
        tempPos.x += (2 * Time.deltaTime * speed * mainSpeed);
        tempPos.y += (2 * Time.deltaTime * speed1 * mainSpeed);
        transform.position = tempPos;

        if(speed > 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else
            GetComponentInChildren<SpriteRenderer>().flipX = false;

    }


    IEnumerator Drifting()
    {
        speed = Random.Range(-speedLimit,speedLimit);
        speed1 = Random.Range(-speedLimit, speedLimit); ;
        yield return new WaitForSeconds(Random.Range(3, 6));
        speed1 = Random.Range(-speedLimit, speedLimit); ;
        speed = Random.Range(-speedLimit, speedLimit); ;
        yield return new WaitForSeconds(Random.Range(5, 8));
        StartCoroutine(Drifting());

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed *= -1;
        speed1 *= -1;
    }
}

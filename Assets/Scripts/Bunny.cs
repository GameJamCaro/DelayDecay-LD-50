using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : MonoBehaviour
{
    Animator anim;
    GameObject player;
    float stepSpeed = 3;
    bool hoppingAway;
    float xOff;
    float yOff;
    SpriteRenderer ren;

    // Start is called before the first frame update
    void Start()
    {
        ren = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        StartCoroutine(AnimationVariation());
        hoppingAway = true;
    }


    bool stop;
    bool flipOnce;

    private void Update()
    {
        if (!stop)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 10 && hoppingAway)
            {
                anim.SetBool("hopping", true);
                stepSpeed = Random.Range(3, 7);
                float step = stepSpeed * Time.deltaTime;
                Vector2 target;
                target.x = player.transform.position.x + xOff;
                target.y = player.transform.position.y + yOff;
                if ((transform.position.x - target.x) > 0.5f)
                {
                    ren.flipX = true;
                }
                if ((transform.position.x - target.x) < -.5f)
                {
                    ren.flipX = false;
                }
                transform.position = Vector2.MoveTowards(transform.position, target, -step);
                StartCoroutine(HopAndWait());
            }
        }
        else
        {
            float step = 1 * Time.deltaTime;
            if (!flipOnce)
            {
                if (ren.flipX)
                {
                    ren.flipX = false;
                }
                else
                    ren.flipX = true;

                flipOnce = true;
            }
            Vector2 target;
            target.x = lake.transform.position.x + xOff;
            target.y = lake.transform.position.y + yOff;
            transform.position = Vector2.MoveTowards(transform.position, target, -step);
            anim.SetBool("hopping", true);
        }

    }


    GameObject lake;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Border"))
        {
            lake = collision.gameObject;
            stop = true;
           

        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            StartCoroutine(ResetStop());

        }

        
    }

    IEnumerator ResetStop()
    {
        yield return new WaitForSeconds(2);
        stop = false;
    }


    IEnumerator AnimationVariation()
    {
        anim.speed = Random.Range(.5f,1);
        yield return new WaitForSeconds(Random.Range(3,9));
        StartCoroutine(AnimationVariation());
   }

    IEnumerator HopAndWait()
    {
        xOff = Random.Range(.5f, 2);
        yOff = Random.Range(.5f, 2);
        yield return new WaitForSeconds(2);
        hoppingAway = false;
        anim.SetBool("hopping", false);
        if(xOff > 1)
        {
            ren.flipX = true;
        }
        yield return new WaitForSeconds(Random.Range(2,4));
        hoppingAway = true;
    }

    



}

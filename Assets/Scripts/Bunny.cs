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


    private void Update()
    {
        if(Vector2.Distance(transform.position, player.transform.position) < 10 && hoppingAway)
        {
            anim.SetBool("hopping", true);
            stepSpeed = Random.Range(3, 7);
            float step = stepSpeed * Time.deltaTime;
            Vector2 target;
            target.x = player.transform.position.x + xOff;
            target.y = player.transform.position.y + yOff;
            if ((transform.position.x - target.x) > 0  && Vector2.Distance(transform.position, player.transform.position) > 3)
            {
                ren.flipX = true;
            }
            else
            {
                ren.flipX = false;
            }
            transform.position = Vector2.MoveTowards(transform.position, target, -step);
            StartCoroutine(HopAndWait());
        }
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

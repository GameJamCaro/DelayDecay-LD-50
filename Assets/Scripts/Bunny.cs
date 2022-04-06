using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(AnimationVariation());
    }

   IEnumerator AnimationVariation()
    {
        anim.speed = Random.Range(.5f,1);
        yield return new WaitForSeconds(Random.Range(3,9));
        StartCoroutine(AnimationVariation());
    }
}

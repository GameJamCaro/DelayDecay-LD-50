using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    bool atDoor;
    SpriteRenderer spriteRen;
    public Sprite[] doorSprites;

    private void Start()
    {
        spriteRen = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && atDoor)
        {
            StartCoroutine(WaitAndEnter());
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            atDoor = true;
            spriteRen.sprite = doorSprites[1];
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            atDoor = false;
            spriteRen.sprite = doorSprites[0];
        }
    }

    IEnumerator WaitAndEnter()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Shrine");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    bool atDoor;
    SpriteRenderer spriteRen;
    public Color activeColor;
    public HealthManager healthManager;

    AudioSource audioSource;
   

    private void Start()
    {
        spriteRen = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.E)) && atDoor)
        {
            
            StartCoroutine(WaitAndEnter());
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && ((PlayerPrefs.GetInt("Stage") == 0 || healthManager.rabitScore == 5) || PlayerPrefs.GetInt("Stage") == 4))
        {
            atDoor = true;
            spriteRen.color = activeColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            atDoor = false;
            spriteRen.color = Color.white;
        }
    }

    IEnumerator WaitAndEnter()
    {
        audioSource.Play();
        PlayerPrefs.SetInt("Health", healthManager.currentHealth);
        yield return new WaitForSeconds(.3f);
        SceneManager.LoadScene("Shrine");
    }

}

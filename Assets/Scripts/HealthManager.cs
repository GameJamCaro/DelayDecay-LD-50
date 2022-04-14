using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public Slider HealthMeter;
    public GameObject lostPanel;
    bool atBunny;
    GameObject currentBunny;
   
    public int rabitScore;
    public Image[] rabitIcons;
    public Color atColor;

    bool hurt;


    AudioSource audioSource;

    public AudioClip hurtSound;
    public AudioClip healthSound;
    public AudioClip bunnyCollectSound;
    public AudioClip deathMotive;

    public AudioSource mainMusic;

    EventSystem eSystem;

    
     


    private void Start()
    {
        Time.timeScale = 1;

        audioSource = GetComponent<AudioSource>();
        eSystem = GameObject.FindObjectOfType<EventSystem>();

        ClearRabitIcons();
        if (!PlayerPrefs.HasKey("Health"))
            currentHealth = 7;
        else
            currentHealth = PlayerPrefs.GetInt("Health");

        HealthMeter.value = currentHealth;
    }

    private void Update()
    {
        if((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.E)) && atBunny)
        {
            rabitScore++;
            ClearRabitIcons();
            for (int i = 0; i < rabitScore; i++)
            {
                rabitIcons[i].color = Color.white;
            }
            audioSource.clip = bunnyCollectSound;
            audioSource.Play();
            Destroy(currentBunny);
        }
    }


    public void LoseHealth(int amount)
    {
        if (currentHealth < 1)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            mainMusic.Stop();
            lostPanel.SetActive(true);
            eSystem.SetSelectedGameObject(lostPanel.transform.GetChild(1).gameObject);
        }
        else
        {
            currentHealth -= amount;
            HealthMeter.value = currentHealth;
            audioSource.clip = hurtSound;
            audioSource.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            if (!hurt)
            {
                
                LoseHealth(1);
                HealthMeter.value = currentHealth;
                hurt = true;
                StartCoroutine(ResetHurt());
            }
        }

        if (collision.CompareTag("Health"))
        {
            if (currentHealth < maxHealth)
            {
                currentHealth++;
                audioSource.clip = healthSound;
                audioSource.Play();
                HealthMeter.value = currentHealth;
                Destroy(collision.gameObject);
            }
        }

        if (collision.CompareTag("Rabit"))
        {
            atBunny = true;
            currentBunny = collision.gameObject;
            
            currentBunny.GetComponent<SpriteRenderer>().color = atColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Rabit"))
        {
            atBunny = false;
           
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        }
    }

    


    void ClearRabitIcons()
    {
        foreach (Image icon in rabitIcons)
        {
            if(PlayerPrefs.GetInt("Stage") == 0 || PlayerPrefs.GetInt("Stage") == 4)
            {
                icon.enabled = false;
            }
            else
                icon.color = Color.black;
        }
    }

    IEnumerator ResetHurt()
    {
        yield return new WaitForSeconds(1);
        hurt = false;
    }
}

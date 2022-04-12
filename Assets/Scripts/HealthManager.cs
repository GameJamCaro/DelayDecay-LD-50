using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
     


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        ClearRabitIcons();
        if (!PlayerPrefs.HasKey("Health"))
            currentHealth = 7;
        else
            currentHealth = PlayerPrefs.GetInt("Health");

        HealthMeter.value = currentHealth;
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1") && atBunny)
        {
            rabitScore++;
            ClearRabitIcons();
            for (int i = 0; i < rabitScore; i++)
            {
                rabitIcons[i].enabled = true;
            }
            Destroy(currentBunny);
        }
    }


    public void LoseHealth(int amount)
    {
        if (currentHealth < 1)
        {
            lostPanel.SetActive(true);
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
                Debug.Log(currentHealth);
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
            icon.enabled = false;
        }
    }

    IEnumerator ResetHurt()
    {
        yield return new WaitForSeconds(1);
        hurt = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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


    private void Start()
    {
        ClearRabitIcons();

        currentHealth = maxHealth;
        HealthMeter.value = currentHealth;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && atBunny)
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
            //lostPanel.SetActive(true);
        }
        else
        {
            currentHealth -= amount;
            HealthMeter.value = currentHealth;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log(currentHealth);
            LoseHealth(1);
            HealthMeter.value = currentHealth;
        }

        if (collision.CompareTag("Health"))
        {
            currentHealth++;
            HealthMeter.value = currentHealth;
            Destroy(collision.gameObject);
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
}

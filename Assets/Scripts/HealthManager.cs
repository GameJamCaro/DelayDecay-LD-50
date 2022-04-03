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
    int youthScore;
    public TextMeshProUGUI score;


    private void Start()
    {
        currentHealth = maxHealth;
        HealthMeter.value = currentHealth;
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

    private void OnTriggerEnter2D(Collider2D collision)
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
    }
}

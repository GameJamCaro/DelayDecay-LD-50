using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Tilemaps;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    
    int tempTime;

    public Tilemap[] tilemaps;

    public Color[] decayColors;

    public GameObject decayPanel;
    

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("Location: " + PlayerPrefs.GetInt("LocationID"));
        Debug.Log("Stage: " + PlayerPrefs.GetInt("Stage"));
        if (PlayerPrefs.HasKey("Stage"))
        {
            if (PlayerPrefs.GetInt("Stage") == 1)
            {
                tempTime = 60;
            }

            if (PlayerPrefs.GetInt("Stage") == 2)
            {
                tempTime = 30;
            }
           

            if (PlayerPrefs.GetInt("Stage") > 0)
            {
                timeText.text = tempTime + " seconds";
                StartCoroutine(CountDown());
            }
        }
    }

    
    public void ResetStage()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    

    IEnumerator CountDown()
    {
            yield return new WaitForSeconds(1);
            if (tempTime > 0)
            {
                tempTime--;
                timeText.text = tempTime + " seconds";
                StartCoroutine(CountDown());
            }
            else
            {
            StartCoroutine(TimesUp());
            }


    }

    private IEnumerator TimesUp()
    {
        timeText.text = "Time's up";
        yield return new WaitForSeconds(3);
        foreach(Tilemap map in tilemaps)
        {
            map.color = decayColors[0];
        }
        yield return new WaitForSeconds(2);
        foreach (Tilemap map in tilemaps)
        {
            map.color = decayColors[1];
        }
        yield return new WaitForSeconds(1);
        foreach (Tilemap map in tilemaps)
        {
            map.color = decayColors[2];
        }
        yield return new WaitForSeconds(1.5f);
        decayPanel.SetActive(true);
    }
}

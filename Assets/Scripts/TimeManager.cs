using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Tilemaps;

public class TimeManager : MonoBehaviour
{
    public GameObject timeText;
    
    int tempTime;

    public Tilemap[] tilemaps;

    public Color[] decayColors;

    public GameObject decayPanel;
    

   
    void Start()
    {

        Cursor.visible = false;

        if (PlayerPrefs.HasKey("Stage"))
        {
            timeText.SetActive(true);
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
                timeText.GetComponentInChildren<TextMeshProUGUI>().text = tempTime + " sec";
                StartCoroutine(CountDown());
            }
        }
        else
            timeText.SetActive(false);
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
                timeText.GetComponentInChildren<TextMeshProUGUI>().text = tempTime + " sec";
                StartCoroutine(CountDown());
            }
            else
            {
            StartCoroutine(TimesUp());
            }
    }


    private IEnumerator TimesUp()
    {
        timeText.GetComponentInChildren<TextMeshProUGUI>().text = "Time's up";
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TimeManager : MonoBehaviour
{
    public GameObject timeText;
    
    int tempTime;

    public GameObject grassParent;

    public Color[] decayColors;

    public GameObject decayPanel;

    EventSystem eSystem;
    public AudioSource mainMusic;

    public Camera cam;

   
    void Start()
    {
        eSystem = GameObject.FindObjectOfType<EventSystem>();

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
                tempTime = 50;
            }

            if (PlayerPrefs.GetInt("Stage") == 3)
            {
                tempTime = 40;
            }

            if (PlayerPrefs.GetInt("Stage") == 4)
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
        mainMusic.Stop();
        timeText.GetComponentInChildren<TextMeshProUGUI>().text = "Time's up";
       
        for (int i = 0; i < grassParent.transform.childCount; i++)
        {
            grassParent.transform.GetChild(i).GetComponent<SpriteRenderer>().material.SetColor("_Color", decayColors[0]);
            grassParent.transform.GetChild(i).GetComponent<SpriteRenderer>().material.SetColor("_Color1", decayColors[1]);
            cam.backgroundColor = decayColors[0];
        }
        yield return new WaitForSeconds(2);
        for (int i = 0; i < grassParent.transform.childCount; i++)
        {
            grassParent.transform.GetChild(i).GetComponent<SpriteRenderer>().material.SetColor("_Color", decayColors[1]);
            grassParent.transform.GetChild(i).GetComponent<SpriteRenderer>().material.SetColor("_Color1", decayColors[2]);
            cam.backgroundColor = decayColors[2];
        }
        yield return new WaitForSeconds(3);
        for (int i = 0; i < grassParent.transform.childCount; i++)
        {
            grassParent.transform.GetChild(i).GetComponent<SpriteRenderer>().material.SetColor("_Color", decayColors[2]);
            grassParent.transform.GetChild(i).GetComponent<SpriteRenderer>().material.SetColor("_Color1", decayColors[3]);
            cam.backgroundColor = decayColors[3];
        }
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
        Cursor.visible = true;
        decayPanel.SetActive(true);
        eSystem.SetSelectedGameObject(decayPanel.transform.GetChild(1).gameObject);
    }
}

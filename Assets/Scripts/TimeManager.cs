using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    
    int tempTime;
    

    // Start is called before the first frame update
    void Start()
    {
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

            }
    }
}

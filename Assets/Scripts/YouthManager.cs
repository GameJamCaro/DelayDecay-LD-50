using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouthManager : MonoBehaviour
{
    public int maxYouth;
    public int currentYouth;
    public Slider YouthMeter;
    public GameObject lostPanel;


    private void Start()
    {
        currentYouth = maxYouth;
        YouthMeter.value = currentYouth;
    }



    public void LoseYouth(int amount)
    {
        if (currentYouth < 0)
        {
            lostPanel.SetActive(true);
        }
        else
        {
            currentYouth -= amount;
            YouthMeter.value = currentYouth;
        }
    }
}

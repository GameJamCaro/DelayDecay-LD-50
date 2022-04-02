using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouthManager : MonoBehaviour
{
    public int maxYouth;
    public int currentYouth;

    private void Start()
    {
        currentYouth = maxYouth;
    }


    public void LoseYouth(int amount)
    {
        currentYouth -= amount;
    }
}

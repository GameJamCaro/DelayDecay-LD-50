using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabitPlacement : MonoBehaviour
{
    public Vector2[] locations;
    public GameObject rabits;


    private void Start()
    {
        if(PlayerPrefs.HasKey("LocationID"))
            Instantiate(rabits, locations[PlayerPrefs.GetInt("LocationID")], Quaternion.identity);
    }
}

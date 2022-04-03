using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public TextAsset[] inkFiles;
    public TMP_FontAsset[] fonts;
    public TextMeshProUGUI eventText;
    public TextMeshProUGUI resultText;

    public int stageID;

    Story story;
    public float probabilty;
    public TextMeshProUGUI diceText;

    public GameObject options;
    public GameObject fortuneWheel;

   

    public string[] locations;
   

    string[] currentResults;

    YouthManager youthManager;

   


    private void Awake()
    {
        story = new Story(inkFiles[0].text);
       
        youthManager = GetComponent<YouthManager>();
    }

    private void Start()
    {
        /*
        story.Continue();
        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            buttons[i].SetActive(true);
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = story.currentChoices[i].text;
        }
        */
    }

    private void Update()
    {
        /*
        if(story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                buttons[i].SetActive(true);
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = story.currentChoices[i].text;
            }
            
        }
        else
        {
            options.SetActive(false);
            fortuneWheel.SetActive(true);
        }
        
        switch(stageID)
        {
            case 0:
                eventText.font = fonts[stageID];
                break;
            
        }
        */
    }

    public void RollDice()
    {
        probabilty = Random.Range(10,30);
        diceText.text = probabilty.ToString();
        fortuneWheel.transform.GetChild(1).gameObject.SetActive(true);
        StartCoroutine(RotateResults());

    }

    public void MakeChoice1()
    {
        StopAllCoroutines();
        fortuneWheel.SetActive(true);
        fortuneWheel.transform.GetChild(1).gameObject.SetActive(false);
        fortuneWheel.transform.GetChild(2).gameObject.SetActive(false);
        diceText.text = "Wheel of fortune";
      
        youthManager.LoseYouth(3);

    }

   

   

    IEnumerator RotateResults()
    {
        int i = 0;
        WaitForSeconds wait = new WaitForSeconds(.4f);
        for (int j = 0; j < probabilty; j++)
        {
           
            yield return wait;
            fortuneWheel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = locations[i];

            if (i < locations.Length-1)
                i++;
            else
                i = 0;
            
        }
       





    }

    public void GoToMiniGame()
    {
        PlayerPrefs.SetInt("YouthScore", youthManager.currentYouth);
        SceneManager.LoadScene(2);
    }
}

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
    public TextMeshProUGUI godText;
    public TextMeshProUGUI youText;
    public TextMeshProUGUI resultText;

    public int stageID;

    Story story;
    public float probabilty;
    public TextMeshProUGUI diceText;

    public GameObject options;
    public GameObject fortuneWheel;

    int lineCounter;
   

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
        fortuneWheel.SetActive(false);

     
    }

    


    public void ContinueConversation()
    {

        if (story.canContinue)
        {
            lineCounter++;
            if (lineCounter % 2 == 1)
            {
                godText.text = story.Continue();
            }
            else
            {
                youText.text = story.Continue();
            }
        }
        else
            fortuneWheel.SetActive(true);

        


    }

   
    public void RollDice()
    {
        probabilty = Random.Range(15,30);
        diceText.text = "Your lucky number is " + probabilty;
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
        float waitTime = .7f;
        bool once = false;
        bool once1 = false;
        bool once2 = false;
        for (int j = 0; j < probabilty; j++)
        {
            yield return new WaitForSeconds(waitTime);
            fortuneWheel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = locations[i];

            if (i < locations.Length-1)
                i++;
            else
                i = 0;
            if(j > probabilty / 10 && !once)
            {
                waitTime -= .2f;
                once = true;
            }
            if (j > probabilty /5 && !once1)
            {
                waitTime -= .2f;
                once1 = true;
            }
            if(j > (probabilty /4)*3 && !once2) 
            {
                waitTime = .7f;
                once2 = true;
            }
        }
       





    }

    public void GoToMiniGame()
    {
        PlayerPrefs.SetInt("YouthScore", youthManager.currentYouth);
        SceneManager.LoadScene(2);
    }
}

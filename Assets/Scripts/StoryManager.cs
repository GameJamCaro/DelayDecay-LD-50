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

    public GameObject[] buttons;

    public string[] results1;
    public string[] results2;
    public string[] results3;

    string[] currentResults;

   


    private void Awake()
    {
        story = new Story(inkFiles[0].text);
        fortuneWheel.SetActive(false);
    }

    private void Start()
    {
        story.Continue();
        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            buttons[i].SetActive(true);
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = story.currentChoices[i].text;
        }
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
        diceText.text = "Wheel of fortune";
        currentResults = results1;

    }

    public void MakeChoice2()
    {

        StopAllCoroutines();
        fortuneWheel.SetActive(true);
        fortuneWheel.transform.GetChild(1).gameObject.SetActive(false);
        diceText.text = "Wheel of fortune";
        currentResults = results2;

    }

    public void MakeChoice3()
    {
        StopAllCoroutines();
        fortuneWheel.SetActive(true);
        fortuneWheel.transform.GetChild(1).gameObject.SetActive(false);
        diceText.text = "Wheel of fortune";
        currentResults = results3;

    }

    IEnumerator RotateResults()
    {
        int i = 0;
        WaitForSeconds wait = new WaitForSeconds(.4f);
        for (int j = 0; j < probabilty; j++)
        {
           
            yield return wait;
            fortuneWheel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentResults[i];

            if (i < results1.Length-1)
                i++;
            else
                i = 0;
            
        }
        yield return new WaitForSeconds(5);
        fortuneWheel.transform.GetChild(2).gameObject.SetActive(true);





    }

    public void GoToMiniGame()
    {
        SceneManager.LoadScene(2);
    }
}

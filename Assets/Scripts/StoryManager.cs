using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;

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


    private void Awake()
    {
        story = new Story(inkFiles[0].text);
        fortuneWheel.SetActive(false);
    }

    private void Update()
    {
        if(story.currentChoices.Count > 0)
        {

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
    }

    public void RollDice()
    {
        probabilty = Random.value;
        diceText.text = probabilty.ToString();
    }
}

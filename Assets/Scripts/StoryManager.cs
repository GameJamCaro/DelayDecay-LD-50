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
    AudioSource audioSource;
    public AudioClip[] talkSounds;
    public AudioClip coinSound;

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

    string currentText;



   


    private void Awake()
    {
        story = new Story(inkFiles[0].text);
       
        youthManager = GetComponent<YouthManager>();
    }

    private void Start()
    {
        fortuneWheel.SetActive(false);
        audioSource = GetComponent<AudioSource>();

     
    }

    


    public void ContinueConversation()
    {

        if (story.canContinue)
        {
            lineCounter++;
            currentText = story.Continue();
           
            if (lineCounter % 2 == 1)
            {
                
                
                StartCoroutine(TypeSentence(currentText, godText,0));

            }
            else
            {
                
                
                StartCoroutine(TypeSentence(currentText, youText,1));
            }
        }
        else
        {
            fortuneWheel.SetActive(true);
            PlayerPrefs.SetInt("Stage", PlayerPrefs.GetInt("Stage") + 1);
        }


    }

    public IEnumerator TypeSentence(string sentence, TextMeshProUGUI textElement, int talkerID)
    {
        textElement.text = "";
        
        foreach (char letter in sentence.ToCharArray())
        {
            if(talkerID == 0)
            {
                audioSource.clip = talkSounds[0];
            }
            else
            {
                audioSource.clip = talkSounds[Random.Range(1, 3)];
            }

            audioSource.pitch = Random.Range(.8f, 1.2f);
            audioSource.Play();
            textElement.text += letter;
            float time = Random.Range(0.05f, 0.1f);
            yield return new WaitForSeconds(time);
        }
    }


    public void RollDice()
    {
        audioSource.clip = coinSound;
        audioSource.loop = true;
        audioSource.Play();
        probabilty = Random.Range(10,31);
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
        audioSource.loop = false;
        audioSource.Stop();
       





    }

    public void GoToGame()
    {
       
        SceneManager.LoadScene("World");
    }
}

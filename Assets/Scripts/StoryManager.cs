using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
    public GameObject continueButton;

    int lineCounter;

    public string[] locations;
   
    string currentText;


    bool talkOver;

    public GameObject exit;

    public GameObject dialogPanel;
    public GameObject endPanel;

    public EventSystem eSystem;
    public AudioSource mainMusic;

    public TMP_FontAsset neutralFont;



    private void Start()
    {
        story = new Story(inkFiles[PlayerPrefs.GetInt("Stage")].text);

        fortuneWheel.SetActive(false);
        audioSource = GetComponent<AudioSource>();

        Cursor.visible = true;
    }

    


    public void ContinueConversation()
    {

        if (story.canContinue)
        {
            lineCounter++;
            if (lineCounter % 2 == 1)
            {
                StopAllCoroutines();

                if(lineCounter > 1)
                   youText.text = currentText;
                
                currentText = "";
                currentText = story.Continue();
                StartCoroutine(TypeSentence(currentText, godText,0));
            }
            else
            {
                StopAllCoroutines();

                godText.text = currentText;
                currentText = "";
                currentText = story.Continue();
                StartCoroutine(TypeSentence(currentText, youText,1));
            }
        }
        else
        {
            fortuneWheel.SetActive(true);
            talkOver = true;
            
            eSystem.SetSelectedGameObject(fortuneWheel.transform.GetChild(0).gameObject);
            continueButton.SetActive(false);

            if (PlayerPrefs.GetInt("Stage") == 4)
            {
                
                fortuneWheel.SetActive(false);
             
                endPanel.SetActive(true);
                eSystem.SetSelectedGameObject(endPanel.transform.GetChild(1).gameObject);
                mainMusic.Stop();
            }
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

    bool once;

    public void RollDice()
    {
        if (!once)
        {
            StopAllCoroutines();
            if (lineCounter % 2 == 1)
                godText.text = currentText;
            if (lineCounter % 2 == 0)
                youText.text = currentText;
            audioSource.clip = coinSound;
            audioSource.loop = true;
            audioSource.Play();
            probabilty = Random.Range(10, 20);
            
            
            StartCoroutine(RotateResults());
            once = true;
        }
    }

   

   

   

    IEnumerator RotateResults()
    {
        int i = 0;
        float waitTime = .5f;
        bool once = false;
        bool once1 = false;
        bool once2 = false;
        for (int j = 0; j < probabilty; j++)
        {
            yield return new WaitForSeconds(waitTime);
            
          
            if (i < locations.Length-1)
                i++;
            else
                i = 0;
            if(j > probabilty/10 && !once)
            {
                waitTime -= .2f;
                once = true;
            }
            if (j > probabilty/5 && !once1)
            {
                waitTime -= .1f;
                once1 = true;
            }
            if(j > (probabilty /5)*4 && !once2) 
            {
                waitTime = .5f;
                once2 = true;
            }
            if (i == PlayerPrefs.GetInt("LocationID"))
            {
                if (i < locations.Length-1)
                    i += 1;
                else
                    i = 1;
            }
            diceText.font = neutralFont;
            diceText.text = locations[i];
            
        }
        if (PlayerPrefs.GetInt("Stage") == 3)
        {
            diceText.text = "There are no more bunnies";
            story = new Story(inkFiles[4].text);
            continueButton.SetActive(true);
            eSystem.SetSelectedGameObject(continueButton);
        }
        else 
        {
            exit.SetActive(true);
        }
        PlayerPrefs.SetInt("LocationID", i);
        audioSource.loop = false;
        audioSource.Stop();
      
       
       







    }

    public void GoToGame()
    {
        if (talkOver)
            PlayerPrefs.SetInt("Stage", PlayerPrefs.GetInt("Stage") + 1);


        SceneManager.LoadScene("World");
    }
}

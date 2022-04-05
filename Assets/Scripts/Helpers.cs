using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Helpers 
{
    public static IEnumerator TypeSentence(string sentence, TextMeshProUGUI textElement)
    {
        textElement.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            textElement.text += letter;
            float time = Random.Range(0.01f, 0.1f);
            yield return new WaitForSeconds(time);
        }
    }


    public static IEnumerator WaitAndDeactivate(float time, GameObject go)
    {
        yield return new WaitForSeconds(time);
        go.SetActive(false);

    }
}

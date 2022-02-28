using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextBlink : MonoBehaviour
{
    Text flashingText;
                       
    void Start ()
    { 
        flashingText = GetComponent<Text> (); 
        StartCoroutine (BlinkText()); 
    } 
    
    public IEnumerator BlinkText()
    { 
        while (true) 
        { 
            flashingText.text = "Are you Ready?"; 
            yield return new WaitForSeconds (0.7f); 

            flashingText.text = ""; 
            yield return new WaitForSeconds (0.7f); 
        } 
    }

}

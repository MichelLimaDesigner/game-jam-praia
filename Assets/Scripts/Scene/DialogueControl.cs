using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj;
    public Image profile;
    public Text speechText;
    public Text actorNameText;

    [Header("Settings")]
    public float typingSpeed;
    private string[] sentences;
    private int index;

    public void Speech(Sprite p, string[] txt, string actorName){
        dialogueObj.SetActive(true);
        profile.sprite = p;
        sentences = txt;
        actorNameText.text = actorName;
        DisplaySentence();
    }

    IEnumerator TypeSentence(){
        foreach(char letter in sentences[index].ToCharArray()){
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

     public void NextSentence(){
        if(speechText.text == sentences[index]){
            if(index < sentences.Length - 1){
                index++;
                DisplaySentence(); // Chamamos a função para exibir todo o texto imediatamente
            }else{
                speechText.text = "";
                index = 0;
            }
        }
    } 

     // Função para exibir todo o texto de uma vez
    private void DisplaySentence(){
        if (index < sentences.Length){
            speechText.text = sentences[index];
        }else{
            dialogueObj.SetActive(false);
        }
    }

}

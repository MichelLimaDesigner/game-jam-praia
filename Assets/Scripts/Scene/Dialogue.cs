using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Sprite profile;
    public string[] speechText;
    public string actorName;

    private DialogueControl dc;

    public LayerMask playerLayer;
    public float radious;

    bool onRadious;

    private void Start(){
        dc = FindObjectOfType<DialogueControl>();
    }

    private void FixedUpdate(){
        Interact();
    }

    private void Update(){
        if(onRadious){
            dc.Speech(profile, speechText, actorName);
        }
    }

    public void Interact(){
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radious, playerLayer);
        if(hit != null){
            onRadious = true;
        }else{
            onRadious = false;
            dc.dialogueObj.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(transform.position, radious);
    }
}

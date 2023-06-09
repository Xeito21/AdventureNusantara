using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogueScript;
    private bool isDetected;



    private void Update()
    {
        if (isDetected && Input.GetKeyDown(KeyCode.E))
        {
            dialogueScript.StartDialogue();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isDetected = true;
            dialogueScript.ToggleIndicator(isDetected);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isDetected = false;
            dialogueScript.ToggleIndicator(isDetected);
            dialogueScript.EndDialogue();
        }
    }
}

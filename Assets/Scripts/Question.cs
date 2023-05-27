using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    public string question;
    public int answer = 0;
    public string[] multiChoice;
    public GameObject quizUI;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerManager.Instance.GetQuestion();
            gameObject.SetActive(false);
            quizUI.SetActive(true);
        }
    }
}

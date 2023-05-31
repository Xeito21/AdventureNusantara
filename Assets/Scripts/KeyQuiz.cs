using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyQuiz : MonoBehaviour
{
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

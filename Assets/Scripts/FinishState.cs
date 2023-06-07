using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishState : MonoBehaviour
{
    [Header("Panel")]
    public GameObject finishPanel;

    [Header("Stars")]
    [SerializeField] private GameObject[] starsObject;

    [Header("Teks")]
    [SerializeField] private TextMeshProUGUI scoreFinish;

    [Header("References")]
    public QuestionManager questionManager;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            finishPanel.SetActive(true);
            UpdateStars();
            UpdateScore();
        }
    }

    private void UpdateStars()
    {
        int score = questionManager.scorePlayer;

        if (score > 2000)
        {
            ActivateStars(3);
        }
        else if (score >= 1000)
        {
            ActivateStars(2);
        }
        else
        {
            ActivateStars(1);
        }
    }

    private void ActivateStars(int count)
    {
        for (int i = 0; i < starsObject.Length; i++)
        {
            starsObject[i].SetActive(i < count);
        }
    }

    private void UpdateScore()
    {
        scoreFinish.text = questionManager.scorePlayer.ToString();
    }
}

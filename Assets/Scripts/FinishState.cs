using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishState : MonoBehaviour
{
    [Header("Panel")]
    public GameObject finishPanel;
    [SerializeField] private TMP_Text scoreHasil;


    //[Header("Stars")]
    //[SerializeField] private GameObject[] starsObject;

    [Header("References")]
    public QuestionManager questionManager;
    public PlayerManager playerManager;
    private ScoreQuiz scoreQuiz;


    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        scoreQuiz = FindObjectOfType<ScoreQuiz>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.Play("Finish");
            finishPanel.SetActive(true);
            scoreHasil.text = scoreQuiz.scoreQuizPlayer.ToString();
            scoreQuiz.UpdateStars();
            SavePlayerStatus();
        }
    }
    public void SavePlayerStatus()
    {
        PlayerPrefs.SetInt("JumlahCoin", playerManager.jumlahCoin);
        PlayerPrefs.SetInt("LevelForest", scoreQuiz.scoreQuizPlayer);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishState : MonoBehaviour
{
    [Header("Panel")]
    public GameObject finishPanel;
    [SerializeField] private TMP_Text scoreHasil;
    [SerializeField] private TMP_Text warnKeyText;
    public float displayDuration = 3f; 


    [Header("References")]
    public QuestionManager questionManager;
    public PlayerManager playerManager;
    private ScoreQuiz scoreQuiz;

    private Coroutine textDisplayCoroutine; 

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        scoreQuiz = FindObjectOfType<ScoreQuiz>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (playerManager.jumlahKey == 3)
            {
                AudioManager.Instance.Play("Finish");
                finishPanel.SetActive(true);
                scoreHasil.text = scoreQuiz.scoreQuizPlayer.ToString();
                scoreQuiz.UpdateStars();
                SavePlayerStatus();
            }
            else
            {
                AudioManager.Instance.Play("Warn");
                ShowWarnKeyText();
            }
        }
    }

    private void ShowWarnKeyText()
    {
        if (textDisplayCoroutine != null)
            StopCoroutine(textDisplayCoroutine);
        warnKeyText.gameObject.SetActive(true);
        textDisplayCoroutine = StartCoroutine(HideTextAfterDuration());
    }

    private IEnumerator HideTextAfterDuration()
    {
        yield return new WaitForSeconds(displayDuration);
        warnKeyText.gameObject.SetActive(false);
    }

    public void SavePlayerStatus()
    {
        PlayerPrefs.SetInt("JumlahCoin", playerManager.jumlahCoin);
        PlayerPrefs.SetInt("LevelForest", scoreQuiz.scoreQuizPlayer);
    }
}

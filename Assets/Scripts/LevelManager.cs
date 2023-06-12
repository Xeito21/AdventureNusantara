using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button[] lvlButtons;
    public GameObject[] starsObjectsForest;
    public GameObject[] starsObjectsMount;

    [SerializeField] private TMP_Text scoreForestText;
    [SerializeField] private TMP_Text scoreMountText;

    private ScoreQuiz scoreQuiz;


    private void Awake()
    {
        scoreQuiz = FindObjectOfType<ScoreQuiz>();
    }

    void Start()
    {

        int levelAt = PlayerPrefs.GetInt("levelAt", 11);

        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 11 > levelAt)
            {
                lvlButtons[i].interactable = false;
            }
        }
        int scoreForest = PlayerPrefs.GetInt("LevelForest", 0);
        int scoreMount = PlayerPrefs.GetInt("LevelMount", 0);
        scoreForestText.text = "Score : " + scoreForest.ToString();
        scoreMountText.text = "Score : " + scoreMount.ToString();
        scoreQuiz.scoreQuizPlayer = scoreForest;
        scoreQuiz.scoreQuizPlayer = scoreMount;
        UpdateStarsForest(scoreForest);
        UpdateStarsMount(scoreMount);
    }

    private void UpdateStarsForest(int score)
    {
        for (int i = 0; i < starsObjectsForest.Length; i++)
        {
            if (i < score)
            {
                starsObjectsForest[i].SetActive(true); 
            }
            else
            {
                starsObjectsForest[i].SetActive(false); 
            }
        }
    }

    private void UpdateStarsMount(int score)
    {
        for (int i = 0; i < starsObjectsMount.Length; i++)
        {
            if (i < score)
            {
                starsObjectsMount[i].SetActive(true);
            }
            else
            {
                starsObjectsMount[i].SetActive(false);
            }
        }
    }

}
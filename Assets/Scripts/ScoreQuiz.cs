using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreQuiz : MonoBehaviour
{
    [SerializeField] private GameObject[] starsObject;
    public int scoreQuizPlayer;

    public void UpdateStars()
    {
        int score = scoreQuizPlayer;

        if (score > 2000)
        {
            ActivateStars(3);
        }
        else if (score >= 1000)
        {
            ActivateStars(2);
        }
        else if (score > 500)
        {
            ActivateStars(1);
        }
        else
        {
            ActivateStars(0);
        }
    }

    private void ActivateStars(int count)
    {
        for (int i = 0; i < starsObject.Length; i++)
        {
            starsObject[i].SetActive(i < count);
        }
    }
}

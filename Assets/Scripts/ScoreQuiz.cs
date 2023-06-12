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

        switch (score)
        {
            case > 2000:
                ActivateStars(3);
                break;
            case >= 1000:
                ActivateStars(2);
                break;
            case > 500:
                ActivateStars(1);
                break;
            default:
                break;
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

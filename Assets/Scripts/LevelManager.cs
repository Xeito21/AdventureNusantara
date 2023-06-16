using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button[] lvlButtons;
    public GameObject[] starGameObjectsForest;
    public GameObject[] starGameObjectsMount;

    [SerializeField] private TMP_Text scoreForestText;
    [SerializeField] private TMP_Text scoreMountText;

    private void Start()
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

        UpdateStars(starGameObjectsForest, scoreForest);
        UpdateStars(starGameObjectsMount, scoreMount);
    }

    private void UpdateStars(GameObject[] starGameObjects, int score)
    {
        int starCount = PerhitunganBintang(score);

        for (int i = 0; i < starGameObjects.Length; i++)
        {
            starGameObjects[i].SetActive(i < starCount);
        }
    }

    private int PerhitunganBintang(int score)
    {
        if (score >= 2000)
        {
            return 3;
        }
        else if (score >= 1000)
        {
            return 2;
        }
        else if (score >= 500)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}

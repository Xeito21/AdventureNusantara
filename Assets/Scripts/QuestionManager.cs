using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class QuestionManager : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] private int scoreCounter;
    [SerializeField] private int scorePlayer = 500;
    [SerializeField] private int pertanyaanSekarang;
    [SerializeField] private int jawabanBenarCounter = 0;
    private int totalPertanyaan = 0;
    private int currentIndex = 0;


    [Header("List Pertanyaan dan Jawaban")]
    [SerializeField] private List<TanyaDanJawab> originalJnA;
    [SerializeField] private List<TanyaDanJawab> JnA;

    [Header("GameObject")]
    [SerializeField] private GameObject quizPanel;
    [SerializeField] private GameObject gameOverQuizPanel;
    [SerializeField] private GameObject[] opsi;
    [SerializeField] private GameObject[] keyObject;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI[] pesanHasil;
    [SerializeField] private TextMeshProUGUI scoreHasil;
    [SerializeField] private TextMeshProUGUI hasilJawaban;
    [SerializeField] private TextMeshProUGUI pertanyaanText;

    [Header("References")]
    public PlayerManager playerManager;

    private void Start()
    {
        totalPertanyaan = JnA.Count;
        currentIndex = 0;
        originalJnA = new List<TanyaDanJawab>(JnA);
        gameOverQuizPanel.SetActive(false);
        generatePertanyaan();
    }

    public void GameOverQuiz()
    {
        gameOverQuizPanel.SetActive(true);
        quizPanel.SetActive(false);

        if (jawabanBenarCounter == 3)
        {
            hasilJawaban.text = scoreCounter + "/" + jawabanBenarCounter + "\nKamu benar Semua!";
        }
        else
        {
            hasilJawaban.text = scoreCounter + "/" + jawabanBenarCounter + "\nBerlatih lagi!";
        }
        int randomIndex = Random.Range(0, pesanHasil.Length);

        for (int i = 0; i < pesanHasil.Length; i++)
        {
            if (i == randomIndex)
            {
                pesanHasil[i].gameObject.SetActive(true);
            }
            else
            {
                pesanHasil[i].gameObject.SetActive(false);
            }
        }
    }

    public void Benar()
    {
        scoreCounter += 1;
        JnA.RemoveAt(pertanyaanSekarang);
        jawabanBenarCounter += 1;
        CheckJawabanBenar();


    }

    public void SelesaiQuiz()
    {
        gameOverQuizPanel.SetActive(false);
    }


    public void CheckJawabanBenar()
    {

        if (jawabanBenarCounter >= 3)
        {
            scorePlayer += 500;
            scoreHasil.text = scorePlayer.ToString();
            playerManager.IncreaseKeys(1);
            KeyDestroy();
            GameOverQuiz();

        }
        else
        {
            generatePertanyaan();
        }
    }

    public void Salah()
    {
        JnA.RemoveAt(pertanyaanSekarang);
        CheckJawabanBenar();
    }

    private void SetJawaban()
    {
        for (int i = 0; i < opsi.Length; i++)
        {
            opsi[i].GetComponent<Jawaban>().isBenar = false;
            opsi[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = JnA[pertanyaanSekarang].Jawaban[i];
            if (JnA[pertanyaanSekarang].jawabanBenar == i + 1)
            {
                opsi[i].GetComponent<Jawaban>().isBenar = true;
            }
        }
    }

    private void generatePertanyaan()
    {
        if (JnA.Count > 0)
        {
            pertanyaanSekarang = Random.Range(0, JnA.Count);
            pertanyaanText.text = JnA[pertanyaanSekarang].Pertanyaan;
            SetJawaban();
        }
        else
        {
            Debug.Log("Habis");
            GameOverQuiz();
        }

    }

    public void PopUpQuiz()
    {
        quizPanel.SetActive(true);
        scoreCounter = 0;
        pertanyaanSekarang = 0;
        jawabanBenarCounter = 0;
        totalPertanyaan = originalJnA.Count;
        JnA.Clear();
        JnA.AddRange(originalJnA);

    }

    private void KeyDestroy()
    {
        if (currentIndex < keyObject.Length)
        {
            Destroy(keyObject[currentIndex]);
            currentIndex++;
        }
    }
}
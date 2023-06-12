using System;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    
    [Header("Value")]
    [SerializeField] private int scoreCounter;
    //[SerializeField] public int scorePlayer;
    [SerializeField] private int pertanyaanSekarang;
    [SerializeField] private int jawabanBenarCounter = 0;
    private int currentIndex = 0;
    private int totalPertanyaan = 0;


    [Header("Waktu")]
    [SerializeField] private float waktuMaksimal = 30.0f;
    [SerializeField] private float displayDuration = 2f;
    [SerializeField] private float penaltyTime = 5.0f;
    private float waktuMulai;
    private float waktuSekarang;

    [Header("List Pertanyaan dan Jawaban")]
    [SerializeField] private List<TanyaDanJawab> originalJnA;
    [SerializeField] private List<TanyaDanJawab> JnA;

    [Header("GameObject")]
    [SerializeField] private GameObject quizPanel;
    [SerializeField] private GameObject gameOverQuizPanel;
    [SerializeField] private GameObject[] opsi;
    [SerializeField] private List<GameObject> keyObjects;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI[] pesanHasil;
    [SerializeField] private TextMeshProUGUI cekJawaban;
    [SerializeField] public TextMeshProUGUI scoreHasil;
    [SerializeField] private TextMeshProUGUI hasilJawaban;
    [SerializeField] private TextMeshProUGUI pertanyaanText;
    [SerializeField] private TextMeshProUGUI waktuText;


    [Header("Warna Teks")]
    [SerializeField] private Color benarColor = Color.green;
    [SerializeField] private Color salahColor = Color.red;


    [Header("Boolean")]
    private bool isGameOver = false;
    public bool isQuizStarted = false;
    public bool isQuizTampil = false;
    private bool isJawabanBenarTertekan = false;
    private bool isJawabanSalahTertekan = false;

    [Header("References")]
    public ScoreQuiz scoreQuiz;
    public PlayerManager playerManager;
    public static QuestionManager Instance;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        totalPertanyaan = JnA.Count;
        currentIndex = 0;
        originalJnA = new List<TanyaDanJawab>(JnA);
        gameOverQuizPanel.SetActive(false);
        generatePertanyaan();
    }

    private void Update()
    {
        CountDownTimer();
    }


    private void CountDownTimer()
    {
        if (!isGameOver && isQuizStarted)
        {
            waktuSekarang = DapatSisaWaktu();
            waktuText.text = Mathf.FloorToInt(waktuSekarang).ToString();

            if (waktuSekarang <= 0)
            {
                WaktuHabis();
            }
            else if (waktuSekarang <= 11)
            {
                waktuText.color = Color.red;
            }
            else
            {
                waktuText.color = Color.white;
            }
        }
    }


    private void WaktuHabis()
    {
        isGameOver = true;
        waktuText.text = "0";
        GameOverQuiz();
    }

    public void GameOverQuiz()
    {
        AudioManager.Instance.Play("Complete");
        AudioManager.Instance.Stop("CountDown");
        isQuizStarted = false;
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


        float sisaWaktuBerakhir = DapatSisaWaktu();
        float waktuPengalian = sisaWaktuBerakhir / waktuMaksimal;

        int minScore = 300;
        int maxScore = 700;

        int scoreRange = maxScore - minScore;
        int scoredPoints = Mathf.RoundToInt(scoreRange * waktuPengalian * jawabanBenarCounter);

        scoreQuiz.scoreQuizPlayer += scoredPoints;
        scoreHasil.text = scoreQuiz.scoreQuizPlayer.ToString();
    }

    public void Benar()
    {
        if (!isJawabanBenarTertekan)
        {
            AudioManager.Instance.Play("Correct");
            isJawabanBenarTertekan = true;
            scoreCounter += 1;
            JnA.RemoveAt(pertanyaanSekarang);
            jawabanBenarCounter += 1;
            CheckJawabanBenar();
            StartCoroutine(ShowJawabanText("Benar", benarColor));
        }
        else
        {
            StartCoroutine(ShowJawabanText("Sudah Terjawab", Color.red));
        }
    }

    public void Salah()
    {
        if (!isJawabanSalahTertekan)
        {
            AudioManager.Instance.Play("Wrong");
            isJawabanSalahTertekan = true;
            JnA.RemoveAt(pertanyaanSekarang);
            CheckJawabanBenar();
            StartCoroutine(ShowJawabanText("Salah", salahColor));
            waktuMulai -= penaltyTime;
            if (waktuSekarang < 0)
            {
                waktuSekarang = 0;
            }
        }
        else
        {
            StartCoroutine(ShowJawabanText("Sudah Terjawab", Color.red));
        }


    }

    public void SelesaiQuiz()
    {

        isQuizTampil=false;
        gameOverQuizPanel.SetActive(false);
    }
    public void CheckJawabanBenar()
    {
        if (jawabanBenarCounter >= 3)
        {
            playerManager.IncreaseKeys(1);
            if (currentIndex < keyObjects.Count)
            {
                GameObject keyObject = keyObjects[currentIndex];
                KeyDestroy(keyObject);
                currentIndex++;
            }

            // Pengecekan indeks terakhir
            if (currentIndex >= keyObjects.Count)
            {
                currentIndex = keyObjects.Count - 1;
            }

            GameOverQuiz();
        }
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
        isJawabanBenarTertekan = false;
        isJawabanSalahTertekan = false;
        AudioManager.Instance.Play("Generate");
        if (JnA.Count > 0)
        {
            pertanyaanSekarang = Random.Range(0, JnA.Count);
            pertanyaanText.text = JnA[pertanyaanSekarang].Pertanyaan;
            SetJawaban();
        }
        else
        {
            GameOverQuiz();
        }
    }

    public void PopUpQuiz()
    {
        AudioManager.Instance.Play("CountDown");
        AudioManager.Instance.Play("PopUp");
        waktuMulai = Time.time;
        waktuSekarang = waktuMaksimal;
        isGameOver = false;
        quizPanel.SetActive(true);
        scoreCounter = 0;
        pertanyaanSekarang = 0;
        jawabanBenarCounter = 0;
        totalPertanyaan = originalJnA.Count;
        JnA.Clear();
        JnA.AddRange(originalJnA);
        isQuizStarted = true;
    }

    private void KeyDestroy(GameObject keyObject)
    {
        AudioManager.Instance.Play("Key");
        keyObjects.Remove(keyObject);
        Destroy(keyObject);
    }

    private float DapatSisaWaktu()
    {
        float waktuBerlalu = Time.time - waktuMulai;
        float sisaWaktu = waktuMaksimal - waktuBerlalu;

        if (sisaWaktu < 0)
        {
            sisaWaktu = 0;
        }

        return sisaWaktu;

    }

    private IEnumerator ShowJawabanText(string text, Color color)
    {
        cekJawaban.text = text;
        cekJawaban.color = color;
        cekJawaban.gameObject.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        cekJawaban.gameObject.SetActive(false);
        generatePertanyaan();
    }
}

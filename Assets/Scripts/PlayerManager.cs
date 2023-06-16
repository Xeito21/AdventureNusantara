using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Status")]
    [SerializeField] private int jumlahNyawa = 3;
    [SerializeField] public int jumlahCoin = 0;
    [SerializeField] public int jumlahKey = 0;

    [Header("KnockBack")]
    [SerializeField] private float knockbackCounter;
    [SerializeField] private float knockbackTotalTime;
    [SerializeField] private float knockBackForce;


    [Header("Player")]
    [SerializeField] private SpriteRenderer karakterSprite;
    [SerializeField] private Rigidbody2D rb;
    Vector2 checkPointPosisi;

    [Header("GameObject")]
    [SerializeField] private GameObject[] nyawaObject;
    [SerializeField] private GameObject GameOverUI;

    [Header("Animator")]
    [SerializeField] public Animator animPlayer;

    [Header("TextObject")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI keyText;
    [SerializeField] private TextMeshProUGUI[] labelKalah;
    [SerializeField] private TextMeshProUGUI heartLabel;


    [Header("References")]
    public QuestionManager questionManager;
    public AudioManager audioManager;
    public PlayerMovement playerMovement;
    public CameraManager cameraManager;
    public static PlayerManager Instance;

    [Header("Boolean")]
    private bool knockFromRight;

    void Awake()
    {
        Instance = this;
        questionManager = FindObjectOfType<QuestionManager>();
        playerMovement = GetComponent<PlayerMovement>();
        cameraManager = FindObjectOfType<CameraManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        jumlahCoin = PlayerPrefs.GetInt("JumlahCoin", 0);
        UpdatePlayer();
        checkPointPosisi = transform.position;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Heart":
                if(jumlahNyawa < 3)
                {
                    IncreaseHearts(1);
                    AudioManager.Instance.Play("Heart");
                    Destroy(other.gameObject);
                }
                else
                {
                    AudioManager.Instance.Play("Warn");
                    StartCoroutine(HeartText("Heart anda sudah Penuh!", 2f));
                }
                break;
            case "Coin":
                IncreaseCoins(1);
                Destroy(other.gameObject);
                break;
            case "Key":
                questionManager.PopUpQuiz();
                break;
            case "Obstacle":
                TerkenaDamage();
                ApplyKnockBackObs();
                if (other.transform.position.y <= transform.position.y)
                {
                    knockFromRight = true;
                }
                if (other.transform.position.y > transform.position.y)
                {
                    knockFromRight = false;
                }
                break;
            default:
                break;
        }
    }

    public void TerkenaDamage()
    {
        AudioManager.Instance.Play("Hurt");
        jumlahNyawa -= 1;
        HealthUpdate();
        StartCoroutine(BlinkSprite(0.5f));

        if (jumlahNyawa <= 0)
        {
            GameOver();
        }
    }

    public void DamageJatuh()
    {
        Debug.Log("Jatuh");
        AudioManager.Instance.Play("Fall");
        jumlahNyawa -= 1;
        HealthUpdate();
        StartCoroutine(BlinkSprite(0.5f));

        if (jumlahNyawa <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        AudioManager.Instance.Play("GameOver");
        PlayerPrefs.SetInt("JumlahCoin", jumlahCoin);
        karakterSprite.GetComponent<SpriteRenderer>().enabled = false;
        playerMovement.rb.simulated = false;
        audioManager.Stop("BGM");
        GameOverUI.SetActive(true);
        int randomIndex = Random.Range(0, labelKalah.Length);
        for (int i = 0; i < labelKalah.Length; i++)
        {
            if (i == randomIndex)
                labelKalah[i].gameObject.SetActive(true);
            else
                labelKalah[i].gameObject.SetActive(false);
        }
    }

    void HealthUpdate()
    {
        for (int i = 0; i < nyawaObject.Length; i++)
        {
            if (i < jumlahNyawa)
                nyawaObject[i].SetActive(true);
            else
                nyawaObject[i].SetActive(false);
        }
    }

    public void IncreaseHearts(int amount)
    {
            jumlahNyawa += amount;
            HealthUpdate();

    }

    public void IncreaseCoins(int counter)
    {
        jumlahCoin += counter;
        AudioManager.Instance.Play("Coin");
        coinText.text = jumlahCoin.ToString();
    }

    public void IncreaseKeys(int counter)
    {
        jumlahKey += counter;
        keyText.text = jumlahKey.ToString();
    }

    IEnumerator BlinkSprite(float duration)
    {
        float elapsedTime = 0f;
        Color originalColor = karakterSprite.color;
        Color blinkColor = Color.red;

        while (elapsedTime < duration)
        {
            karakterSprite.color = blinkColor;
            yield return new WaitForSeconds(0.1f);
            karakterSprite.color = originalColor;
            yield return new WaitForSeconds(0.1f);
            elapsedTime += 0.2f;
        }
       karakterSprite.color = Color.white;
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkPointPosisi = pos;
    }


    private void UpdatePlayer()
    {
        coinText.text = jumlahCoin.ToString();
    }
    public IEnumerator HidupKembali(float duration)
    {
        if (jumlahNyawa > 0)
        {
            yield return new WaitForSeconds(duration);
            playerMovement.rb.velocity = Vector2.zero;
            transform.localScale = Vector3.zero;
            playerMovement.rb.simulated = false;
            transform.position = checkPointPosisi;
            transform.localScale = Vector3.one;
            HealthUpdate();
            cameraManager.EnableVirtualCamera();
            playerMovement.rb.simulated = true;
        }
        else
        {
            cameraManager.DisableVirtualCamera();
            playerMovement.rb.simulated = false;
        }

    }

    private void ApplyKnockBackObs()
    {
        if (knockFromRight == true)
        {
            rb.velocity = new Vector2(-knockBackForce, knockBackForce);
        }
        if (knockFromRight == false)
        {
            rb.velocity = new Vector2(knockBackForce, knockBackForce);
        }

        knockbackCounter -= Time.deltaTime;
    }


    private IEnumerator HeartText(string text, float duration)
    {
        heartLabel.text = text;
        heartLabel.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        heartLabel.gameObject.SetActive(false);
    }
}

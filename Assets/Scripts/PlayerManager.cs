using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Status")]
    [SerializeField] private int jumlahNyawa = 3;
    [SerializeField] private int jumlahCoin = 0;
    [SerializeField] private int jumlahKey = 0;

    [Header("Player")]
    [SerializeField] private SpriteRenderer karakterSprite;
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

    [Header("References")]
    public QuestionManager questionManager;
    public PlayerMovement playerMovement;
    public CameraManager cameraManager;
    public static PlayerManager Instance;



    void Awake() 
    {
        Instance = this;
        playerMovement = GetComponent<PlayerMovement>();
        cameraManager = FindObjectOfType<CameraManager>();

    }

    private void Start()
    {
        checkPointPosisi = transform.position;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Heart":
                IncreaseHearts(1);
                Destroy(other.gameObject);
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
                break;
            default:
                break;
        }
    }
    public void TerkenaDamage()
    {
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
        Time.timeScale = 0f;
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
        Debug.Log(jumlahCoin.ToString());
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

        karakterSprite.color = originalColor;
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkPointPosisi = pos;
    }


    public IEnumerator HidupKembali(float duration)
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

}

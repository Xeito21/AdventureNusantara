using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private int currentCoins = 0;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        coinText.text = currentCoins.ToString();
    }

    public void IncreaseCoins(int counter)
    {
        currentCoins += counter;
        coinText.text = currentCoins.ToString();
    }
}

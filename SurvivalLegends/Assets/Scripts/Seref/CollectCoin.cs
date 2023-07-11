using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectCoin : MonoBehaviour
{
    public int coinAmount;
    public TMP_Text coinUI;

    private void Awake()
    {
        coinAmount = PlayerPrefs.GetInt("CoinAmount", 0);
        coinUI.text = "Coins: " + coinAmount.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            int addCoin = coinAmount + 1;
            PlayerPrefs.SetInt("CoinAmount", addCoin);
            Debug.Log(coinAmount + "coins.");
            coinUI.text = "Coins: " + coinAmount.ToString();
            Destroy(other.gameObject);

            //other.gameObject.SetActive(false);
        }
    }
} 
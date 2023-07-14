using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectCoin : MonoBehaviour
{
    public int coinAmount;
    public TMP_Text coinUI;
    public Buton1 buton1;
    public Buton2 buton2;

    private const string CoinAmountKey = "CoinAmount";

    private void OnEnable()
    {
        ADController.OnGaveReward += GiveCoin;
    }

    private void OnDisable()
    {
        ADController.OnGaveReward -= GiveCoin;
    }

    private void GiveCoin()
    {
        coinAmount += 50;
    }

    private void Awake()
    {
        ResetCoinAmount();
        coinUI.text = coinAmount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinAmount++;
            PlayerPrefs.SetInt(CoinAmountKey, coinAmount);
            Debug.Log(coinAmount + " coins.");
            coinUI.text = coinAmount.ToString();
            Destroy(other.gameObject);

            // Butonlarýn durumunu kontrol et ve pasif hale getir
            buton1.UpdateButtonInteractivity();
            //buton2.UpdateButtonInteractivity();
        }
    }

    private void ResetCoinAmount()
    {
        coinAmount = 0; // Oyun baþýnda coin miktarýný sýfýrla
        PlayerPrefs.SetInt(CoinAmountKey, coinAmount);
    }

    private void Start()
    {
        ResetCoinAmount();
        coinUI.text = coinAmount.ToString();
    }
}

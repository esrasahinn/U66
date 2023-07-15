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
    //public Buton1Ninja buton1N;
    public Buton2 buton2;
    public Buton3 buton3;
    //public Buton3Rifle buton3R;   
    public Buton4 buton4;
    public Buton5 buton5;
    public Buton6 buton6;

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
        UpdateButtonInteractivity();
    }

    private void Awake()
    {
        ResetCoinAmount();
        coinUI.text = coinAmount.ToString();
        UpdateButtonInteractivity();
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
            //buton1N.UpdateButtonInteractivity();
            buton2.UpdateButtonInteractivity();
            buton3.UpdateButtonInteractivity();
            //buton3R.UpdateButtonInteractivity();
            buton4.UpdateButtonInteractivity();
            buton5.UpdateButtonInteractivity();
            buton6.UpdateButtonInteractivity();
        }
    }

    private void ResetCoinAmount()
    {
        coinAmount = 0;
        //coinAmount = PlayerPrefs.GetInt(CoinAmountKey, 0);
       // PlayerPrefs.SetInt(CoinAmountKey, coinAmount);
    }

    private void Start()
    {
        ResetCoinAmount();
        coinUI.text = coinAmount.ToString();
        buton1.UpdateButtonInteractivity();
       // buton1N.UpdateButtonInteractivity();
        buton2.UpdateButtonInteractivity();
        buton3.UpdateButtonInteractivity();
        //buton3R.UpdateButtonInteractivity();
        buton4.UpdateButtonInteractivity();
        buton5.UpdateButtonInteractivity();
        buton6.UpdateButtonInteractivity();
    }

    public void UpdateButtonInteractivity()
    {
        buton1.UpdateButtonInteractivity();
        //buton1N.UpdateButtonInteractivity();
        buton2.UpdateButtonInteractivity();
        buton3.UpdateButtonInteractivity();
        //buton3R.UpdateButtonInteractivity();
        buton4.UpdateButtonInteractivity();
        buton5.UpdateButtonInteractivity();
        buton6.UpdateButtonInteractivity();
    }
}

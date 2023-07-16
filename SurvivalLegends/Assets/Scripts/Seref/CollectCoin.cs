using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectCoin : MonoBehaviour
{
    public int coinAmount;
    public TMP_Text coinUI;
    public List<Button> allButtons;
    public Button buton1;
    public Button buton2;
    public Button buton3;
    public Button buton4;
    public Button buton5;
    public Button buton6;

    private const string CoinAmountKey = "CoinAmount";
    private expController expController;

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
        coinAmount++;
        PlayerPrefs.SetInt(CoinAmountKey, coinAmount);
        Debug.Log(coinAmount + " coins.");
        coinUI.text = coinAmount.ToString();
        UpdateButtonInteractivity(); // Butonlarýn etkinlik durumunu güncelle
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
            UpdateButtonInteractivity();
        }
    }

    private void ResetCoinAmount()
    {
        coinAmount = 0;
        //coinAmount = PlayerPrefs.GetInt(CoinAmountKey, 0);
    }

    private void Start()
    {
        ResetCoinAmount();
        coinUI.text = coinAmount.ToString();

        // expController örneðini al
        expController = FindObjectOfType<expController>();

        // Buton örneklerini atama
        foreach (Button button in allButtons)
        {
            if (button.name == "Buton1")
                buton1 = button;
            else if (button.name == "Buton2")
                buton2 = button;
            else if (button.name == "Buton3")
                buton3 = button;
            else if (button.name == "Buton4")
                buton4 = button;
            else if (button.name == "Buton5")
                buton5 = button;
            else if (button.name == "Buton6")
                buton6 = button;
        }

        // Butonlarýn etkinlik durumunu güncelle
        UpdateButtonInteractivity();
        expController.SetRandomButtons(); // Rastgele butonlarý güncelle
    }

    public void UpdateButtonInteractivity()
    {
        int collectedCoinCount = coinAmount;

        // Butonlarýn etkinlik durumunu güncelle
        buton1.interactable = collectedCoinCount >= 5;
        buton2.interactable = collectedCoinCount >= 5;
        buton3.interactable = collectedCoinCount >= 5;
        buton4.interactable = collectedCoinCount >= 5;
        buton5.interactable = collectedCoinCount >= 5;
        buton6.interactable = collectedCoinCount >= 5;
    }
}

//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class SpendButton : MonoBehaviour
//{
//    public int coinCost;
//    public CollectCoin collectCoinScript;
//    public TMP_Text coinInsufficientUI; // Yetersiz coin mesajýnýn UI bileþeni

//    private Button button;

//    private void Start()
//    {
//        button = GetComponent<Button>();
//        button.onClick.AddListener(Spend);
//        UpdateInteractable();
//    }

//    private void UpdateInteractable()
//    {
//        button.interactable = collectCoinScript.coinAmount >= coinCost;

//        if (coinInsufficientUI != null)
//        {
//            coinInsufficientUI.gameObject.SetActive(!button.interactable);
//        }
//    }

//    private void Spend()
//    {
//        if (collectCoinScript.coinAmount >= coinCost)
//        {
//            collectCoinScript.SpendCoin(coinCost);
//            UpdateInteractable();
//        }
//    }
//}

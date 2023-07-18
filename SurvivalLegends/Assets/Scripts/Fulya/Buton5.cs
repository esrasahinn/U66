using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buton5 : MonoBehaviour
{
    public GameObject meteorPrefab;
    public int dusmanHasarMiktari = 10;
    private expController controller;
    private GameObject[] dusmanlar;
    public Image buton5;
    public Text countdownText;

    [SerializeField]
    private int coinCost = 5; // Alým için gereken coin miktarý

    private Button button;

    private void Awake()
    {
        controller = FindObjectOfType<expController>();
        button = GetComponent<Button>();
        UpdateButtonInteractivity();
    }

    public void ButonTiklama()
    {
        CollectCoin collectCoinScript = FindObjectOfType<CollectCoin>();
        if (collectCoinScript != null && collectCoinScript.coinAmount >= coinCost)
        {
            int playerCoins = collectCoinScript.coinAmount;

            playerCoins -= coinCost; // Coinlerden düþülüyor
            PlayerPrefs.SetInt("CoinAmount", playerCoins);

            // Düþmanlarý bul
            dusmanlar = GameObject.FindGameObjectsWithTag("Dusman");

            if (dusmanlar.Length > 0)
            {
                // Rastgele 3 düþman seç
                List<GameObject> rastgeleDusmanlar = new List<GameObject>();
                int maxDusmanSayisi = Mathf.Min(dusmanlar.Length, 3);

                while (rastgeleDusmanlar.Count < maxDusmanSayisi)
                {
                    int rastgeleIndex = Random.Range(0, dusmanlar.Length);
                    GameObject rastgeleDusman = dusmanlar[rastgeleIndex];

                    if (!rastgeleDusmanlar.Contains(rastgeleDusman))
                    {
                        rastgeleDusmanlar.Add(rastgeleDusman);

                        // Meteoru düþmana at
                        Vector3 spawnPosition = rastgeleDusman.transform.position + Vector3.up * 6f;
                        GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

                        // Düþmana zarar verme iþlemini yap
                        if (rastgeleDusman.GetComponent<RangedEnemyController>() != null)
                        {
                            RangedEnemyController dusmanAI = rastgeleDusman.GetComponent<RangedEnemyController>();
                            Meteor suScript = meteor.GetComponent<Meteor>();
                            suScript.Atesle(dusmanHasarMiktari, dusmanAI);
                        }
                        else if (rastgeleDusman.GetComponent<EnemyController>() != null)
                        {
                            EnemyController dusmanController = rastgeleDusman.GetComponent<EnemyController>();
                            Meteor suScript = meteor.GetComponent<Meteor>();
                            suScript.Atesle(dusmanHasarMiktari, dusmanController);
                        }
                    }
                }

                controller.HidePopup();
                controller.ResumeGame();

                // Coin sayýsýný güncelle
                collectCoinScript.coinAmount = playerCoins;
                collectCoinScript.coinUI.text = playerCoins.ToString();

                countdownText.text = "8";
                countdownText.gameObject.SetActive(true);
                buton5.gameObject.SetActive(true);

                InvokeRepeating(nameof(UpdateCountdown), 1f, 1f);
            }
        }
        else
        {
            Debug.Log("Yeterli coininiz yok.");
        }

        UpdateButtonInteractivity();
    }

    private void UpdateCountdown()
    {
        int remainingTime = int.Parse(countdownText.text);
        countdownText.text = (remainingTime - 1).ToString();

        if (remainingTime <= 1)
        {
            CancelInvoke(nameof(UpdateCountdown));
            countdownText.text = "";
            countdownText.gameObject.SetActive(false);
            buton5.gameObject.SetActive(false);
            Debug.Log("Geri sayým tamamlandý.");
        }
    }

    public void UpdateButtonInteractivity()
    {
        CollectCoin collectCoinScript = FindObjectOfType<CollectCoin>();
        if (collectCoinScript != null && collectCoinScript.coinAmount >= coinCost)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
    public void SetInteractable(bool interactable)
    {
        button.interactable = interactable;
    }
}

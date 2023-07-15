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
    private int coinCost = 5; // Al�m i�in gereken coin miktar�

    private void Awake()
    {
        controller = FindObjectOfType<expController>();
    }

    public void ButonTiklama()
    {
        int playerCoins = PlayerPrefs.GetInt("CoinAmount", 0); // Oyuncunun sahip oldu�u coin miktar�

        if (playerCoins >= coinCost)
        {
            playerCoins -= coinCost; // Coinlerden d���l�yor
            PlayerPrefs.SetInt("CoinAmount", playerCoins);

            // D��manlar� bul
            dusmanlar = GameObject.FindGameObjectsWithTag("Dusman");

            if (dusmanlar.Length > 0)
            {
                // Rastgele 3 d��man se�
                List<GameObject> rastgeleDusmanlar = new List<GameObject>();
                int maxDusmanSayisi = Mathf.Min(dusmanlar.Length, 3);

                while (rastgeleDusmanlar.Count < maxDusmanSayisi)
                {
                    int rastgeleIndex = Random.Range(0, dusmanlar.Length);
                    GameObject rastgeleDusman = dusmanlar[rastgeleIndex];

                    if (!rastgeleDusmanlar.Contains(rastgeleDusman))
                    {
                        rastgeleDusmanlar.Add(rastgeleDusman);

                        // Meteoru d��mana at
                        Vector3 spawnPosition = rastgeleDusman.transform.position + Vector3.up * 6f;
                        GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

                        // D��mana zarar verme i�lemini yap
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

                // Coin say�s�n� g�ncelle
                CollectCoin collectCoinScript = FindObjectOfType<CollectCoin>();
                if (collectCoinScript != null)
                {
                    collectCoinScript.coinAmount = playerCoins;
                    collectCoinScript.coinUI.text = playerCoins.ToString();
                }

                countdownText.text = "5";
                countdownText.gameObject.SetActive(true);
                buton5.gameObject.SetActive(true);

                InvokeRepeating(nameof(UpdateCountdown), 1f, 1f);
            }
        }
        else
        {
            Debug.Log("Yeterli coininiz yok.");
        }
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
            Debug.Log("Geri say�m tamamland�.");
        }
    }
}

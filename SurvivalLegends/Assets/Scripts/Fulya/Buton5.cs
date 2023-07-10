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
    public Text countdownText; // UI metin ��esi

    private void Awake()
    {
        controller = FindObjectOfType<expController>();
    }

    public void ButonTiklama()
    {
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
            controller.ResumeGame(); // Oyunu devam ettir

            countdownText.text = "5"; // Metin ��esini g�ncelle
            countdownText.gameObject.SetActive(true); // Metin ��esini etkinle�tir
            buton5.gameObject.SetActive(true); // Resmi etkinle�tir

            InvokeRepeating(nameof(UpdateCountdown), 1f, 1f); // Saniyede bir geri say�m� g�ncelle
        }
    }

    private void UpdateCountdown()
    {
        int remainingTime = int.Parse(countdownText.text); // Geri say�m s�resini al

        remainingTime--; // Geri say�m s�resini azalt
        countdownText.text = remainingTime.ToString(); // Metin ��esini g�ncelle

        if (remainingTime <= 0)
        {
            CancelInvoke(nameof(UpdateCountdown));
            countdownText.text = ""; // Metin ��esini temizle
            countdownText.gameObject.SetActive(false); // Metin ��esini devre d��� b�rak
            buton5.gameObject.SetActive(false); // Resmi devre d��� b�rak
            Debug.Log("Geri say�m tamamland�.");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buton4 : MonoBehaviour
{
    public GameObject zehirliSuPrefab;
    public int dusmanHasarMiktari = 10;
    private expController controller;
    private GameObject[] dusmanlar;
    public Image buton4;
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

                    // Zehirli suyu d��mana at
                    Vector3 spawnPosition = rastgeleDusman.transform.position + Vector3.up * 6f;
                    GameObject zehirliSu = Instantiate(zehirliSuPrefab, spawnPosition, Quaternion.identity);

                    // D��mana zarar verme i�lemini yap
                    if (rastgeleDusman.GetComponent<RangedEnemyController>() != null)
                    {
                        RangedEnemyController dusmanAI = rastgeleDusman.GetComponent<RangedEnemyController>();
                        ZehirliSu suScript = zehirliSu.GetComponent<ZehirliSu>();
                        suScript.Atesle(dusmanHasarMiktari, dusmanAI);
                    }
                    else if (rastgeleDusman.GetComponent<EnemyController>() != null)
                    {
                        EnemyController dusmanController = rastgeleDusman.GetComponent<EnemyController>();
                        ZehirliSu suScript = zehirliSu.GetComponent<ZehirliSu>();
                        suScript.Atesle(dusmanHasarMiktari, dusmanController);
                    }
                }
            }

            controller.HidePopup();
            controller.ResumeGame(); // Oyunu devam ettir

            countdownText.text = "5"; // Metin ��esini g�ncelle
            countdownText.gameObject.SetActive(true); // Metin ��esini etkinle�tir
            buton4.gameObject.SetActive(true); // Resmi etkinle�tir

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
            buton4.gameObject.SetActive(false); // Resmi devre d��� b�rak
            Debug.Log("Geri say�m tamamland�.");
        }
    }
}



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
    public Text countdownText; // UI metin öðesi

    private void Awake()
    {
        controller = FindObjectOfType<expController>();
    }

    public void ButonTiklama()
    {
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

                    // Zehirli suyu düþmana at
                    Vector3 spawnPosition = rastgeleDusman.transform.position + Vector3.up * 6f;
                    GameObject zehirliSu = Instantiate(zehirliSuPrefab, spawnPosition, Quaternion.identity);

                    // Düþmana zarar verme iþlemini yap
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

            countdownText.text = "5"; // Metin öðesini güncelle
            countdownText.gameObject.SetActive(true); // Metin öðesini etkinleþtir
            buton4.gameObject.SetActive(true); // Resmi etkinleþtir

            InvokeRepeating(nameof(UpdateCountdown), 1f, 1f); // Saniyede bir geri sayýmý güncelle
        }
    }

    private void UpdateCountdown()
    {
        int remainingTime = int.Parse(countdownText.text); // Geri sayým süresini al

        remainingTime--; // Geri sayým süresini azalt
        countdownText.text = remainingTime.ToString(); // Metin öðesini güncelle

        if (remainingTime <= 0)
        {
            CancelInvoke(nameof(UpdateCountdown));
            countdownText.text = ""; // Metin öðesini temizle
            countdownText.gameObject.SetActive(false); // Metin öðesini devre dýþý býrak
            buton4.gameObject.SetActive(false); // Resmi devre dýþý býrak
            Debug.Log("Geri sayým tamamlandý.");
        }
    }
}



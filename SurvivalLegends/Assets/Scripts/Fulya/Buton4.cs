using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buton4 : MonoBehaviour
{
    public GameObject zehirliSuPrefab;
    public int dusmanHasarMiktari = 10;
    private expController controller;
    private GameObject[] dusmanlar;

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
        }
    }
}



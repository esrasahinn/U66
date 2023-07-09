using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buton5 : MonoBehaviour
{
    public GameObject meteorPrefab;
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

                    // Meteoru düþmana at
                    Vector3 spawnPosition = rastgeleDusman.transform.position + Vector3.up * 6f;
                    GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

                    // Düþmana zarar verme iþlemini yap
                    if (rastgeleDusman.GetComponent<EnemyAI>() != null)
                    {
                        EnemyAI dusmanAI = rastgeleDusman.GetComponent<EnemyAI>();
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
        }
    }
}
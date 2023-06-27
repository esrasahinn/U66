using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buton4 : MonoBehaviour
{
    public GameObject zehirliSuPrefab;
    public Transform dusmanAlan;
    public int dusmanHasarMiktari = 10;
    private expController controller;

    private void Awake()
    {
        controller = FindObjectOfType<expController>();
    }

    public void ButonTiklama()
    {
        // Zehirli suyu rastgele bir konuma yerleþtir
        Vector3 randomPosition = RandomPositionInArea(dusmanAlan);
        randomPosition.y += 5f; // Yükseklik ekleyerek prefab'ý yukarý taþý
        GameObject zehirliSu = Instantiate(zehirliSuPrefab, randomPosition, Quaternion.identity);

        // Rastgele düþmana zarar ver
        DusmanlaraZararVer(zehirliSu.GetComponent<ZehirliSu>());
    }

    private Vector3 RandomPositionInArea(Transform area)
    {
        // Alanýn sýnýrlarýný kullanarak rastgele bir konum üret
        Vector3 min = area.position - area.localScale / 2f;
        Vector3 max = area.position + area.localScale / 2f;

        return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
    }

    private void DusmanlaraZararVer(ZehirliSu zehirliSu)
    {
        // Tüm düþmanlarý seç
        EnemyAI[] dusmanlar = FindObjectsOfType<EnemyAI>();

        // Rastgele düþmana zarar ver
        if (dusmanlar.Length > 0)
        {
            // Rastgele bir düþman seç
            int rastgeleDusmanIndex = Random.Range(0, dusmanlar.Length);
            EnemyAI rastgeleDusman = dusmanlar[rastgeleDusmanIndex];

            // Seçilen düþmana zarar ver
            rastgeleDusman.HasarAl(dusmanHasarMiktari);

            // Zehirli suyu düþmana at
            zehirliSu.At(rastgeleDusman);
        }

        controller.HidePopup();
    }
}


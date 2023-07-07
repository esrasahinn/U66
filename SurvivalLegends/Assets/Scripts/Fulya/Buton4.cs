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
        controller.HidePopup();
       // controller.ResumeGame(); // Oyunu devam ettir
        // Zehirli suyu düþmana at
        ZehirliSu suScript = zehirliSu.GetComponent<ZehirliSu>();
        suScript.Atesle(dusmanHasarMiktari);
    }

    private Vector3 RandomPositionInArea(Transform area)
    {
        // Alanýn sýnýrlarýný kullanarak rastgele bir konum üret
        Vector3 min = area.position - area.localScale / 2f;
        Vector3 max = area.position + area.localScale / 2f;

        return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
    }
}
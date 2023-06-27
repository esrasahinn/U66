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
        // Zehirli suyu rastgele bir konuma yerle�tir
        Vector3 randomPosition = RandomPositionInArea(dusmanAlan);
        randomPosition.y += 5f; // Y�kseklik ekleyerek prefab'� yukar� ta��
        GameObject zehirliSu = Instantiate(zehirliSuPrefab, randomPosition, Quaternion.identity);

        // Rastgele d��mana zarar ver
        DusmanlaraZararVer(zehirliSu.GetComponent<ZehirliSu>());
    }

    private Vector3 RandomPositionInArea(Transform area)
    {
        // Alan�n s�n�rlar�n� kullanarak rastgele bir konum �ret
        Vector3 min = area.position - area.localScale / 2f;
        Vector3 max = area.position + area.localScale / 2f;

        return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
    }

    private void DusmanlaraZararVer(ZehirliSu zehirliSu)
    {
        // T�m d��manlar� se�
        EnemyAI[] dusmanlar = FindObjectsOfType<EnemyAI>();

        // Rastgele d��mana zarar ver
        if (dusmanlar.Length > 0)
        {
            // Rastgele bir d��man se�
            int rastgeleDusmanIndex = Random.Range(0, dusmanlar.Length);
            EnemyAI rastgeleDusman = dusmanlar[rastgeleDusmanIndex];

            // Se�ilen d��mana zarar ver
            rastgeleDusman.HasarAl(dusmanHasarMiktari);

            // Zehirli suyu d��mana at
            zehirliSu.At(rastgeleDusman);
        }

        controller.HidePopup();
    }
}


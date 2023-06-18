using UnityEngine;
using UnityEngine.UI;

public class Dusman : MonoBehaviour
{
    [SerializeField] float can = 100f;
    [SerializeField] float maxCan = 100f;
    [SerializeField] float atisMenzili = 10f; // D��man�n ate� edebilece�i maksimum mesafe

    [SerializeField] Slider canBariSlider; // Can �ubu�u Slider bile�eni

    public Transform atisNoktasi; // Mermilerin ��kaca�� at�� noktas�
    public GameObject mermiPrefab; // Mermi prefabi
    public int mermiHasari = 10; // Mermi hasar�

    private GameObject hedef;
    private bool takipEdiyor = false;

    [SerializeField] float atisAraligi = 1f; // Mermi at�� aral���
    private float atisZamani = 0f; // Son at�� zaman�

    private void Start()
    {
        HedefSec();

        if (canBariSlider != null)
        {
            canBariSlider.maxValue = maxCan; // Can �ubu�unun maksimum de�erini ayarla
            canBariSlider.value = can; // Can �ubu�unun de�erini ayarla
        }
    }

    private void Update()
    {
        if (hedef != null && takipEdiyor)
        {
            // D��man� oyuncuya d�nd�r
            transform.LookAt(hedef.transform);

            // D��man� ileri do�ru hareket ettir
            transform.Translate(Vector3.forward * Time.deltaTime);

            // Takip i�lemi burada devam eder

            // E�er mesafe kontrol� gerekiyorsa, mesafe kontrol� yap�labilir
            float mesafe = Vector3.Distance(transform.position, hedef.transform.position);
            if (mesafe <= atisMenzili)
            {
                // Hasar alma i�lemini burada ger�ekle�tir

                // Mermi atma i�lemi
                if (Time.time >= atisZamani)
                {
                    AtisYap();
                    atisZamani = Time.time + atisAraligi;
                }
            }
        }
    }

    public void HasarAl(int hasar)
    {
        can -= hasar;

        if (canBariSlider != null)
        {
            canBariSlider.value = can; // Can �ubu�unun de�erini g�ncelle
        }

        if (can <= 0)
        {
            Olum();
        }
    }

    private void Olum()
    {
        Debug.Log("Dusman Oldu");
        // D��man�n �l�m�yle ilgili yap�lmas� gereken i�lemler buraya eklenebilir.
        Destroy(gameObject); // D��man nesnesini yok etmek i�in kullanabilirsiniz.
    }

    private void HedefSec()
    {
        hedef = GameObject.FindGameObjectWithTag("Player");
        if (hedef != null)
        {
            takipEdiyor = true;
        }
        else
        {
            takipEdiyor = false;
        }
    }

    private void AtisYap()
    {
        if (atisNoktasi != null && mermiPrefab != null)
        {
            // Mermiyi olu�tur
            GameObject mermi = Instantiate(mermiPrefab, atisNoktasi.position, atisNoktasi.rotation);

            // Olu�turulan mermiye MermiBilgisi bile�enini ekle
            DusmanMermi dusmanMermi = mermi.GetComponent<DusmanMermi>();
            if (dusmanMermi != null)
            {
                dusmanMermi.Ayarla(mermiHasari, hedef.transform); // Mermi hasar�n� ayarla ve hedefi iletilir
            }
        }
    }
}
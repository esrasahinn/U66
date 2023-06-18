using UnityEngine;
using UnityEngine.UI;

public class Dusman : MonoBehaviour
{
    [SerializeField] float can = 100f;
    [SerializeField] float maxCan = 100f;
    [SerializeField] float atisMenzili = 10f; // Düþmanýn ateþ edebileceði maksimum mesafe

    [SerializeField] Slider canBariSlider; // Can çubuðu Slider bileþeni

    public Transform atisNoktasi; // Mermilerin çýkacaðý atýþ noktasý
    public GameObject mermiPrefab; // Mermi prefabi
    public int mermiHasari = 10; // Mermi hasarý
    private PlayerBehaviour _playerBehaviour;

    private GameObject hedef;
    private bool takipEdiyor = false;

    [SerializeField] float atisAraligi = 1f; // Mermi atýþ aralýðý
    private float atisZamani = 0f; // Son atýþ zamaný

    private void Start()
    {
        HedefSec();

        if (canBariSlider != null)
        {
            canBariSlider.maxValue = maxCan;
            canBariSlider.value = can;
        }

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            _playerBehaviour = playerObject.GetComponent<PlayerBehaviour>();
        }
    }

    private void Update()
    {
        if (hedef != null && takipEdiyor)
        {
            // Düþmaný oyuncuya döndür
            transform.LookAt(hedef.transform);

            // Düþmaný ileri doðru hareket ettir
            transform.Translate(Vector3.forward * Time.deltaTime);

            // Takip iþlemi burada devam eder

            // Eðer mesafe kontrolü gerekiyorsa, mesafe kontrolü yapýlabilir
            float mesafe = Vector3.Distance(transform.position, hedef.transform.position);
            if (mesafe <= atisMenzili)
            {
                // Hasar alma iþlemini burada gerçekleþtir

                // Mermi atma iþlemi
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
            canBariSlider.value = can;
        }

        if (can <= 0)
        {
            Olum();
        }
        else if (_playerBehaviour != null)
        {
            _playerBehaviour.PlayerTakeDmg(hasar);
        }
    }

    private void Olum()
    {
        Debug.Log("Dusman Oldu");
        // Düþmanýn ölümüyle ilgili yapýlmasý gereken iþlemler buraya eklenebilir.
        Destroy(gameObject); // Düþman nesnesini yok etmek için kullanabilirsiniz.
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
            GameObject mermi = Instantiate(mermiPrefab, atisNoktasi.position, atisNoktasi.rotation);
            DusmanMermi dusmanMermi = mermi.GetComponent<DusmanMermi>();
            if (dusmanMermi != null)
            {
                dusmanMermi.Ayarla(mermiHasari, hedef.transform);

                GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
                if (playerObject != null)
                {
                    PlayerBehaviour playerBehaviour = playerObject.GetComponent<PlayerBehaviour>();
                    if (playerBehaviour != null)
                    {
                        playerBehaviour.PlayerTakeDmg(mermiHasari);
                    }
                }
            }
        }
    }
}
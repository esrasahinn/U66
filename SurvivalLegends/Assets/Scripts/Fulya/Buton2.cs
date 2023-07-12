using UnityEngine;
using UnityEngine.UI;

public class Buton2 : MonoBehaviour
{
    public float hasarAlmamaSure = 10f; // Hasar almama s�resi (saniye)
    private bool hasarAlmamaAktif = false; // Hasar almama durumu
    private float hasarAlmamaSureKalan = 0f; // Geriye kalan hasar almama s�resi
    private expController controller;
    private ArcherPlayerBehaviour arcPlayerBehaviour;
    private PlayerBehaviour playerBehaviour;
    public Image buton2;
    public Text countdownText;

    [SerializeField]
    private int coinCost = 5; // Al�m i�in gereken coin miktar�

    private void Awake()
    {
        controller = FindObjectOfType<expController>();
        arcPlayerBehaviour = FindObjectOfType<ArcherPlayerBehaviour>();
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
    }

    private void Update()
    {
        if (hasarAlmamaAktif)
        {
            if (hasarAlmamaSureKalan > 0f)
            {
                hasarAlmamaSureKalan -= Time.deltaTime;

                if (hasarAlmamaSureKalan <= 0f)
                {
                    hasarAlmamaAktif = false;
                    arcPlayerBehaviour.DeactivateImmunity();
                    Debug.Log("Hasar alma s�resi doldu.");
                }
            }
        }
    }

    public void ButonTiklama()
    {
        int playerCoins = PlayerPrefs.GetInt("CoinAmount", 0); // Oyuncunun sahip oldu�u coin miktar�

        if (playerCoins >= coinCost && !hasarAlmamaAktif)
        {
            playerCoins -= coinCost; // Coinlerden d���l�yor
            PlayerPrefs.SetInt("CoinAmount", playerCoins);

            hasarAlmamaAktif = true;
            controller.HidePopup();
            controller.ResumeGame();
            hasarAlmamaSureKalan = hasarAlmamaSure;
            Debug.Log("Hasar almama s�resi ba�lad�.");

            arcPlayerBehaviour.ActivateImmunity(hasarAlmamaSure);
            countdownText.text = Mathf.CeilToInt(hasarAlmamaSure).ToString();
            countdownText.gameObject.SetActive(true);
            buton2.gameObject.SetActive(true);

            // Coin say�s�n� g�ncelle
            CollectCoin collectCoinScript = FindObjectOfType<CollectCoin>();
            if (collectCoinScript != null)
            {
                collectCoinScript.coinAmount = playerCoins;
                collectCoinScript.coinUI.text = playerCoins.ToString();
            }

            InvokeRepeating(nameof(UpdateCountdown), 1f, 1f);
        }
        else
        {
            Debug.Log("Yeterli coininiz yok veya hasar almama zaten aktif.");
        }
    }

    private void UpdateCountdown()
    {
        int remainingTime = Mathf.CeilToInt(hasarAlmamaSureKalan);
        countdownText.text = remainingTime.ToString();
        hasarAlmamaSureKalan -= 1f;

        if (hasarAlmamaSureKalan <= 0)
        {
            CancelInvoke(nameof(UpdateCountdown));
            countdownText.text = "";
            countdownText.gameObject.SetActive(false);
            buton2.gameObject.SetActive(false);
            Debug.Log("Geri say�m tamamland�.");
        }
    }
}

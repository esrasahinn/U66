using UnityEngine;
using UnityEngine.UI;

public class Buton3 : MonoBehaviour
{
    private bool canDoldurmaAktif = false; // Can doldurma durumu
    private expController controller;
    private ArcherPlayerBehaviour arcPlayer;
    private PlayerBehaviour player;
    private Healthbar _healthbar; // _healthbar referans� eklendi
    public Image buton3;
    public GameObject healPrefab; // Heal prefab� eklendi
    private GameObject healInstance; // Heal prefab� �rne�i eklendi
    public Text countdownText;

    [SerializeField]
    private int coinCost = 5; // Al�m i�in gereken coin miktar�

    private Button button;

    private void Awake()
    {
        controller = FindObjectOfType<expController>();
        arcPlayer = ArcherPlayerBehaviour.GetInstance();
        player = PlayerBehaviour.GetInstance();

        _healthbar = FindObjectOfType<Healthbar>();
        button = GetComponent<Button>();
        UpdateButtonInteractivity();
    }

    public void ButonTiklama()
    {
        CollectCoin collectCoinScript = FindObjectOfType<CollectCoin>();
        if (collectCoinScript != null && collectCoinScript.coinAmount >= coinCost && !canDoldurmaAktif)
        {
            int playerCoins = collectCoinScript.coinAmount;

            playerCoins -= coinCost; // Coinlerden d���l�yor
            PlayerPrefs.SetInt("CoinAmount", playerCoins);

            canDoldurmaAktif = true;
            arcPlayer.PerformLeftShiftAction();
            controller.HidePopup();
            controller.ResumeGame();
            Debug.Log("Karakterin can� dolduruldu.");

            // Coin say�s�n� g�ncelle
            collectCoinScript.coinAmount = playerCoins;
            collectCoinScript.coinUI.text = playerCoins.ToString();

            countdownText.text = "5";
            countdownText.gameObject.SetActive(true);
            buton3.gameObject.SetActive(true);

            // Heal prefab�n� olu�tur ve player'�n alt nesnesi yap
            healInstance = Instantiate(healPrefab, arcPlayer.transform.position, Quaternion.identity);
            healInstance.transform.parent = arcPlayer.transform;

            // Heal prefab�n� aktifle�tir
            healInstance.SetActive(true);

            InvokeRepeating(nameof(UpdateCountdown), 1f, 1f);

            UpdateButtonInteractivity();
        }
        else
        {
            Debug.Log("Yeterli coininiz yok veya can doldurma zaten aktif.");
        }
    }

    public void ButonTiklamaRifle()
    {
        CollectCoin collectCoinScript = FindObjectOfType<CollectCoin>();
        if (collectCoinScript != null && collectCoinScript.coinAmount >= coinCost && !canDoldurmaAktif)
        {
            int playerCoins = collectCoinScript.coinAmount;

            playerCoins -= coinCost; // Coinlerden d���l�yor
            PlayerPrefs.SetInt("CoinAmount", playerCoins);

            canDoldurmaAktif = true;
            player.PerformLeftShiftAction();
            controller.HidePopup();
            controller.ResumeGame();
            Debug.Log("Karakterin can� dolduruldu.");

            // Coin say�s�n� g�ncelle
            collectCoinScript.coinAmount = playerCoins;
            collectCoinScript.coinUI.text = playerCoins.ToString();

            countdownText.text = "5";
            countdownText.gameObject.SetActive(true);
            buton3.gameObject.SetActive(true);

            // Heal prefab�n� olu�tur ve player'�n alt nesnesi yap
            healInstance = Instantiate(healPrefab, player.transform.position, Quaternion.identity);
            healInstance.transform.parent = player.transform;

            // Heal prefab�n� aktifle�tir
            healInstance.SetActive(true);

            InvokeRepeating(nameof(UpdateCountdown), 1f, 1f);

            UpdateButtonInteractivity();
        }
        else
        {
            Debug.Log("Yeterli coininiz yok veya can doldurma zaten aktif.");
        }
    }

    private void UpdateCountdown()
    {
        int remainingTime = int.Parse(countdownText.text);
        countdownText.text = (remainingTime - 1).ToString();

        if (remainingTime <= 1)
        {
            CancelInvoke(nameof(UpdateCountdown));
            countdownText.text = "";
            countdownText.gameObject.SetActive(false);
            buton3.gameObject.SetActive(false);
            Debug.Log("Geri say�m tamamland�.");

            // Heal prefab�n� kapat
            if (healInstance != null)
            {
                healInstance.SetActive(false);
                Destroy(healInstance);
            }
        }
    }

    public void UpdateButtonInteractivity()
    {
        CollectCoin collectCoinScript = FindObjectOfType<CollectCoin>();
        if (collectCoinScript != null && collectCoinScript.coinAmount >= coinCost && !canDoldurmaAktif)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }
    public void SetInteractable(bool interactable)
    {
        button.interactable = interactable;
    }
}

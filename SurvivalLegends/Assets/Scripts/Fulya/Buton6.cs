using UnityEngine;
using UnityEngine.UI;

public class Buton6 : MonoBehaviour
{
    public GameObject flyingCubePrefab;
    public Transform playerTransform;
    public float rotationSpeed = 10f;
    public float destroyDelay = 20f;
    private expController controller;
    private GameObject flyingCubeInstance;
    public Image buton6;
    public Text countdownText;

    [SerializeField]
    private int coinCost = 5; // Al�m i�in gereken coin miktar�

    private void Awake()
    {
        controller = FindObjectOfType<expController>();
    }

    public void ButonTiklama()
    {
        int playerCoins = PlayerPrefs.GetInt("CoinAmount", 0); // Oyuncunun sahip oldu�u coin miktar�

        if (playerCoins >= coinCost)
        {
            playerCoins -= coinCost; // Coinlerden d���l�yor
            PlayerPrefs.SetInt("CoinAmount", playerCoins);

            if (flyingCubeInstance == null)
            {
                SpawnFlyingCube();
                Invoke("DestroyFlyingCube", destroyDelay);

                countdownText.text = "20";
                countdownText.gameObject.SetActive(true);
                buton6.gameObject.SetActive(true);

                InvokeRepeating(nameof(UpdateCountdown), 1f, 1f);

                // Coin say�s�n� g�ncelle
                CollectCoin collectCoinScript = FindObjectOfType<CollectCoin>();
                if (collectCoinScript != null)
                {
                    collectCoinScript.coinAmount = playerCoins;
                    collectCoinScript.coinUI.text = playerCoins.ToString();
                }
            }
            else
            {
                DestroyFlyingCube();

                countdownText.text = "";
                countdownText.gameObject.SetActive(false);
                buton6.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Yeterli coininiz yok.");
        }
        controller.HidePopup();
        controller.ResumeGame();
    }

    private void SpawnFlyingCube()
    {
        if (playerTransform == null || flyingCubePrefab == null)
        {
            return;
        }

        Vector3 spawnPosition = playerTransform.position;
        Quaternion spawnRotation = Quaternion.identity;
        flyingCubeInstance = Instantiate(flyingCubePrefab, spawnPosition, spawnRotation);

        FlyingCube flyingCubeScript = flyingCubeInstance.GetComponent<FlyingCube>();
        flyingCubeScript.target = playerTransform;
        flyingCubeScript.rotationSpeed = rotationSpeed;
    }

    private void DestroyFlyingCube()
    {
        if (flyingCubeInstance != null)
        {
            Destroy(flyingCubeInstance);
            flyingCubeInstance = null;
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
            buton6.gameObject.SetActive(false);
            Debug.Log("Geri say�m tamamland�.");
        }
    }
}

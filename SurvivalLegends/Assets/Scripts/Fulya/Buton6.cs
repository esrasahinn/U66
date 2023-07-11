using UnityEngine;
using UnityEngine.UI;

public class Buton6 : MonoBehaviour
{
    public GameObject flyingCubePrefab; // Uçan küp prefabý
    public Transform playerTransform; // Oyuncu transformu
    public float rotationSpeed = 10f; // Küpün dönme hýzý
    public float destroyDelay = 20f; // Kaybolma gecikmesi süresi
    private expController controller;
    private GameObject flyingCubeInstance; // Oluþturulan uçan küp
    public Image buton6;
    public Text countdownText; // UI metin öðesi

    private void Awake()
    {
        controller = FindObjectOfType<expController>();
    }

    public void ButonTiklama()
    {
        if (flyingCubeInstance == null)
        {
            SpawnFlyingCube();
            Invoke("DestroyFlyingCube", destroyDelay);

            countdownText.text = "20"; // Metin öðesini güncelle
            countdownText.gameObject.SetActive(true); // Metin öðesini etkinleþtir
            buton6.gameObject.SetActive(true); // Resmi etkinleþtir

            InvokeRepeating(nameof(UpdateCountdown), 1f, 1f); // Saniyede bir geri sayýmý güncelle
        }
        else
        {
            DestroyFlyingCube();

            countdownText.text = ""; // Metin öðesini temizle
            countdownText.gameObject.SetActive(false); // Metin öðesini devre dýþý býrak
            buton6.gameObject.SetActive(false); // Resmi devre dýþý býrak
        }
        controller.HidePopup();
        controller.ResumeGame(); // Oyunu devam ettir
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
        int remainingTime = int.Parse(countdownText.text); // Geri sayým süresini al

        remainingTime--; // Geri sayým süresini azalt
        countdownText.text = remainingTime.ToString(); // Metin öðesini güncelle

        if (remainingTime <= 0)
        {
            CancelInvoke(nameof(UpdateCountdown));
            countdownText.text = ""; // Metin öðesini temizle
            countdownText.gameObject.SetActive(false); // Metin öðesini devre dýþý býrak
            buton6.gameObject.SetActive(false); // Resmi devre dýþý býrak

            Debug.Log("Geri sayým tamamlandý.");
        }
    }
}









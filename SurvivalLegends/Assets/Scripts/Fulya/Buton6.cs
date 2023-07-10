using UnityEngine;
using UnityEngine.UI;

public class Buton6 : MonoBehaviour
{
    public GameObject flyingCubePrefab; // U�an k�p prefab�
    public Transform playerTransform; // Oyuncu transformu
    public float rotationSpeed = 10f; // K�p�n d�nme h�z�
    public float destroyDelay = 20f; // Kaybolma gecikmesi s�resi
    private expController controller;
    private GameObject flyingCubeInstance; // Olu�turulan u�an k�p
    public Image buton6;
    public Text countdownText; // UI metin ��esi

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

            countdownText.text = "20"; // Metin ��esini g�ncelle
            countdownText.gameObject.SetActive(true); // Metin ��esini etkinle�tir
            buton6.gameObject.SetActive(true); // Resmi etkinle�tir

            InvokeRepeating(nameof(UpdateCountdown), 1f, 1f); // Saniyede bir geri say�m� g�ncelle
        }
        else
        {
            DestroyFlyingCube();

            countdownText.text = ""; // Metin ��esini temizle
            countdownText.gameObject.SetActive(false); // Metin ��esini devre d��� b�rak
            buton6.gameObject.SetActive(false); // Resmi devre d��� b�rak
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
        int remainingTime = int.Parse(countdownText.text); // Geri say�m s�resini al

        remainingTime--; // Geri say�m s�resini azalt
        countdownText.text = remainingTime.ToString(); // Metin ��esini g�ncelle

        if (remainingTime <= 0)
        {
            CancelInvoke(nameof(UpdateCountdown));
            countdownText.text = ""; // Metin ��esini temizle
            countdownText.gameObject.SetActive(false); // Metin ��esini devre d��� b�rak
            buton6.gameObject.SetActive(false); // Resmi devre d��� b�rak

            Debug.Log("Geri say�m tamamland�.");
        }
    }
}









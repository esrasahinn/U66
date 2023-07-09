using UnityEngine;

public class Buton6 : MonoBehaviour
{
    public GameObject flyingCubePrefab; // U�an k�p prefab�
    public Transform playerTransform; // Oyuncu transformu
    public float rotationSpeed = 10f; // K�p�n d�nme h�z�
    public float destroyDelay = 5f; // Kaybolma gecikmesi s�resi
    private expController controller;
    private GameObject flyingCubeInstance; // Olu�turulan u�an k�p



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
        }
        else
        {
            DestroyFlyingCube();
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
}











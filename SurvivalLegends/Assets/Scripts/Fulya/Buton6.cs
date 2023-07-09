using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Buton6 : MonoBehaviour
{
    public GameObject flyingCubePrefab; // U�an k�p prefab�
    public Transform playerTransform; // Oyuncu transformu
    public float rotationSpeed = 10f; // K�p�n d�nme h�z�
    public float spawnRadius = 2f; // U�an k�plerin olu�turulaca�� yar��ap

    private bool isFlyingCubesActive = false; // U�an k�pler aktif mi?
    private GameObject[] flyingCubes; // Olu�turulan u�an k�plerin listesi

    public void ButonTiklama()
    {
        isFlyingCubesActive = !isFlyingCubesActive;

        if (isFlyingCubesActive)
        {
            SpawnFlyingCubes();
        }
        else
        {
            DestroyFlyingCubes();
        }
    }

    private void SpawnFlyingCubes()
    {
        // Player'�n etraf�nda d�nerek u�an k�pleri olu�turma
        int numCubes = 8; // Olu�turulacak k�p say�s�
        float angleStep = 360f / numCubes; // K�pler aras�ndaki a�� fark�
        flyingCubes = new GameObject[numCubes]; // Olu�turulan k�plerin listesi

        for (int i = 0; i < numCubes; i++)
        {
            float angle = i * angleStep;
            Vector3 spawnPosition = playerTransform.position + Quaternion.Euler(0f, angle, 0f) * (Vector3.forward * spawnRadius);
            Quaternion spawnRotation = Quaternion.Euler(0f, angle, 0f);
            GameObject flyingCube = Instantiate(flyingCubePrefab, spawnPosition, spawnRotation);
            flyingCubes[i] = flyingCube;

            FlyingCube flyingCubeScript = flyingCube.GetComponent<FlyingCube>();
            flyingCubeScript.target = playerTransform;
            flyingCubeScript.rotationSpeed = rotationSpeed;
        }
    }

    private void DestroyFlyingCubes()
    {
        // Olu�turulan u�an k�pleri yok etme
        if (flyingCubes != null)
        {
            foreach (GameObject flyingCube in flyingCubes)
            {
                if (flyingCube != null)
                {
                    Destroy(flyingCube);
                }
            }
        }
    }
}


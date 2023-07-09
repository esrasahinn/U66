using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Buton6 : MonoBehaviour
{
    public GameObject flyingCubePrefab; // Uçan küp prefabý
    public Transform playerTransform; // Oyuncu transformu
    public float rotationSpeed = 10f; // Küpün dönme hýzý
    public float spawnRadius = 2f; // Uçan küplerin oluþturulacaðý yarýçap

    private bool isFlyingCubesActive = false; // Uçan küpler aktif mi?
    private GameObject[] flyingCubes; // Oluþturulan uçan küplerin listesi

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
        // Player'ýn etrafýnda dönerek uçan küpleri oluþturma
        int numCubes = 8; // Oluþturulacak küp sayýsý
        float angleStep = 360f / numCubes; // Küpler arasýndaki açý farký
        flyingCubes = new GameObject[numCubes]; // Oluþturulan küplerin listesi

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
        // Oluþturulan uçan küpleri yok etme
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


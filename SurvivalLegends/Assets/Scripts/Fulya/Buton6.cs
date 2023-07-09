using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buton6 : MonoBehaviour
{
    public GameObject flyingCubePrefab; // Uçan küp prefabý
    public Transform playerTransform; // Oyuncu transformu
    public float rotationSpeed = 10f; // Küpün dönme hýzý

    private GameObject flyingCubeInstance; // Oluþturulan uçan küp

    public void ButonTiklama()
    {
        if (flyingCubeInstance == null)
        {
            SpawnFlyingCube();
        }
        else
        {
            DestroyFlyingCube();
        }
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



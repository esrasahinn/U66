using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    public float rotationSpeed = 10f; // Küpün dönme hýzý

    private void Update()
    {
        // Küpün kendi etrafýnda dönme iþlemi
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}


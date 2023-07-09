using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    public float rotationSpeed = 10f; // K�p�n d�nme h�z�

    private void Update()
    {
        // K�p�n kendi etraf�nda d�nme i�lemi
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}


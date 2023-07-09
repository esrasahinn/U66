using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCube : MonoBehaviour
{
    public Transform target; // Hedef transformu (oyuncu transformu)
    public float rotationSpeed = 10f; // Küpün dönme hýzý
    public float followSpeed = 5f; // Takip hýzý

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        // Küpün kendi etrafýnda dönme iþlemi
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Hedefin pozisyonunu takip etme
        Vector3 targetPosition = target.position;
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, followSpeed * Time.deltaTime);
        transform.position = newPosition;
    }
}

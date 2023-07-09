using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCube : MonoBehaviour
{
    public Transform target; // Hedef transform (oyuncu transformu)
    public float rotationSpeed = 10f; // K�p�n d�nme h�z�

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        // K�p�n hedefe do�ru d�nmesi
        Vector3 direction = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}



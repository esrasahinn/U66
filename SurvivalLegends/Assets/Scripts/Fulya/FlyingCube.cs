using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCube : MonoBehaviour
{
    public Transform target; // Hedef transform (oyuncu transformu)
    public float rotationSpeed = 10f; // Küpün dönme hýzý

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        // Küpün hedefe doðru dönmesi
        Vector3 direction = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}



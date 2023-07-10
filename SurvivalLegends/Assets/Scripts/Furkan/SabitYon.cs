using UnityEngine;

public class SabitYon : MonoBehaviour
{
    public float hiz = 10f; // Okun hareket hýzý

    void Update()
    {
        // Okun ileri yönde hareket etmesini saðla
        transform.Translate(Vector3.forward * hiz * Time.deltaTime);

        // Okun rotasyonunu ileri yöne doðru ayarla
        transform.rotation = Quaternion.LookRotation(transform.forward);
    }
}
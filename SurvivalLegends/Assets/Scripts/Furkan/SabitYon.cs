using UnityEngine;

public class SabitYon : MonoBehaviour
{
    public float hiz = 10f; // Okun hareket h�z�

    void Update()
    {
        // Okun ileri y�nde hareket etmesini sa�la
        transform.Translate(Vector3.forward * hiz * Time.deltaTime);

        // Okun rotasyonunu ileri y�ne do�ru ayarla
        transform.rotation = Quaternion.LookRotation(transform.forward);
    }
}
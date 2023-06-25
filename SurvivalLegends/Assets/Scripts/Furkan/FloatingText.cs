using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime = 3f;
    public float OffsetRange = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DestroyTime);

        Transform target = transform.parent; // Karakterin transformunu alýr
        Vector3 offset = new Vector3(Random.Range(-OffsetRange, OffsetRange), 2f, 0f);
        Vector3 randomOffset = target.position + offset;
        transform.position = randomOffset;
    }
}
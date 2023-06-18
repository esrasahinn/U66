using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] float maxDistance;
    private Vector3 initialPosition;

    public void Initialize(Vector3 startPosition)
    {
        initialPosition = startPosition;
    }

    void Update()
    {
        float distanceTraveled = Vector3.Distance(transform.position, initialPosition);

        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Buraya can eksilme fonksiyonu
            Destroy(gameObject);
        }
    }
}

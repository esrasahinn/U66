using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherProjectileController : ArcherPlayerBehaviour
{
    [SerializeField] float maxDistance;
    private Vector3 initialPosition;

    public ArcherPlayerBehaviour _playH;

    public void Awake()
    {


        _playH = GameObject.FindGameObjectWithTag("Player").GetComponent<ArcherPlayerBehaviour>();
    }
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
            _playH.PlayerTakeDmg(20);
            //_playH.destroyPlayer();

        }
    }
}


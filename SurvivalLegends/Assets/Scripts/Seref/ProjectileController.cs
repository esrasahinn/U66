using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : PlayerBehaviour
{
    [SerializeField] float maxDistance;
    private Vector3 initialPosition;
    private bool isInvincible = false;
    public PlayerBehaviour _playH;



    public void Awake()
    {


        _playH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
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
                Destroy(gameObject);
                _playH.PlayerTakeDmg(100);
                _playH.DestroyPlayer();
            
        }
    }
}
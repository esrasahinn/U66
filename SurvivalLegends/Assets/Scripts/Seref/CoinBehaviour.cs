using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float minFolSpeed;
    [SerializeField] float maxFolSpeed;
    Vector3 velocity = Vector3.zero;
    public float stopY = 0.5f;
    private Rigidbody rb;
    Transform target;
    float yOffset = 1.0f; // Desired height offset from the player's position

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (transform.position.y <= stopY)
        {
            rb.isKinematic = true;
            StartCoroutine(FlyToPlayer());
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    IEnumerator FlyToPlayer()
    {
        yield return new WaitForSeconds(1f);
        Vector3 targetPosition = target.transform.position + new Vector3(0, yOffset, 0);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, Time.deltaTime * Random.Range(minFolSpeed, maxFolSpeed));
    }
}

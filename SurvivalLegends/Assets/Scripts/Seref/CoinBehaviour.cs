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
        float smoothTime = Time.deltaTime * Random.Range(minFolSpeed, maxFolSpeed);
        float maxDistance = Vector3.Distance(transform.position, target.position);
        float thresholdDistance = 2f; // Adjust this threshold to control when to start decreasing smoothTime

        while (transform.position != target.position)
        {
            if (Vector3.Distance(transform.position, target.position) <= thresholdDistance)
            {
                smoothTime = 0.05f;
            }

            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
            yield return null;
        }
    }
}

//// Debug.Log("FlyToPlayer coroutine started.");
// yield return new WaitForSeconds(1f);

// Vector3 targetPosition = target.position + new Vector3(0, 1, 0);
// while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
// {
//     transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, Time.deltaTime * Random.Range(minFolSpeed, maxFolSpeed));
//    // Debug.Log("Current position: " + transform.position);
//    // Debug.Log("Target position: " + targetPosition);
//     yield return null;
// }
// //Debug.Log("Coin reached the player.");
//  }
//}
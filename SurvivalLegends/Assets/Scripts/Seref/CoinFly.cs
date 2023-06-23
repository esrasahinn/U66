using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CoinFly : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    bool startFly = false;
    Vector3 velocity = Vector3.zero;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void StartFly()
    {
        startFly = true;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        if (startFly)
            {
                transform.position = Vector3.SmoothDamp(transform.position, GameObject.Find("Player").transform.position, ref velocity, Time.deltaTime * Random.Range(7, 11));
            }
        
    }
}

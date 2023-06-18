using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCoin : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public int minCollectibles;
    public int maxCollectibles;
    public float minSplashForce;
    public float maxSplashForce;
    public float verticalForceMultiplier = 2f;
    public float splashRadius;
    //Animator animator;


    public void CoinDrop()
    {
        int numCollectibles = Random.Range(minCollectibles, maxCollectibles + 1);

        for (int i = 0; i < numCollectibles; i++)
        {
            GameObject collectible = Instantiate(collectiblePrefab, transform.position, Quaternion.identity);
            Rigidbody rb = collectible.GetComponent<Rigidbody>();

            float splashForce = Random.Range(minSplashForce, maxSplashForce);
            Vector3 splashDirection = new Vector3(Random.Range(-1f, 1f), verticalForceMultiplier, Random.Range(-1f, 1f)).normalized;
            Vector3 forceVector = splashDirection * splashForce;
            rb.AddForce(forceVector, ForceMode.Impulse);
        }
    }
}

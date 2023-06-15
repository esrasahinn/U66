using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCoin : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public float minSplashForce;
    public float maxSplashForce;
    public float splashRadius;

    public void CoinDrop()
    {
        int coinDropAmount = Random.Range(3,4);

        for (int i = 0; i < coinDropAmount; i++)
        {
            GameObject collectible = Instantiate(collectiblePrefab, transform.position + Random.insideUnitSphere * splashRadius, Quaternion.identity);

            Rigidbody rb = collectible.GetComponent<Rigidbody>();

            float splashForce = Random.Range(minSplashForce, maxSplashForce);
            Vector3 splashDirection = Random.onUnitSphere.normalized;
            Vector3 forceVector = splashDirection * splashForce;
            rb.AddForce(forceVector, ForceMode.Impulse);

            //StartCoroutine(StartAnimation());
        }


    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(2f); 
        GetComponent<Animator>().Play("CoinAnimation"); 
    }

}

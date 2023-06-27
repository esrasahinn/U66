using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public int coinAmount = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinAmount++;
            Debug.Log(coinAmount + " coins.");
            Destroy(other.gameObject);
            //other.gameObject.SetActive(false);
        }
    }
} 
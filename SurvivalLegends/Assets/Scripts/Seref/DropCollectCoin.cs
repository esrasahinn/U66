using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCollectCoin : MonoBehaviour
{
    public GameObject coinPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CoinDrop()
    {
        Vector3 position = transform.position;
        GameObject coin = Instantiate(coinPrefab, position, Quaternion.identity);
    }
}

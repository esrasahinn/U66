using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGemChest : MonoBehaviour
{
    public ShopManager gemScript;
    public int earnedGem;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GemChest"))
        {
            gemScript.diamondScript.diamondAmount += other.GetComponent<GemAmount>().gemAmount;
            //gemScript.diamondUI.text = gemScript.diamondScript.diamondAmount.ToString();
            gemScript.SaveDiamondCount();
            Debug.Log("Gem Collected");
            Destroy(gameObject);
        }
    }
}
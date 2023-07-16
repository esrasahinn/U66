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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("GemChest"))
        {
            int gemAmount = hit.gameObject.GetComponent<GemAmount>().gemAmount;
            gemScript.diamondScript.diamondAmount += gemAmount;
            gemScript.diamondUI.text = gemScript.diamondScript.diamondAmount.ToString();
            gemScript.SaveDiamondCount();
            Debug.Log("Gem Collected");
            Destroy(hit.gameObject);
        }
    }
}
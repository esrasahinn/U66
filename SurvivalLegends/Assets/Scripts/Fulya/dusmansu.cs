using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dusmansu : MonoBehaviour
{
    [SerializeField] Healthbar _healthbar;
    DropCoin coinScript;

    void Start()
    {
        coinScript = gameObject.GetComponent<DropCoin>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerTakeDmg(100);
            Debug.Log(GameManager.gameManager._playerHealth.Health);

            if (GameManager.gameManager._playerHealth.Health <= 0)
            {                
                Destroy(gameObject);
                coinScript.CoinDrop();
            }
        }

     
    }

    private void PlayerTakeDmg(int dmg)
    {
        GameManager.gameManager._playerHealth.DmgUnit(dmg);
        _healthbar.SetHealth(GameManager.gameManager._playerHealth.Health);
    }

    private void PlayerHeal(int healing)
    {
        GameManager.gameManager._playerHealth.HealUnit(healing);
        _healthbar.SetHealth(GameManager.gameManager._playerHealth.Health);
    }
}
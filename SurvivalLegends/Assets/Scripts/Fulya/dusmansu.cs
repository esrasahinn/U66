using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dusmansu : MonoBehaviour
{
    [SerializeField] Healthbar _healthbar;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerTakeDmg(20);
            Debug.Log(GameManager.gameManager._playerHealth.Health);

            if (GameManager.gameManager._playerHealth.Health <= 0)
            {
                Destroy(gameObject);
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
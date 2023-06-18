using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
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
            Debug.Log(GameManager.gameManager._dusmanHealth.Health);

            if (GameManager.gameManager._dusmanHealth.Health <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayerHeal(10);
            Debug.Log(GameManager.gameManager._dusmanHealth.Health);
        }
    }

    private void PlayerTakeDmg(int dmg)
    {
        GameManager.gameManager._dusmanHealth.DmgUnit(dmg);
        _healthbar.SetHealth(GameManager.gameManager._dusmanHealth.Health);
    }

    private void PlayerHeal(int healing)
    {
        GameManager.gameManager._dusmanHealth.HealUnit(healing);
        _healthbar.SetHealth(GameManager.gameManager._dusmanHealth.Health);
    }
}
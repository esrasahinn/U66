using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] Healthbar _healthbar;
    private int _health = 100; // Karakterin can deðeri
    private static PlayerBehaviour _instance;

    public static PlayerBehaviour GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

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

    public void destroyPlayer()
    {

        if (GameManager.gameManager._dusmanHealth.Health <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void PlayerTakeDmg(int dmg)
    {
        _health -= dmg;
        _healthbar.SetHealth(_health);

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void PlayerHeal(int healing)
    {
        GameManager.gameManager._dusmanHealth.HealUnit(healing);
        _healthbar.SetHealth(GameManager.gameManager._dusmanHealth.Health);
    }
}
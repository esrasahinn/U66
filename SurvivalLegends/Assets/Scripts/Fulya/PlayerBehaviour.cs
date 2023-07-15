using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] Healthbar _healthbar;
    public int _health = 100;
    private static PlayerBehaviour _instance;
    private Animator _animator;
    private MenzileGirenDusmanaAtesVeDonme menzileGirenDusmanaAtesVeDonme; // MenzileGirenDusmanaAtesVeDonme scriptine eriþmek için referans
    private bool isDead = false;
    private bool isImmuneToDamage = false; // Hasar almama durumu

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

    private void Start()
    {
        _animator = GetComponent<Animator>();
        menzileGirenDusmanaAtesVeDonme = GetComponent<MenzileGirenDusmanaAtesVeDonme>(); // MenzileGirenDusmanaAtesVeDonme componentini al
    }

    private void Update()
    {
        if (!isDead && !isImmuneToDamage)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerTakeDmg(20);
                Debug.Log(GameManager.gameManager._dusmanHealth.Health);

                if (GameManager.gameManager._dusmanHealth.Health <= 0)
                {
                    DestroyPlayer();
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                PlayerHeal(10);
                Debug.Log(GameManager.gameManager._dusmanHealth.Health);
            }
        }
    }

    public void DestroyPlayer()
    {
        isDead = true;
        _animator.SetBool("Death", true);
        StartCoroutine(ResetAfterAnimation());
    }

    private IEnumerator ResetAfterAnimation()
    {
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f; // Sahneyi duraklat
    }

    //  private IEnumerator PauseAfterDeath()
    //  {
    //      yield return new WaitForSeconds(3f); // 3 saniye bekle
    //      Time.timeScale = 0f; // Oyun zamanýný durdur
    //      Debug.Log("Sahne duraklatýldý!");
    //  }

    //  private IEnumerator ResetAfterAnimation()
    //  {
    //      yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
    //      _health = 0; // Can deðerini sýfýrla
    //      _healthbar.SetHealth(_health);
    //      gameObject.SetActive(true);
    //      menzileGirenDusmanaAtesVeDonme.enabled = false; // MenzileGirenDusmanaAtesVeDonme scriptini devre dýþý býrak
    //  }

    public void PlayerTakeDmg(int dmg)
    {
        if (!isImmuneToDamage)
        {
            _health -= dmg;
            _healthbar.SetHealth(_health);

            if (_health <= 0)
            {
                DestroyPlayer();
            }
        }
    }

    public void PlayerHeal(int healing)
    {
        GameManager.gameManager._dusmanHealth.HealUnit(healing);
        _health = GameManager.gameManager._dusmanHealth.Health;
        _healthbar.SetHealth(_health);
    }

    public void ActivateImmunity(float duration)
    {
        StartCoroutine(ImmuneToDamage(duration));
    }

    private IEnumerator ImmuneToDamage(float duration)
    {
        isImmuneToDamage = true;
        yield return new WaitForSeconds(duration);
        isImmuneToDamage = false;
        Debug.Log("Hasar almama süresi doldu.");
    }

    public void DeactivateImmunity()
    {
        isImmuneToDamage = false;
    }

    public bool IsImmuneToDamage
    {
        get { return isImmuneToDamage; }
    }

    public void PerformLeftShiftAction()
    {
        PlayerHeal(10);
        Debug.Log(GameManager.gameManager._dusmanHealth.Health);
    } 
}

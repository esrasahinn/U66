using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : PlayerBehaviour
{
    [SerializeField] float maxDistance;
    private Vector3 initialPosition;
    private bool isInvincible = false;
    public PlayerBehaviour _playH;

    private bool ability2Active = false;
    private float ability2Duration = 30f;
    private float ability2Timer = 0f;
    private float ability2DamageMultiplier = 2f;

    public void Awake()
    {


        _playH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }
    public void Initialize(Vector3 startPosition)
    {
        initialPosition = startPosition;
    }

    void Update()
    {
        if (isInvincible)
        {
            ability2Timer -= Time.deltaTime;
            if (ability2Timer <= 0f)
            {
                isInvincible = false; // Yeni özelliði devre dýþý býrak
            }
        }

        float distanceTraveled = Vector3.Distance(transform.position, initialPosition);

        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
    public void ActivateAbility2()
    {
        ability2Active = true;
        ability2Timer = ability2Duration;
        isInvincible = true; // Yeni özelliði etkinleþtir
        StartCoroutine(DisableAbility2AfterDuration());
    }

    private IEnumerator DisableAbility2AfterDuration()
    {
        yield return new WaitForSeconds(ability2Duration);
        ability2Active = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (ability2Active && isInvincible)
            {
                Destroy(gameObject);
                _playH.PlayerTakeDmg(0);
                _playH.destroyPlayer();
            }
            else
            {
                Destroy(gameObject);
                _playH.PlayerTakeDmg(20);
                _playH.destroyPlayer();
            }
        }
    }
}
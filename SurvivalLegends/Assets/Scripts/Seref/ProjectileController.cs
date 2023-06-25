using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : PlayerBehaviour
{
    [SerializeField] float maxDistance;
    private Vector3 initialPosition;

    public PlayerBehaviour _playH;
    public ArcherPlayerBehaviour _aplayH;


    private bool canTriggerEnter = true;
    private bool ability2Active = false;
    private float ability2Duration = 30f;
    private float ability2Timer = 0f;
    private float ability2DamageMultiplier = 2f;

    public void Awake()
    {


        _playH = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        _aplayH = GameObject.FindGameObjectWithTag("Player").GetComponent<ArcherPlayerBehaviour>();
    }
    public void Initialize(Vector3 startPosition)
    {
        initialPosition = startPosition;
    }

    void Update()
    {
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
        canTriggerEnter = false; // Trigger etkinliðini durdur
        StartCoroutine(DisableAbility2AfterDuration());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canTriggerEnter && other.gameObject.CompareTag("Player"))
        {
            if (ability2Active)
            {
                Debug.Log("Ability 2  de karakterin hasar almama kýsmý");
                _playH.PlayerTakeDmg(0);
            }
            else
            {
                _playH.PlayerTakeDmg(20);
            }
        }
    }
    private IEnumerator DisableAbility2AfterDuration()
    {
        yield return new WaitForSeconds(ability2Duration);
        ability2Active = false;
        canTriggerEnter = true;
    }



}

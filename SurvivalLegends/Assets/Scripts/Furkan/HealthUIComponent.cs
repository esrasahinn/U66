using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIComponent : MonoBehaviour
{
    [SerializeField] HealthBar healthBarToSpawn;
    [SerializeField] Transform healthBarAttachPoint;
    [SerializeField] EnemyAI EnemyAI;

    private void Start()
    {
        InGameUI inGameUI = FindObjectOfType<InGameUI>();
        HealthBar newHealthBar = Instantiate(healthBarToSpawn, inGameUI.transform);
        newHealthBar.Init(healthBarAttachPoint);
       // dusman.onHealthChange += newHealthBar.SetHealtSliderValue;
    }
}
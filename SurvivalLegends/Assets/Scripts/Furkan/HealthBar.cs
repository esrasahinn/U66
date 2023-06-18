using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;

    //private Transform _attachPoint;

    public void Init(Transform attachPoint)
    {
        //_attachPoint = attachPoint;
    }

    public void SetHealtSliderValue(float health, float delta, float maxHealth)
    {
        healthSlider.value = health / maxHealth;
    }

    private void Update()
    {
        //Vector3 attachScreenPoint = Camera.main.WorldToScreenPoint(_attachPoint.position);
        //transform.position = attachScreenPoint;
    }
}

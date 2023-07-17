using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NinjaPlayer : MonoBehaviour
{
    [SerializeField] JoyStick moveStick;
    [SerializeField] JoyStick aimStick;
    [SerializeField] CharacterController characterController;
    public float moveSpeed = 20f;
    [SerializeField] float maxMoveSpeed = 50f;
    [SerializeField] float minMoveSpeed = 10f;
    [SerializeField] float turnSpeed = 30f;
    [SerializeField] float animTurnSpeed = 30f;
    [SerializeField] float health = 100f;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] Slider healthSlider;

    [Header("Inventory")]
    [SerializeField] NinjaInventoryComponent inventoryComponent;

    [SerializeField] ShopSystem testShopSystem;
    [SerializeField] ShopItem testItem;

    void TestPurchase()
    {
        testShopSystem.TryPurchase(testItem, GetComponent<CreditComponent>());
    }

    Vector2 moveInput;
    Vector2 aimInput;
    Camera mainCam;
    CameraController cameraController;
    Animator animator;
    private bool ability1Active = false;

    private float ability1Duration = 30f;
    private float ability1Timer = 0f;
    private float ability1SpeedMultiplier = 2f;

    public static NinjaPlayer instance;
    float animatorTurnSpeed;
    public bool isSpeedBoostActive = false;
    public float originalMoveSpeed;
    public float speedBoostDuration;

    public void AddMoveSpeed(float boostAmt)
    {
        moveSpeed += boostAmt;
        moveSpeed = Mathf.Clamp(moveSpeed, minMoveSpeed, maxMoveSpeed);
    }

    void Start()
    {
        moveStick.onStickValueUpdated += moveStickUpdated;
        //  aimStick.onStickValueUpdated += aimStickUpdated;
        mainCam = Camera.main;
        cameraController = FindObjectOfType<CameraController>();
        animator = GetComponent<Animator>();

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = health;
        }

        //Invoke("TestPurchase", 3);
    }

    void moveStickUpdated(Vector2 inputValue)
    {
        moveInput = inputValue;
    }
    public void AttackPoint()
    {
        inventoryComponent.GetActiveWeapon().Attack();
    }

    void OnAimJoystickValueChanged(Vector2 inputValue)
    {
        aimInput = inputValue;
    }

    void OnMoveJoystickValueChanged(Vector2 inputValue)
    {
        moveInput = inputValue;
    }

    Vector3 JoystickInputToWorldDir(Vector3 inputVal)
    {
        Vector3 rightDir = mainCam.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        return rightDir * inputVal.x + upDir * inputVal.y;
    }

    void Update()
    {
        PerformMoveAndAim();
        UpdateCamera();
    }

    private void PerformMoveAndAim()
    {
        Vector3 moveDir = JoystickInputToWorldDir(moveInput);
        characterController.Move(moveDir * Time.deltaTime * moveSpeed);
        UpdateAim(moveDir);

        float aim = Vector3.Dot(moveDir, transform.forward);
        float rforward = Vector3.Dot(moveDir, transform.forward);
        float forward = Vector3.Dot(moveDir, transform.forward);
        float right = Vector3.Dot(moveDir, transform.right);

        animator.SetFloat("forwardSpeed", forward);
        animator.SetFloat("rightSpeed", right);

        animator.SetFloat("Aim", aim);
        animator.SetFloat("rforward", rforward);

        if (Mathf.Abs(moveInput.x) > 0 || Mathf.Abs(moveInput.y) > 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    private void UpdateAim(Vector3 currentMoveDir)
    {
        Vector3 aimDir = currentMoveDir;
        if (aimInput.magnitude != 0)
        {
            aimDir = JoystickInputToWorldDir(aimInput);
        }
        RotateTowards(aimDir);
    }

    private void UpdateCamera()
    {
        if (moveInput.magnitude != 0 && aimInput.magnitude == 0 && cameraController != null)
        {
            cameraController.AddYawInput(moveInput.x);
        }
    }

    private void RotateTowards(Vector3 aimDir)
    {
        float currentTurnSpeed = 0;
        if (aimDir.magnitude != 0)
        {
            Quaternion prevRot = transform.rotation;
            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDir, Vector3.up), turnLerpAlpha);

            Quaternion currentRot = transform.rotation;
            float dir = Vector3.Dot(aimDir, transform.right) > 0 ? 1 : -1;
            float rotationDelta = Quaternion.Angle(prevRot, currentRot) * dir;
            currentTurnSpeed = rotationDelta / Time.deltaTime;
        }
        animatorTurnSpeed = Mathf.Lerp(animatorTurnSpeed, currentTurnSpeed, Time.deltaTime * animTurnSpeed);
        animator.SetFloat("turnSpeed", animatorTurnSpeed);
    }

    private void Awake()
    {
        instance = this;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (healthSlider != null)
        {
            healthSlider.value = health;
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died");
        // Additional actions to be performed when the player dies.
        // Destroy(gameObject);
    }
}

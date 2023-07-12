using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NinjaMulti : NetworkBehaviour
{
    [SerializeField] JoyStick moveStick;
    [SerializeField] JoyStick aimStick;
    [SerializeField] CharacterController characterController;
    [SerializeField] public float moveSpeed = 20f;
    [SerializeField] float maxMoveSpeed = 50f;
    [SerializeField] float minMoveSpeed = 10f;
    [SerializeField] float turnSpeed = 30f;
    [SerializeField] float animTurnSpeed = 30f;
    [SerializeField] float can = 100f;
    [SerializeField] float maxCan = 100f;
    //a
    [SerializeField] Slider canBariSlider; // Can Çubuğu Slider bileşeni

    [Header("Inventory")]
    [SerializeField] NinjaInventoryComponent inventoryComponent;

    [SerializeField] ShopSystem testShopSystem;
    [SerializeField] ShopItem testItem;

    Vector2 moveInput;
    Vector2 aimInput;
    public ArcherPlayerBehaviour _playH;
    Camera mainCam;
    CameraController cameraController;
    Animator animator;
    private bool ability1Active = false;

    private float ability1Duration = 30f;
    private float ability1Timer = 0f;
    private float ability1SpeedMultiplier = 2f;

    public static NinjaMulti instance;
    float animatorTurnSpeed;

    public void AddMoveSpeed(float boostAmt)
    {
        moveSpeed += boostAmt;
        moveSpeed = Mathf.Clamp(moveSpeed, minMoveSpeed, maxMoveSpeed);
    }

    Vector3 StickInputToWorldDir(Vector3 inputVal)
    {
        Vector3 rightDir = mainCam.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        return rightDir * inputVal.x + upDir * inputVal.y;
    }

    void Start()
    {
        if (!IsOwner)
        {
            // Sadece sahibi olduğunuz karakteri kontrol edebilirsiniz, bu yüzden diğer oyuncuların bu kodu çalıştırmasını engellemek için geri dönün.
            return;
        }

        moveStick.onStickValueUpdated += moveStickUpdated;
        aimStick.onStickValueUpdated += aimStickUpdated;
        mainCam = Camera.main;
        cameraController = FindObjectOfType<CameraController>();
        animator = GetComponent<Animator>();

        if (canBariSlider != null)
        {
            canBariSlider.maxValue = maxCan; // Can Çubuğunun maksimum değerini ayarla
            canBariSlider.value = can; // Can Çubuğunun değerini ayarla
        }

        // Invoke("TestPurchase", 3);
    }

    [ServerRpc]
    void TestPurchaseServerRpc()
    {
        if (IsOwner)
        {
            testShopSystem.TryPurchase(testItem, GetComponent<CreditComponent>());
        }
    }

    public void AttackPoint()
    {
        if (!IsOwner)
        {
            // Sadece sahibi olduğunuz karakterin saldırı yapabilmesi için geri dönün.
            return;
        }

        inventoryComponent.GetActiveWeapon().Attack();
    }

    void aimStickUpdated(Vector2 inputValue)
    {
        aimInput = inputValue;
    }

    void moveStickUpdated(Vector2 inputValue)
    {
        moveInput = inputValue;
    }

    [ClientRpc]
    void MovePlayerClientRpc(Vector3 moveDir, Vector3 aimDir, float aim, float rforward, float forward, float right)
    {
        characterController.Move(moveDir * Time.deltaTime * moveSpeed);

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

    void UpdateAim(Vector3 currentMoveDir)
    {
        Vector3 AimDir = currentMoveDir;
        if (aimInput.magnitude != 0)
        {
            AimDir = StickInputToWorldDir(aimInput);
        }
        RotateTowards(AimDir);
    }

  void UpdateCamera()
{
    if (moveInput.magnitude != 0 && cameraController != null)
    {
        cameraController.AddYawInput(moveInput.x);
    }
}

    void RotateTowards(Vector3 AimDir)
    {
        if (AimDir.magnitude != 0)
        {
            Quaternion prevRot = transform.rotation;
            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(AimDir, Vector3.up), turnLerpAlpha);
            Quaternion currentRot = transform.rotation;
            float Dir = Vector3.Dot(AimDir, transform.right) > 0 ? 1 : -1;
            float rotationDelta = Quaternion.Angle(prevRot, currentRot) * Dir;
            animatorTurnSpeed = rotationDelta / Time.deltaTime;
        }

        animator.SetFloat("turnSpeed", animatorTurnSpeed);
    }

   void Update()
{
    if (!IsOwner)
    {
        return;
    }

    PerformMoveAndAim();
    UpdateCamera();
}

    void PerformMoveAndAim()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        moveInput = new Vector2(horizontalInput, verticalInput);

        Vector3 MoveDir = StickInputToWorldDir(moveInput);
        Vector3 AimDir = MoveDir;
        if (aimInput.magnitude != 0)
        {
            AimDir = StickInputToWorldDir(aimInput);
        }
        RotateTowards(AimDir);

        float aim = Vector3.Dot(MoveDir, transform.forward);
        float rforward = Vector3.Dot(MoveDir, transform.forward);
        float forward = Vector3.Dot(MoveDir, transform.forward);
        float right = Vector3.Dot(MoveDir, transform.right);

        MovePlayerClientRpc(MoveDir, AimDir, aim, rforward, forward, right);
    }

    public void TakeDamage(int damage)
    {
        if (!IsServer)
        {
            // Sadece sunucu tarafından hasar alabilirsiniz, bu yüzden diğer oyuncuların bu kodu çalıştırmasını engellemek için geri dönün.
            return;
        }

        can -= damage;

        if (canBariSlider != null)
        {
            canBariSlider.value = can; // Can Çubuğunun değerini güncelle
        }

        if (can <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died");
        // Ölümle ilgili yapılması gereken işlemler buraya eklenir.
        // Destroy(gameObject); // Karakteri yok etmek için kullanılabilir.
    }

    void Awake()
    {
        instance = this;
    }
}

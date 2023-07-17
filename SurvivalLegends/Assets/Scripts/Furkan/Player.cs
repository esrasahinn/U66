

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] JoyStick moveStick;
    [SerializeField] JoyStick aimStick;
    [SerializeField] CharacterController characterController;
    public float moveSpeed = 20f;
    [SerializeField] float maxMoveSpeed = 50f;
    [SerializeField] float minMoveSpeed = 10f;
    [SerializeField] float turnSpeed = 30f;
    [SerializeField] float animTurnSpeed = 30f;
    [SerializeField] float can = 100f;
    [SerializeField] float maxCan = 100f;
    private float rotationCorrectionFactor = 50f;

    [SerializeField] Slider canBariSlider; // Can çubuðu Slider bileþeni

    [Header("Inventory")]
    [SerializeField] InventoryComponent inventoryComponent;

    [SerializeField] ShopSystem testShopSystem;
    [SerializeField] ShopItem testItem;

    void TestPurchase()
    {
        testShopSystem.TryPurchase(testItem, GetComponent<CreditComponent>());
    }

    Vector2 moveInput;
    Vector2 aimInput;
    public PlayerBehaviour _playH;
    Camera mainCam;
    CameraController cameraController;
    Animator animator;

    public static Player instance;
    float animatorTurnSpeed;

    // Hýzlandýrma özelliði için deðiþkenler
    public bool isSpeedBoostActive = false;
    public float originalMoveSpeed;
    public float speedBoostDuration;

    internal void AddMoveSpeed(float boostAmt)
    {
        moveSpeed += boostAmt;
        moveSpeed = Mathf.Clamp(moveSpeed, minMoveSpeed, maxMoveSpeed);
    }

    // Start is called before the first frame update
    void Start()
    {
        moveStick.onStickValueUpdated += moveStickUpdated;
        aimStick.onStickValueUpdated += aimStickUpdated;
        mainCam = Camera.main;
        cameraController = FindObjectOfType<CameraController>();
        animator = GetComponent<Animator>();

        if (canBariSlider != null)
        {
            canBariSlider.maxValue = maxCan; // Can çubuðunun maksimum deðerini ayarla
            canBariSlider.value = can; // Can çubuðunun deðerini ayarla
        }

        // Invoke("TestPurchase", 3);
    }

    public void AttackPoint()
    {
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

    Vector3 StickInputToWorldDir(Vector3 inputVal)
    {
        Vector3 rightDir = mainCam.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        return rightDir * inputVal.x + upDir * inputVal.y;
    }

    // Update is called once per frame
    void Update()
    {
        PerformMoveAndAim();
        UpdateCamera();
        // SetRunningAnimation((Mathf.Abs(Horizontal) > 0 || Mathf.Abs(Vertical) > 0));
    }

    private void PerformMoveAndAim()
    {
        Vector3 MoveDir = StickInputToWorldDir(moveInput);
        characterController.Move(MoveDir * Time.deltaTime * moveSpeed);

        UpdateAim(MoveDir);

        float aim = Vector3.Dot(MoveDir, transform.forward);
        float rforward = Vector3.Dot(MoveDir, transform.forward);
        float forward = Vector3.Dot(MoveDir, transform.forward);
        float right = Vector3.Dot(MoveDir, transform.right);

        animator.SetFloat("forwardSpeed", forward);
        animator.SetFloat("rightSpeed", right);

        animator.SetFloat("Aim", aim);
        animator.SetFloat("rforward", rforward);

        // Hareket giriþi varsa animasyonu çalýþtýr, yoksa durumu güncelle
        if (Mathf.Abs(moveInput.x) > 0 || Mathf.Abs(moveInput.y) > 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    // private void SetRunningAnimation(bool run) //yeni karakter için(warrior)
    // {
    //     animator.SetBool("Running", run);
    // }

    private void UpdateAim(Vector3 currentMoveDir)
    {
        Vector3 AimDir = currentMoveDir;
        if (aimInput.magnitude != 0)
        {
            AimDir = StickInputToWorldDir(aimInput);
        }
        RotateTowards(AimDir);
    }

    private void UpdateCamera()
    {
        // Oyuncu hareket ediyor ama niþan almýyor ve cameraController var
        if (moveInput.magnitude != 0 && aimInput.magnitude == 0 && cameraController != null)
        {
            cameraController.AddYawInput(moveInput.x);
        }
    }

    private void RotateTowards(Vector3 AimDir)
    {
        float currentTurnSpeed = 0;
        if (AimDir.magnitude != 0)
        {
            Quaternion prevRot = transform.rotation;
            Quaternion targetRot = Quaternion.LookRotation(AimDir, Vector3.up);

            float smoothTime = 0.1f; // Dönüþün hýzýný kontrol etmek için kullanýlan bir süre
            Quaternion currentRot = Quaternion.Slerp(prevRot, targetRot, smoothTime);

            // Düzeltme faktörü uygulayýn
            currentRot = Quaternion.Lerp(prevRot, currentRot, rotationCorrectionFactor);

            float Dir = Vector3.Dot(AimDir, transform.right) > 0 ? 1 : -1;
            float rotationDelta = Quaternion.Angle(prevRot, currentRot) * Dir;
            currentTurnSpeed = rotationDelta / Time.deltaTime;
            transform.rotation = currentRot;
        }
        animatorTurnSpeed = Mathf.Lerp(animatorTurnSpeed, currentTurnSpeed, Time.deltaTime * animTurnSpeed);
        animator.SetFloat("turnSpeed", animatorTurnSpeed);
    }

    private void Awake()
    {
        instance = this;
        originalMoveSpeed = moveSpeed; // Ýlk hýzý kaydet
    }

    public void HasarAl(int hasar)
    {
        can -= hasar;

        if (canBariSlider != null)
        {
            canBariSlider.value = can; // Can çubuðunun deðerini güncelle
        }

        if (can <= 0)
        {
            Olum();
        }
    }

    private void Olum()
    {
        Debug.Log("Player Oldu");
        // Düþmanýn ölümüyle ilgili yapýlmasý gereken iþlemler buraya eklenebilir.
        // Destroy(gameObject); // Düþman nesnesini yok etmek için kullanabilirsiniz.
    }
}

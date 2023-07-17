using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
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

    void Update()
    {
        HandleMovement();
        HandleAim();
        UpdateCamera();
        // SetRunningAnimation((Mathf.Abs(Horizontal) > 0 || Mathf.Abs(Vertical) > 0));
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        characterController.Move(moveDirection * Time.deltaTime * moveSpeed);

        float aim = Vector3.Dot(moveDirection, transform.forward);
        float rforward = Vector3.Dot(moveDirection, transform.forward);
        float forward = Vector3.Dot(moveDirection, transform.forward);
        float right = Vector3.Dot(moveDirection, transform.right);

        animator.SetFloat("forwardSpeed", forward);
        animator.SetFloat("rightSpeed", right);

        animator.SetFloat("Aim", aim);
        animator.SetFloat("rforward", rforward);

        // Hareket giriþi varsa animasyonu çalýþtýr, yoksa durumu güncelle
        if (moveDirection.magnitude > 0)
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

    private void HandleAim()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray mouseRay = mainCam.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        if (groundPlane.Raycast(mouseRay, out float rayDistance))
        {
            Vector3 targetPosition = mouseRay.GetPoint(rayDistance);
            RotateTowards(targetPosition - transform.position);
        }
    }

    private void UpdateCamera()
    {
        // Oyuncu hareket ediyor ama niþan almýyor ve cameraController var
        if (characterController.velocity.magnitude > 0 && Input.GetAxis("Mouse X") == 0 && cameraController != null)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            cameraController.AddYawInput(horizontalInput);
        }
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 targetDirection = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
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

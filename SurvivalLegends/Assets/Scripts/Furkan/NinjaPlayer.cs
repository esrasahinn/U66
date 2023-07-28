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
    [SerializeField] float can = 100f;
    [SerializeField] float maxCan = 100f;
    //a
    [SerializeField] Slider canBariSlider; // Can çubuğu Slider bileşeni

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
    public ArcherPlayerBehaviour _playH;
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


    //public void ActivateAbility1()
    //{
    //    ability1Active = true;
    //    ability1Timer = ability1Duration;
    //    moveSpeed *= ability1SpeedMultiplier;
    //    StartCoroutine(DisableAbility1AfterDuration());
    //}
    //
    //
    //
    //public IEnumerator DisableAbility1AfterDuration()
    //{
    //    yield return new WaitForSeconds(ability1Duration);
    //    ability1Active = false;
    //    moveSpeed /= ability1SpeedMultiplier;
    //}




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
            canBariSlider.maxValue = maxCan; // Can çubuğunun maksimum değerini ayarla
            canBariSlider.value = can; // Can çubuğunun değerini ayarla
        }


        //        Invoke("TestPurchase", 3);
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
        //SetRunningAnimation((Math.Abs(Horizontal) > 0 || Math.Abs(Vertical) > 0));
    }

    private void PerformMoveAndAim()
    {
        // Hareket girişini al
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        moveInput = new Vector2(horizontalInput, verticalInput);

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

        // Hareket girişi varsa animasyonu çalıştır, yoksa durumu güncelle
        if (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    //private void SetRunningAnimation(bool run) //yeni karakter için(warrior)
    //{
    //    animator.SetBool("Running", run);
    //}

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
        // Oyuncu hareket ediyor ama nişan almıyor ve cameraController var
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

            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(AimDir, Vector3.up), turnLerpAlpha);//yavaş dönüş için.

            Quaternion currentRot = transform.rotation;
            float Dir = Vector3.Dot(AimDir, transform.right) > 0 ? 1 : -1;
            float rotationDelta = Quaternion.Angle(prevRot, currentRot) * Dir;
            currentTurnSpeed = rotationDelta / Time.deltaTime;
        }
        animatorTurnSpeed = Mathf.Lerp(animatorTurnSpeed, currentTurnSpeed, Time.deltaTime * animTurnSpeed);
        animator.SetFloat("turnSpeed", animatorTurnSpeed);
    }

    private void Awake()
    {
        instance = this;
    }

    public void HasarAl(int hasar)
    {


        can -= hasar;

        if (canBariSlider != null)
        {
            canBariSlider.value = can; // Can çubuğunun değerini güncelle
        }

        if (can <= 0)
        {
            Olum();
        }


    }



    private void Olum()
    {
        Debug.Log("Player Oldu");
        // Düşmanın ölümüyle ilgili yapılması gereken işlemler buraya eklenebilir.
        // Destroy(gameObject); // Düşman nesnesini yok etmek için kullanabilirsiniz.
    }

}
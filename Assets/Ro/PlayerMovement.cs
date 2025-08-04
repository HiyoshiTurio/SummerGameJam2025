using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
  
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1.0f;
    [SerializeField] private bool isPlayer1 = true;
    [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;

   
    private Rigidbody rb;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float cooldownTimer = 0f;

    private Vector3 moveDirection = Vector3.zero;

 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleMovementInput();
        HandleDashLogic();

    }


    void FixedUpdate()
    {
        RotateToMoveDirection();
        MoveCharacter();
    }

    
    // 入力取得（Horizontal / Vertical）
    private void HandleMovementInput()
    {
        float h = isPlayer1 ? Input.GetAxisRaw("Horizontal") : Input.GetAxisRaw("Horizontal2");
        float v = isPlayer1 ? Input.GetAxisRaw("Vertical") : Input.GetAxisRaw("Vertical2");

        moveDirection = new Vector3(h, 0f, v).normalized;
    }

  
    // Dash処理：状態管理 + 入力検出
    private void HandleDashLogic()
    {
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                dashTimer = 0f;
                isDashing = false;
                cooldownTimer = dashCooldown;
            }
            return;
        }

        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                cooldownTimer = 0f;
            }
            return;
        }

        if (Input.GetKeyDown(dashKey))
        {
            StartDash();
        }
    }

   
    // Dash開始
    private void StartDash()
    {
        isDashing = true;
        dashTimer = dashDuration;
    }

  
    // 移動方向へ向ける
    private void RotateToMoveDirection()
    {
        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }
    }


    // 移動処理
    private void MoveCharacter()
    {
        float speed = isDashing ? dashSpeed : moveSpeed;
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }
}
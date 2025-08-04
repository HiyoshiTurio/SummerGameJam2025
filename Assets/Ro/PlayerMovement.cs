using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1.0f;
    [SerializeField] private float turnSpeed = 360f;
    [SerializeField] private bool isPlayer1 = true;
    [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;

    private Rigidbody rb;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashElapsed = 0f;
    private float cooldownTimer = 0f;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovementInput();
        HandleDashLogic();
    }

    void FixedUpdate()
    {
        RotateToMoveDirection();
        MoveCharacter();
    }

   
    /// プレイヤーの移動方向を取得
    private void HandleMovementInput()
    {
        float h = isPlayer1 ? Input.GetAxisRaw("Horizontal") : Input.GetAxisRaw("Horizontal2");
        float v = isPlayer1 ? Input.GetAxisRaw("Vertical") : Input.GetAxisRaw("Vertical2");

        moveDirection = new Vector3(h, 0f, v).normalized;
    }

    
    /// Dash 状態の管理と入力検出
    private void HandleDashLogic()
    {
        if (isDashing)
        {
            dashElapsed += Time.deltaTime;
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
            {
                isDashing = false;
                dashElapsed = 0f;
                cooldownTimer = dashCooldown;
            }

            return;
        }

        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            return;
        }

        
        if (Input.GetKeyDown(dashKey) && moveDirection != Vector3.zero)
        {
            Debug.Log("Dash");
            StartDash();
        }
    }

  
    /// Dash 開始
    private void StartDash()
    {
        isDashing = true;
        dashTimer = dashDuration;
        dashElapsed = 0f;

       
    }


    /// 向いている方向を更新
    private void RotateToMoveDirection()
    {
       
        if (moveDirection != Vector3.zero)
        {
            Vector3 current = transform.forward.normalized;  
            Vector3 target = moveDirection.normalized;       

            float dot = Vector3.Dot(current, target);        // 2つのベクトルの内積（角度の違いを見る）
            dot = Mathf.Clamp(dot, -1f, 1f);                 

            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;   // 内積からラジアン角を得て、度数法に変換

            // 一定角度以上でなければ回転しない（微小な角度変化は無視）
            if (angle > 0.1f)
            {
                Vector3 cross = Vector3.Cross(current, target);     // 外積を使って、左右どちらに回転すべきかを判断
                float direction = cross.y > 0 ? 1f : -1f;            

                float maxStep = turnSpeed * Time.deltaTime;         // 1フレームで最大どれくらい回転できるか（回転速度）

                float step = Mathf.Min(angle, maxStep) * direction; // 実際に回転する角度（angle が maxStep を超えないように）

                transform.Rotate(0f, step, 0f);                      
            }
        }
    }


    /// 速度に応じて移動
    private void MoveCharacter()
    {
        float speed = moveSpeed;

        if (isDashing)
        {
            float t = dashElapsed / dashDuration;             
            speed = dashSpeed * Mathf.Sin((1f - t) * Mathf.PI * 0.5f);
            Debug.Log($"{speed}");
        }

        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }
}
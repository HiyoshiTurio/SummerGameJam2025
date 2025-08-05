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
    [SerializeField] private ParticleSystem dashEffect;
    [SerializeField] private Animator animator;

    private Rigidbody _rb;
    private bool _isDashing;
    private float _dashTimer;
    private float _dashElapsed;
    private float _cooldownTimer;

    private Vector3 _moveDirection = Vector3.zero;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
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
        const string WalkBool = "isWalking";
        var h = isPlayer1 ? Input.GetAxisRaw("Horizontal") : Input.GetAxisRaw("Horizontal2");
        var v = isPlayer1 ? Input.GetAxisRaw("Vertical") : Input.GetAxisRaw("Vertical2");

        animator.SetBool(WalkBool, h != 0 || v != 0);

        _moveDirection = new Vector3(h, 0f, v).normalized;
    }

    
    /// Dash 状態の管理と入力検出
    private void HandleDashLogic()
    {
        if (_isDashing)
        {
            _dashElapsed += Time.deltaTime;
            _dashTimer -= Time.deltaTime;

            if (_dashTimer <= 0f)
            {
                _isDashing = false;
                _dashElapsed = 0f;
                _cooldownTimer = dashCooldown;
            }

            return;
        }

        if (_cooldownTimer > 0f)
        {
            _cooldownTimer -= Time.deltaTime;
            return;
        }

        
        if (Input.GetKeyDown(dashKey) && _moveDirection != Vector3.zero)
        {
            Debug.Log("Dash");
            StartDash();
            const string DashAnim = "isDashing";
            animator.SetTrigger(DashAnim);
            SoundManager.Instance.Play(SoundKey.Dash);
            var effect =Instantiate(dashEffect, transform.position, Quaternion.identity);
            effect.gameObject.transform.forward = _moveDirection;
        }
    }

  
    /// Dash 開始
    private void StartDash()
    {
        _isDashing = true;
        _dashTimer = dashDuration;
        _dashElapsed = 0f;

       
    }


    /// 向いている方向を更新
    private void RotateToMoveDirection()
    {
       
        if (_moveDirection != Vector3.zero)
        {
            var current = transform.forward.normalized;  
            var target = _moveDirection.normalized;       

            var dot = Vector3.Dot(current, target);        // 2つのベクトルの内積（角度の違いを見る）
            dot = Mathf.Clamp(dot, -1f, 1f);                 

            var angle = Mathf.Acos(dot) * Mathf.Rad2Deg;   // 内積からラジアン角を得て、度数法に変換

            // 一定角度以上でなければ回転しない（微小な角度変化は無視）
            if (angle > 0.1f)
            {
                var cross = Vector3.Cross(current, target);     // 外積を使って、左右どちらに回転すべきかを判断
                var direction = cross.y > 0 ? 1f : -1f;            

                var maxStep = turnSpeed * Time.deltaTime;         // 1フレームで最大どれくらい回転できるか（回転速度）

                var step = Mathf.Min(angle, maxStep) * direction; // 実際に回転する角度（angle が maxStep を超えないように）

                transform.Rotate(0f, step, 0f);                      
            }
        }
    }


    /// 速度に応じて移動
    private void MoveCharacter()
    {
        var speed = moveSpeed;

        if (_moveDirection == Vector3.zero && !_isDashing)
        {
            _rb.velocity = Vector3.zero; 
            return;
        }

        if (_isDashing)
        {
            var t = _dashElapsed / dashDuration;
            speed = dashSpeed * Mathf.Sin((1f - t) * Mathf.PI * 0.5f);
        }

        _rb.velocity = _moveDirection * speed;
    }
}